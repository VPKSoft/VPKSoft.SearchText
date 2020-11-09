﻿#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.SearchText;

namespace TestApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        TextSearcherAndReplacer searcher = null;


        private void BtWholeWord_Click(object sender, EventArgs e)
        {

        }

        private void MnuOpen_Click(object sender, EventArgs e)
        {
            if (odAnyFile.ShowDialog() == DialogResult.OK)
            {
                tbMain.Text = File.ReadAllText(odAnyFile.FileName);
            }
        }

        private void TbMain_TextChanged(object sender, EventArgs e)
        {
            searcher = new TextSearcherAndReplacer(tbMain.Text, tbSearchString.Text, TextSearcherEnums.SearchType.Normal);
            searcher.WrapAround = true;
            searcher.WholeWord = false;
        }

        private void BtWholeWordBackward_Click(object sender, EventArgs e)
        {
            var value = searcher.Backward();
            if (value != TextSearcherAndReplacer.Empty)
            {
                tbMain.SelectionStart = value.position;
                tbMain.SelectionLength = value.length;
                tbMain.ScrollToCaret();
            }
        }

        private void BtWholeWordForward_Click(object sender, EventArgs e)
        {
            var value = searcher.Forward();
            if (value != TextSearcherAndReplacer.Empty)
            {
                tbMain.SelectionStart = value.position;
                tbMain.SelectionLength = value.length;
                tbMain.ScrollToCaret();
            }
        }

        private void BtTestSomething_Click(object sender, EventArgs e)
        {
            var result = searcher.FindAll(100);
        }
    }
}
