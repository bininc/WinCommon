namespace DevControl
{
    partial class UCYearMonthSelector
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbBeginYear = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbBeginMonth = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginMonth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmbBeginYear);
            this.flowLayoutPanel1.Controls.Add(this.cmbBeginMonth);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(189, 41);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cmbBeginYear
            // 
            this.cmbBeginYear.Location = new System.Drawing.Point(0, 3);
            this.cmbBeginYear.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cmbBeginYear.Name = "cmbBeginYear";
            this.cmbBeginYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBeginYear.Size = new System.Drawing.Size(100, 36);
            this.cmbBeginYear.TabIndex = 1;
            // 
            // cmbBeginMonth
            // 
            this.cmbBeginMonth.Location = new System.Drawing.Point(106, 3);
            this.cmbBeginMonth.Name = "cmbBeginMonth";
            this.cmbBeginMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBeginMonth.Size = new System.Drawing.Size(80, 36);
            this.cmbBeginMonth.TabIndex = 2;
            // 
            // UCYearMonthSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(189, 41);
            this.Name = "UCYearMonthSelector";
            this.Size = new System.Drawing.Size(189, 41);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginMonth.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBeginYear;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBeginMonth;
    }
}
