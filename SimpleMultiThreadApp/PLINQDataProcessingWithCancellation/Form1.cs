using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace PLINQDataProcessingWithCancellation {
    public partial class MainForm : Form {
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        public MainForm() {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e) {
            //Запустить новую задачу для обработки целых чисел.
            Task.Factory.StartNew(() => {
                ProcessInData();
                });
        }

        private void ProcessInData() {
            //Получить очень большой массив целых чисел
            int[] source = Enumerable.Range(1, 100000000).ToArray();
            int[] modThreeIsZero = null;

            // говорим запросу PLINQ что он должен ожидать входящего запроса на отмену выполнения, добавив WithCancellation(cancelToken.Token)
            try {
                modThreeIsZero = (from num in source.AsParallel().WithCancellation(cancelToken.Token)
                                              where num % 3 == 0
                                              orderby num descending
                                              select num).ToArray();
                MessageBox.Show(string.Format("Found {0} numbers that match query!", modThreeIsZero.Count()));
            } catch (OperationCanceledException ex) {
                this.Invoke((Action)delegate {
                    this.Text = ex.Message;
                });    
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            cancelToken.Cancel();
        }
    }
}
