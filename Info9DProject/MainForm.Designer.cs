namespace Info9DProject
{
    partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DGVTotal = new System.Windows.Forms.DataGridView();
            this.TCLTotal = new System.Windows.Forms.TabControl();
            this.QualityTP = new System.Windows.Forms.TabPage();
            this.TBDescribeQuality = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SafetyTP = new System.Windows.Forms.TabPage();
            this.TBDescribeSafety = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BUTAdd = new System.Windows.Forms.Button();
            this.BUTShow = new System.Windows.Forms.Button();
            this.IDofDGVTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAMEofDGVTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BUTFocus = new System.Windows.Forms.Button();
            this.BUTQuery = new System.Windows.Forms.Button();
            this.BUTChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTotal)).BeginInit();
            this.TCLTotal.SuspendLayout();
            this.QualityTP.SuspendLayout();
            this.SafetyTP.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 57);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DGVTotal);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TCLTotal);
            this.splitContainer1.Size = new System.Drawing.Size(800, 393);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 0;
            // 
            // DGVTotal
            // 
            this.DGVTotal.AllowUserToAddRows = false;
            this.DGVTotal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVTotal.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DGVTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVTotal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDofDGVTotal,
            this.NAMEofDGVTotal});
            this.DGVTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVTotal.Location = new System.Drawing.Point(0, 0);
            this.DGVTotal.Name = "DGVTotal";
            this.DGVTotal.RowTemplate.Height = 23;
            this.DGVTotal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVTotal.Size = new System.Drawing.Size(800, 190);
            this.DGVTotal.TabIndex = 0;
            this.DGVTotal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVTotal_CellClick);
            // 
            // TCLTotal
            // 
            this.TCLTotal.Controls.Add(this.QualityTP);
            this.TCLTotal.Controls.Add(this.SafetyTP);
            this.TCLTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TCLTotal.Location = new System.Drawing.Point(0, 0);
            this.TCLTotal.Name = "TCLTotal";
            this.TCLTotal.SelectedIndex = 0;
            this.TCLTotal.Size = new System.Drawing.Size(800, 199);
            this.TCLTotal.TabIndex = 0;
            // 
            // QualityTP
            // 
            this.QualityTP.Controls.Add(this.TBDescribeQuality);
            this.QualityTP.Controls.Add(this.label1);
            this.QualityTP.Location = new System.Drawing.Point(4, 22);
            this.QualityTP.Name = "QualityTP";
            this.QualityTP.Padding = new System.Windows.Forms.Padding(3);
            this.QualityTP.Size = new System.Drawing.Size(792, 173);
            this.QualityTP.TabIndex = 0;
            this.QualityTP.Text = "质量";
            this.QualityTP.UseVisualStyleBackColor = true;
            // 
            // TBDescribeQuality
            // 
            this.TBDescribeQuality.Location = new System.Drawing.Point(85, 25);
            this.TBDescribeQuality.Name = "TBDescribeQuality";
            this.TBDescribeQuality.Size = new System.Drawing.Size(165, 21);
            this.TBDescribeQuality.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "说明：";
            // 
            // SafetyTP
            // 
            this.SafetyTP.Controls.Add(this.TBDescribeSafety);
            this.SafetyTP.Controls.Add(this.label2);
            this.SafetyTP.Location = new System.Drawing.Point(4, 22);
            this.SafetyTP.Name = "SafetyTP";
            this.SafetyTP.Padding = new System.Windows.Forms.Padding(3);
            this.SafetyTP.Size = new System.Drawing.Size(792, 173);
            this.SafetyTP.TabIndex = 1;
            this.SafetyTP.Text = "安全";
            this.SafetyTP.UseVisualStyleBackColor = true;
            // 
            // TBDescribeSafety
            // 
            this.TBDescribeSafety.Location = new System.Drawing.Point(98, 36);
            this.TBDescribeSafety.Name = "TBDescribeSafety";
            this.TBDescribeSafety.Size = new System.Drawing.Size(173, 21);
            this.TBDescribeSafety.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "说明：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BUTChange);
            this.groupBox1.Controls.Add(this.BUTQuery);
            this.groupBox1.Controls.Add(this.BUTFocus);
            this.groupBox1.Controls.Add(this.BUTAdd);
            this.groupBox1.Controls.Add(this.BUTShow);
            this.groupBox1.Location = new System.Drawing.Point(0, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 47);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工具栏";
            // 
            // BUTAdd
            // 
            this.BUTAdd.Location = new System.Drawing.Point(300, 18);
            this.BUTAdd.Name = "BUTAdd";
            this.BUTAdd.Size = new System.Drawing.Size(75, 23);
            this.BUTAdd.TabIndex = 1;
            this.BUTAdd.Text = "新增";
            this.BUTAdd.UseVisualStyleBackColor = true;
            this.BUTAdd.Click += new System.EventHandler(this.BUTAdd_Click);
            // 
            // BUTShow
            // 
            this.BUTShow.Location = new System.Drawing.Point(16, 18);
            this.BUTShow.Name = "BUTShow";
            this.BUTShow.Size = new System.Drawing.Size(75, 23);
            this.BUTShow.TabIndex = 0;
            this.BUTShow.Text = "显示";
            this.BUTShow.UseVisualStyleBackColor = true;
            this.BUTShow.Click += new System.EventHandler(this.BUTShow_Click);
            // 
            // IDofDGVTotal
            // 
            this.IDofDGVTotal.FillWeight = 1F;
            this.IDofDGVTotal.HeaderText = "ID";
            this.IDofDGVTotal.Name = "IDofDGVTotal";
            // 
            // NAMEofDGVTotal
            // 
            this.NAMEofDGVTotal.FillWeight = 1F;
            this.NAMEofDGVTotal.HeaderText = "名称";
            this.NAMEofDGVTotal.Name = "NAMEofDGVTotal";
            // 
            // BUTFocus
            // 
            this.BUTFocus.Location = new System.Drawing.Point(109, 18);
            this.BUTFocus.Name = "BUTFocus";
            this.BUTFocus.Size = new System.Drawing.Size(75, 23);
            this.BUTFocus.TabIndex = 2;
            this.BUTFocus.Text = "关注";
            this.BUTFocus.UseVisualStyleBackColor = true;
            this.BUTFocus.Click += new System.EventHandler(this.BUTFocus_Click);
            // 
            // BUTQuery
            // 
            this.BUTQuery.Location = new System.Drawing.Point(191, 18);
            this.BUTQuery.Name = "BUTQuery";
            this.BUTQuery.Size = new System.Drawing.Size(75, 23);
            this.BUTQuery.TabIndex = 3;
            this.BUTQuery.Text = "查询";
            this.BUTQuery.UseVisualStyleBackColor = true;
            this.BUTQuery.Click += new System.EventHandler(this.BUTQuery_Click);
            // 
            // BUTChange
            // 
            this.BUTChange.Location = new System.Drawing.Point(397, 18);
            this.BUTChange.Name = "BUTChange";
            this.BUTChange.Size = new System.Drawing.Size(75, 23);
            this.BUTChange.TabIndex = 4;
            this.BUTChange.Text = "修改";
            this.BUTChange.UseVisualStyleBackColor = true;
            this.BUTChange.Click += new System.EventHandler(this.BUTChange_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "信息总览";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVTotal)).EndInit();
            this.TCLTotal.ResumeLayout(false);
            this.QualityTP.ResumeLayout(false);
            this.QualityTP.PerformLayout();
            this.SafetyTP.ResumeLayout(false);
            this.SafetyTP.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView DGVTotal;
        private System.Windows.Forms.TabControl TCLTotal;
        private System.Windows.Forms.TabPage QualityTP;
        private System.Windows.Forms.TabPage SafetyTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBDescribeQuality;
        private System.Windows.Forms.TextBox TBDescribeSafety;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BUTShow;
        private System.Windows.Forms.Button BUTAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDofDGVTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAMEofDGVTotal;
        private System.Windows.Forms.Button BUTFocus;
        private System.Windows.Forms.Button BUTQuery;
        private System.Windows.Forms.Button BUTChange;
    }
}