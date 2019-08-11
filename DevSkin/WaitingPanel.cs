using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace DevSkin
{
    public partial class WaitingPanel : UCBase
    {
        #region 构造函数
        public WaitingPanel()
        {
            //SetStyle(System.Windows.Forms.ControlStyles.Opaque, true);
            InitializeComponent();
            //this.SetStyle(ControlStyles.UserPaint,true);
            ////设置Style支持透明背景色
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.FromArgb(100, 100, 100, 100);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 显示等待Panel
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="getDataMethod"></param>
        /// <param name="getDataCompleteMethod"></param>
        /// <param name="waitingMsg"></param>
        public static void ShowPanel(Control parentControl, Func<object> getDataMethod, Action<object> getDataCompleteMethod, string waitingMsg = "数据加载中")
        {
            if (parentControl == null) return;

            WaitingPanelEx panelEx = WaitingPanelEx.NewWithControl(parentControl);
            panelEx.Show(waitingMsg);
            if (getDataMethod != null && getDataCompleteMethod != null)
                parentControl.CrossThreadCallsAsync(getDataMethod, x =>
                 {
                     getDataCompleteMethod?.Invoke(x);
                     panelEx.Close();
                 });
        }
        /// <summary>
        /// 隐藏等待Panel
        /// </summary>
        /// <param name="parentControl"></param>
        public static void HidePanel(Control parentControl)
        {
            if (parentControl == null) return;
            WaitingPanelEx panelEx = WaitingPanelEx.NewWithControl(parentControl);
            panelEx.Close();
        }

        /// <summary>
        /// 显示等待框
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="waitingMsg"></param>
        public void Show(Control parentControl, string waitingMsg = "数据加载中")
        {
            if (parentControl == null) return;

            #region 显示加载窗口
            progressPanelMain.Caption = waitingMsg + "...   ";
            progressPanelMain.Width += 50;
            progressPanelMain.Height += 5;
            SizeChanged += (object sender, EventArgs e) =>
            {
                progressPanelMain.Top = (int)Math.Round((this.Height - progressPanelMain.Height) / 2.0);
                progressPanelMain.Left = (int)Math.Round((this.Width - progressPanelMain.Width) / 2.0);
            };

            parentControl.Controls.Add(this);
            Dock = DockStyle.Fill;
            BringToFront();
            Show();
            parentControl.Enabled = false;
            #endregion
        }

        /// <summary>
        /// 隐藏等待窗
        /// </summary>
        /// <param name="parentControl"></param>
        public void Hide(Control parentControl)
        {
            if (parentControl == null) return;

            #region 隐藏加载窗口
            SendToBack();
            Hide();
            parentControl.Controls.Remove(this);
            //Instance.Dispose();
            //instance = null;
            parentControl.Enabled = true;
            #endregion
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    float vlblControlWidth;
        //    float vlblControlHeight;

        //    Pen labelBorderPen;
        //    SolidBrush labelBackColorBrush;

        //    if (_transparentBG)
        //    {
        //        Color drawColor = Color.FromArgb(this._alpha, this.BackColor);
        //        labelBorderPen = new Pen(drawColor, 0);
        //        labelBackColorBrush = new SolidBrush(drawColor);
        //    }
        //    else
        //    {
        //        labelBorderPen = new Pen(this.BackColor, 0);
        //        labelBackColorBrush = new SolidBrush(this.BackColor);
        //    }
        //    base.OnPaintBackground(e);
        //    vlblControlWidth = this.ClientSize.Width;
        //    vlblControlHeight = this.ClientSize.Height;
        //    e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
        //    e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);
        //}

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Focus();
        }
        #endregion
    }

    public class WaitingPanelEx
    {
        private static Dictionary<Control, WaitingPanelEx> _dic = new Dictionary<Control, WaitingPanelEx>();
        private ushort _refCount;
        public WaitingPanel Panel { get; private set; }

        public ushort RefCount
        {
            get { return _refCount; }
            private set
            {
                if (_refCount != value)
                {
                    _refCount = value;
                    if (_refCount <= 0)
                    {
                        Panel.Hide(Ctrl);
                        _dic.Remove(Ctrl);
                    }
                }
            }
        }

        public Control Ctrl { get; private set; }

        private WaitingPanelEx() { }
        public static WaitingPanelEx NewWithControl(Control control)
        {
            if (control == null) return null;

            WaitingPanelEx panelEx = null;
            if (_dic.ContainsKey(control))
            {
                panelEx = _dic[control];
            }
            else
            {
                panelEx = new WaitingPanelEx();
                panelEx.Ctrl = control;
                panelEx.Panel = new WaitingPanel();
            }

            return panelEx;
        }

        public void Show(string waitingMsg)
        {
            RefCount++;
            Panel.Show(Ctrl, waitingMsg);
        }

        public void Close()
        {
            RefCount--;
        }
    }
}
