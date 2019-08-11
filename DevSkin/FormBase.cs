using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.Threading;
using DevExpress.Utils;

namespace DevSkin
{

    public class FormBase : DevExpress.XtraEditors.XtraForm
    {
        #region 构造函数
        public FormBase()
        {
            InitializeComponent();
            if (DesignMode)
                return;
            FormList.Add(this);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 316);
            this.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.False;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FormBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }
        #endregion

        #region 事件

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (FormList.Contains(this))
                FormList.Remove(this);
            FlushMemory();
        }

        #endregion

        #region 自定义方法

        public bool TouchUI = false;
        /// <summary>
        /// 资源回收
        /// </summary>
        public static void FlushMemory()
        {
            try
            {
                GC.Collect();
            }
            catch
            { }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        public virtual void Init() { }

        public new DialogResult ShowDialog(IWin32Window owner = null)
        {
            Enabled = true;
            if (TouchUI)
                LookAndFeel.TouchUIMode = DefaultBoolean.True;
            if (owner == null) return base.ShowDialog();
            else
            {
                ISupportLookAndFeel iLookAndFeel = owner as ISupportLookAndFeel;
                if (iLookAndFeel != null)
                {
                    UserLookAndFeel lookAndFeel = iLookAndFeel.LookAndFeel;
                    string skinName = lookAndFeel.UseDefaultLookAndFeel ? UserLookAndFeel.Default.SkinName : lookAndFeel.SkinName;
                    if (skinName != LookAndFeel?.SkinName)
                    {
                        LookAndFeel?.SetSkinStyle(skinName);
                    }
                }

                return base.ShowDialog(owner);
            }
        }

        private bool isLoaded = false;

        protected override void OnLoad(EventArgs e)
        {
            if (!isLoaded)
            {
                OnFirstLoad();
                isLoaded = true;
            }
            base.OnLoad(e);
        }

        /// <summary>
        /// 第一次加载
        /// </summary>
        protected virtual void OnFirstLoad()
        { }

        #endregion

        #region 属性
        /// <summary>
        /// 已经创建的所有窗体列表
        /// </summary>
        public static readonly List<FormBase> FormList = new List<FormBase>();
        #endregion
    }
}
