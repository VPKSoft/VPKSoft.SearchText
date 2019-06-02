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
            searcher = new TextSearcherAndReplacer(tbMain.Text, tbSearchString.Text, TextSearcherEnums.SearchType.SimpleExtended);
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
