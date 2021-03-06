﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToXmlWinApp {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void btnAddNewItem_Click(object sender, EventArgs e) {
            // Добавить новый элемент к документу
            LinqToXmlObjectModel.InsertNewElement(txtMake.Text, txtColor.Text, txtPetName.Text);

            // отобразить текущий xml склада в элементе управления textbox
            txtInventory.Text = LinqToXmlObjectModel.GetXmlInventory().ToString();
        }

        private void btnLookUpColors_Click(object sender, EventArgs e) {
            LinqToXmlObjectModel.LookUpColorsForMake(cbxMakeToLookUp.Text);
        }

        private void MainForm_Load(object sender, EventArgs e) {
            // Отобразить текущий XML-документ склада в элементе управления TexBox
            txtInventory.Text = LinqToXmlObjectModel.GetXmlInventory().ToString();
         }
    }
}
