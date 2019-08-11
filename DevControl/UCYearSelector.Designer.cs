namespace DevControl
{
    partial class UCYearSelector
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
            this.cmbYear = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbYear
            // 
            this.cmbYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbYear.Location = new System.Drawing.Point(0, 0);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbYear.Size = new System.Drawing.Size(100, 36);
            this.cmbYear.TabIndex = 0;
            // 
            // UCYearSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbYear);
            this.Name = "UCYearSelector";
            this.Size = new System.Drawing.Size(100, 36);
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ImageComboBoxEdit cmbYear;
    }
}
