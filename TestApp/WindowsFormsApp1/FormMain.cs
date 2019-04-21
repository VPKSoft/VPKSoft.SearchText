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

        private void BtWholeWord_Click(object sender, EventArgs e)
        {
            TextSearcherAndReplacer searcher = new TextSearcherAndReplacer(tbMain.Text, tbSearchString.Text, TextSearcherEnums.SearchType.Normal);
            searcher.WholeWord = true;
            var value = searcher.Forward();
            tbMain.SelectionStart = value.position;
            tbMain.SelectionLength = value.length;
        }

        private void MnuOpen_Click(object sender, EventArgs e)
        {
            if (odAnyFile.ShowDialog() == DialogResult.OK)
            {
                tbMain.Text = File.ReadAllText(odAnyFile.FileName);
            }
        }
    }
}
