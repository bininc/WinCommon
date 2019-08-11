using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.Utils;

namespace DevSkin
{
    public  class UCBase : DevExpress.XtraEditors.XtraUserControl
    {
        #region 事件
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        public event Action<DialogResult> FormClosed;
        #endregion

        #region 构造函数
        public UCBase()
        {
        }

        #endregion

        #region 字段
        private string title;
        private Form _form1;
        private bool _force = false;
        private bool _isShown = false;
        #endregion

        #region 属性
        protected bool TouchUI = false;
        /// <summary>
        /// 控件的父窗体
        /// </summary>
        protected Form _form
        {
            get
            {
                return _form1;
            }
            set
            {
                if (_form1 != value)
                {
                    _form1 = value;
                    _form1.Shown += _form1_Shown;
                    _form1.Closing += _form1_Closing;
                    _form1.FormClosed += _form1_FormClosed;
                    _form1.VisibleChanged += _form1_VisibleChanged;
                }
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                //if (string.IsNullOrWhiteSpace(title))
                //    return this.Name;
                return title;
            }
            set
            {
                title = value;
                OnTitleChanged();
            }
        }
        /// <summary>
        /// 标题描述
        /// </summary>
        public string TitleDescription { get; set; }

        private DialogResult _dialogResult = DialogResult.None;
        protected DialogResult DialogResult
        {
            get
            {
                return _dialogResult;
                //if (_form == null) return DialogResult.None;
                //return _form.DialogResult;
            }
            set
            {
                _dialogResult = value;
                if (_form != null)
                    _form.DialogResult = value;
                if (_dialogResult == DialogResult.OK || _dialogResult == DialogResult.Cancel)
                    _form.Close();
            }
        }
        /// <summary>
        /// 编辑值
        /// </summary>
        public virtual object EditValue { get; set; }

        protected override void OnParentChanged(EventArgs e)
        {
            if (ParentForm != null)
                _form = ParentForm;
            base.OnParentChanged(e);
        }

        #endregion

        #region 方法

        protected override void OnFirstLoad()
        {
            base.OnFirstLoad();

            if (!DesignMode)
            {
                if (_form == null)
                {
                    _form = FindParentForm(this);
                    if (_form != null)
                    {
                        if (_form.Visible)
                            InvokeFormShown();
                    }
                    else
                    {
                        if (Parent != null)
                        {
                            InvokeFormShown();
                            Parent.Disposed += (s, e) => OnFormClosed();
                        }
                    }
                }
                else
                {
                    if (_form.Visible)
                        InvokeFormShown();
                }
            }
        }

        private Form FindParentForm(Control ctrl)
        {
            if (ctrl!=null&& ctrl.Parent!=null)
            {
                if (ctrl.Parent is Form)
                {
                    return (Form) ctrl.Parent;
                }
                else
                {
                    return FindParentForm(ctrl.Parent);
                }
            }
            return null;
        }

        private FormBase _formBase = null;
        public void ShowForm(IWin32Window owner = null)
        {
            if (_formBase != null && !_formBase.IsDisposed)
            {
                if (_formBase.Visible)
                {
                    _formBase.WindowState = FormWindowState.Normal;
                    _formBase.Activate();
                }
                else
                    _formBase.Show(owner);
                return;
            }
            FormBase box = new FormBase();
            _formBase = box;
            box.FormClosed += (sender, e) => { FormClosed?.Invoke(DialogResult); };
            //box.ShowIcon = false;
            box.Controls.Add(this);
            box.ClientSize = new Size(this.Width + 10, this.Height + 10);
            box.Text = this.title;
            this.Left = 5;
            this.Top = 5;
            this.Dock = DockStyle.Fill;
            box.StartPosition = FormStartPosition.CenterScreen;
            _form = box;
            if (TouchUI)
            {
                //box.LookAndFeel.TouchUIMode = DefaultBoolean.True;
                LookAndFeel.TouchUIMode = DefaultBoolean.True;
            }
            if (owner == null)
                box.Show();
            else
            {
                ISupportLookAndFeel iLookAndFeel = owner as ISupportLookAndFeel;
                if (iLookAndFeel != null)
                {
                    UserLookAndFeel lookAndFeel = iLookAndFeel.LookAndFeel;
                    string skinName = lookAndFeel.UseDefaultLookAndFeel ? UserLookAndFeel.Default.SkinName : lookAndFeel.SkinName;
                    if (skinName != box.LookAndFeel.SkinName)
                        box.LookAndFeel.SetSkinStyle(skinName);
                }
                box.Show(owner);
            }
        }

        public DialogResult ShowDialog(IWin32Window owner = null)
        {
            FormBox box = new FormBox();
            box.Controls.Add(this);
            box.ClientSize = new Size(this.Width + 10, this.Height + 10);
            box.Text = this.title;
            this.Left = 5;
            this.Top = 5;
            box.StartPosition = FormStartPosition.CenterScreen;
            //box.TouchUI = TouchUI;
            if (TouchUI)
            {
                //box.LookAndFeel.TouchUIMode = DefaultBoolean.True;
                LookAndFeel.TouchUIMode = DefaultBoolean.True;
            }
            _form = box;
            return box.ShowDialog(owner);
        }

        public DialogResult ShowPanel()
        {
            FormBox box = new FormBox();
            box.FormBorderStyle = FormBorderStyle.None;
            box.Controls.Add(this);
            box.ClientSize = this.Size;
            box.Text = this.title;
            this.Dock = DockStyle.Fill;
            //box.TouchUI = TouchUI;
            if (TouchUI)
            {
                //box.LookAndFeel.TouchUIMode = DefaultBoolean.True;
                LookAndFeel.TouchUIMode = DefaultBoolean.True;
            }
            _form = box;
            return box.ShowDialog();
        }

        private void _form1_Closing(object sender, CancelEventArgs e)
        {
            if (!_force)
                e.Cancel = !OnFormClosing();
            _force = false;
        }

        public void CloseForm(bool force = false)
        {
            _force = force;
            _form?.Close();
        }

        protected virtual bool OnFormClosing()
        {
            return true;
        }

        private void _form1_Shown(object sender, EventArgs e)
        {
            InvokeFormShown();

        }

        public virtual void OnFormShown()
        {

        }

        private void InvokeFormShown()
        {
            if (!_isShown)
            {
                OnFormShown();
                _isShown = true;
            }
        }

        private void _form1_VisibleChanged(object sender, EventArgs e)
        {
            if (_form1.Visible)
                InvokeFormShown();
            else
                _isShown = false;
        }

        void _form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnFormClosed();
            FormClosed?.Invoke(DialogResult);
        }

        protected virtual void OnFormClosed()
        {

        }

        protected virtual void OnTitleChanged()
        {

        }

        public DialogResult ShowFlyoutDialog(FlyoutStyle style = FlyoutStyle.MessageBox)
        {
            FlyoutProperties properties = new FlyoutProperties() { Style = style };
            return FlyoutDialog.Show(null, this, properties);
        }

        public virtual void OnParams(params object[] args)
        {

        }
        #endregion
    }
}
