using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace DataParallelismWithForEach {
    public partial class MainForm : Form {
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        public MainForm() {
            InitializeComponent();
        }
        
        public void ProcessFiles() {
            ParallelOptions parOpts = new ParallelOptions();
            parOpts.CancellationToken = cancelToken.Token;
            parOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            string[] files = Directory.GetFiles(@"C:\Users\Public\Pictures", "*.jpg", SearchOption.AllDirectories);
            string newDir = @"C:\ModifiedPictures";
            Directory.CreateDirectory(newDir);
            try {
                Parallel.ForEach(files, parOpts, currentFile => {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();

                    string filename = Path.GetFileName(currentFile);
                    using (Bitmap bitmap = new Bitmap(currentFile)) {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, filename));

                        this.Invoke((Action)delegate {
                            this.Text = string.Format("Processing {0} on thread {1}", filename, 
                                Thread.CurrentThread.ManagedThreadId);
                        });
                    }
                });
            } catch (OperationCanceledException ex) {
                this.Invoke((Action)delegate {
                    this.Text = ex.Message;
                });
            }
            //MessageBox.Show("All pictures rotated!");
        }



        private void btnProcessImages_Click(object sender, EventArgs e) {
            Task.Factory.StartNew(() => 
            {
                ProcessFiles();
            });
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            cancelToken.Cancel();
        }
    }
}
