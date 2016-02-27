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

namespace FunWithCSharpAsync {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        // данный метод должен вызываться в неблокирующей манере
        private async void btnCallMethod_Click(object sender, EventArgs e) {
            // если метод декорируется ключевым словом async но не имеет хотя бы ОДНОГО 
            // внутреннего вызова метода с применением await то получается БЛОКИРУЮЩИЙ, СИНХРОННЫЙ вызов(компилятор ругнется)
            this.Text = await DoWorkAsync(); // метод поддерживающий await это метод возвращающий Task<T>
        }

        // Соглашение, что метод который возвращает Task помечается async
        private async Task<string> DoWorkAsync() {
            return await Task.Run(() => {
                Thread.Sleep(10000);
                return "Done with work";
            });
        }
        // В случае если метод возвращает void то мы юзаем необощенный Task и опускаем везде return
        private async Task MethodReturningVoidAsync() {
            await Task.Run(() => {/* Выполнение каких то действий... }*/
                Thread.Sleep(4000);
            });
        }

        private async void btnvoidCallMethod_Click(object sender, EventArgs e) {
            await MethodReturningVoidAsync();
            MessageBox.Show("Done!");
        }
    }
}
