namespace TestApp
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.odAnyFile = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tbMain = new System.Windows.Forms.TextBox();
            this.tbSearchString = new System.Windows.Forms.TextBox();
            this.btWholeWordForward = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.btWholeWordBackward = new System.Windows.Forms.Button();
            this.btTestSomething = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // odAnyFile
            // 
            this.odAnyFile.Filter = "All files|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tbMain
            // 
            this.tbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMain.HideSelection = false;
            this.tbMain.Location = new System.Drawing.Point(12, 27);
            this.tbMain.Multiline = true;
            this.tbMain.Name = "tbMain";
            this.tbMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMain.Size = new System.Drawing.Size(776, 369);
            this.tbMain.TabIndex = 0;
            this.tbMain.WordWrap = false;
            this.tbMain.TextChanged += new System.EventHandler(this.TbMain_TextChanged);
            // 
            // tbSearchString
            // 
            this.tbSearchString.Location = new System.Drawing.Point(12, 418);
            this.tbSearchString.Name = "tbSearchString";
            this.tbSearchString.Size = new System.Drawing.Size(321, 20);
            this.tbSearchString.TabIndex = 1;
            this.tbSearchString.TextChanged += new System.EventHandler(this.TbMain_TextChanged);
            // 
            // btWholeWordForward
            // 
            this.btWholeWordForward.Location = new System.Drawing.Point(659, 415);
            this.btWholeWordForward.Name = "btWholeWordForward";
            this.btWholeWordForward.Size = new System.Drawing.Size(129, 23);
            this.btWholeWordForward.TabIndex = 2;
            this.btWholeWordForward.Text = "Whole word test >>";
            this.btWholeWordForward.UseVisualStyleBackColor = true;
            this.btWholeWordForward.Click += new System.EventHandler(this.BtWholeWordForward_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(103, 22);
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.MnuOpen_Click);
            // 
            // btWholeWordBackward
            // 
            this.btWholeWordBackward.Location = new System.Drawing.Point(524, 415);
            this.btWholeWordBackward.Name = "btWholeWordBackward";
            this.btWholeWordBackward.Size = new System.Drawing.Size(129, 23);
            this.btWholeWordBackward.TabIndex = 4;
            this.btWholeWordBackward.Text = "<< Whole word test";
            this.btWholeWordBackward.UseVisualStyleBackColor = true;
            this.btWholeWordBackward.Click += new System.EventHandler(this.BtWholeWordBackward_Click);
            // 
            // btTestSomething
            // 
            this.btTestSomething.Location = new System.Drawing.Point(389, 415);
            this.btTestSomething.Name = "btTestSomething";
            this.btTestSomething.Size = new System.Drawing.Size(129, 23);
            this.btTestSomething.TabIndex = 5;
            this.btTestSomething.Text = "Test something";
            this.btTestSomething.UseVisualStyleBackColor = true;
            this.btTestSomething.Click += new System.EventHandler(this.BtTestSomething_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btTestSomething);
            this.Controls.Add(this.btWholeWordBackward);
            this.Controls.Add(this.btWholeWordForward);
            this.Controls.Add(this.tbSearchString);
            this.Controls.Add(this.tbMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog odAnyFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tbMain;
        private System.Windows.Forms.TextBox tbSearchString;
        private System.Windows.Forms.Button btWholeWordForward;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.Button btWholeWordBackward;
        private System.Windows.Forms.Button btTestSomething;
    }
}

