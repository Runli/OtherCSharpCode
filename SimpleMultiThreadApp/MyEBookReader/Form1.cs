using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace MyEBookReader {
    public partial class MainForm : Form {
        private string theEBook = "";
        public MainForm() {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e) {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += (s, eArgs) => {
                theEBook = eArgs.Result;
                txtBook.Text = theEBook;
            };
            // загрузить книгу "A Tale of Two Cities" Чарльза Диккенса
            string uri = txtBoxInput.Text;
            if (uri.Contains("//"))  wc.DownloadStringAsync(new Uri(uri));
            else wc.DownloadStringAsync(new Uri("http://www.gutenberg.org/files/98/98-8.txt"));
        }

        private void btnGetStats_Click(object sender, EventArgs e) {
            //Получить слова из электронной книги
            string[] words = theEBook.Split(new char[] { 
                ' ', '\u000A', ',', '.', ';', ':', '-', '?', '/', '(', ')', '{', '}' },
                StringSplitOptions.RemoveEmptyEntries);
            //НАйти 10 наиболее часто встречающихся слов.
            string[] tenMostCommon = null;

            // Получить самое длинное слово.
            string longestWord = null;
            // Метод Parallel.Invoke() ожидает в качестве параметра масиива делагов Action<> который 
            //передается неявно с помощью лямбда выражения.
            Parallel.Invoke(
                () => {
                    tenMostCommon = FindTenMostCommon(words);
                },
                () => {
                    longestWord = FindLongestWord(words);
                });
            // Когда все задачи завершены, построить строку,
            // показывающую всю статистику в окне сообщений.
            StringBuilder bookStats = new StringBuilder("Ten most Common words are: \n");
            foreach (string word in tenMostCommon) {
                bookStats.AppendLine(word);
            }
            bookStats.AppendFormat("Longest word is \"{0}\"", longestWord);
            bookStats.AppendLine();
            MessageBox.Show(bookStats.ToString(), "Book Info");

        }

        private string FindLongestWord(string[] words) {
            // Возвращаем самое длинное слово. капец с Linq так просто это в одну строку!!!!
            return (from word in words orderby word.Length descending select word).FirstOrDefault();
        }

        private string[] FindTenMostCommon(string[] words) {
            var frequencyOrder = from word in words
                                 where word.Length > 6
                                 group word by word into g
                                 orderby g.Count() descending
                                 select g.Key;
            string[] commonWords = (frequencyOrder.Take(10)).ToArray();
            return commonWords;
                                    
        }

    }
}
