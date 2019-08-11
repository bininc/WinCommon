namespace DevControl
{
    partial class UCYearMonthRangeSelector
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
            this.cmbBeginYear = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbBeginMonth = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbEndYear = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbEndMonth = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginYear.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEndYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEndMonth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBeginYear
            // 
            this.cmbBeginYear.Location = new System.Drawing.Point(0, 3);
            this.cmbBeginYear.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cmbBeginYear.Name = "cmbBeginYear";
            this.cmbBeginYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBeginYear.Size = new System.Drawing.Size(100, 36);
            this.cmbBeginYear.TabIndex = 0;
            this.cmbBeginYear.SelectedIndexChanged += new System.EventHandler(this.cmbBeginYear_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(192, 13);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "至";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmbBeginYear);
            this.flowLayoutPanel1.Controls.Add(this.cmbBeginMonth);
            this.flowLayoutPanel1.Controls.Add(this.labelControl1);
            this.flowLayoutPanel1.Controls.Add(this.cmbEndYear);
            this.flowLayoutPanel1.Controls.Add(this.cmbEndMonth);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(397, 42);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // cmbBeginMonth
            // 
            this.cmbBeginMonth.Location = new System.Drawing.Point(106, 3);
            this.cmbBeginMonth.Name = "cmbBeginMonth";
            this.cmbBeginMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBeginMonth.Size = new System.Drawing.Size(80, 36);
            this.cmbBeginMonth.TabIndex = 0;
            this.cmbBeginMonth.SelectedIndexChanged += new System.EventHandler(this.cmbBeginYear_SelectedIndexChanged);
            // 
            // cmbEndYear
            // 
            this.cmbEndYear.Location = new System.Drawing.Point(210, 3);
            this.cmbEndYear.Name = "cmbEndYear";
            this.cmbEndYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEndYear.Size = new System.Drawing.Size(100, 36);
            this.cmbEndYear.TabIndex = 2;
            this.cmbEndYear.SelectedIndexChanged += new System.EventHandler(this.cmbBeginYear_SelectedIndexChanged);
            // 
            // cmbEndMonth
            // 
            this.cmbEndMonth.Location = new System.Drawing.Point(316, 3);
            this.cmbEndMonth.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cmbEndMonth.Name = "cmbEndMonth";
            this.cmbEndMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEndMonth.Size = new System.Drawing.Size(80, 36);
            this.cmbEndMonth.TabIndex = 3;
            this.cmbEndMonth.SelectedIndexChanged += new System.EventHandler(this.cmbBeginYear_SelectedIndexChanged);
            // 
            // UCYearMonthRangeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(397, 42);
            this.MinimumSize = new System.Drawing.Size(397, 42);
            this.Name = "UCYearMonthRangeSelector";
            this.Size = new System.Drawing.Size(397, 42);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginYear.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEndYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEndMonth.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBeginYear;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBeginMonth;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbEndYear;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbEndMonth;
    }
}
