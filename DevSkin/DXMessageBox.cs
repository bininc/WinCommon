using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars.Alerter;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;

namespace DevSkin
{
    public static class DXMessageBox
    {
        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        public static event EventHandler BtnOkClick; //确认按钮事件
        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        public static event EventHandler BtnCancelClick; //取消按钮事件
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        public static event EventHandler BtnClick;//按钮点击事件
        /// <summary>
        /// 点击内容是事件
        /// </summary>
        public static AlertClickEventHandler AlertClick;//点击内容事件
        /// <summary>
        /// AlertForm关闭事件
        /// </summary>
        public static AlertFormClosingEventHandler AlertClosing;
        /// <summary>
        /// AlertControl
        /// </summary>
        public static AlertControl alertctrl { get; private set; }

        /// <summary>
        /// 关闭所有alertcontrls
        /// </summary>
        public static void CloseAlertForms()
        {
            if (alertctrl == null) return;
            alertctrl.AlertFormList.ForEach(f =>
            {
                alertctrl.RaiseFormClosing(new AlertFormClosingEventArgs(f, AlertFormCloseReason.UserClosing));
            });
        }

        /// <summary>
        /// 显示消息窗
        /// </summary>
        /// <param name="state"></param>
        private static DialogResult ShowWindow(object state)
        {
            DialogResult dr = DialogResult.Cancel;
            if (!(state is object[])) return dr;   //排除异常参数9
            object[] param = state as object[];
            string msg = param[0] == null ? null : param[0].ToString();
            string title = param[1] == null ? "温馨提示" : param[1].ToString();
            MessageIcon icon = (MessageIcon)param[2];
            MessageButton btn = (MessageButton)param[3];
            IWin32Window owner = param[4] as IWin32Window;
            bool flow = (bool)param[5];
            int autoCloseSec = (int)param[6];

            ISupportLookAndFeel iLookAndFeel = owner as ISupportLookAndFeel;
            string skinName = null;
            if (iLookAndFeel != null)
            {
                UserLookAndFeel lookAndFeel = iLookAndFeel.LookAndFeel;
                skinName = lookAndFeel.UseDefaultLookAndFeel
                    ? UserLookAndFeel.Default.SkinName
                    : lookAndFeel.SkinName;
            }
            if (flow)
            {
                if (alertctrl == null)
                {
                    alertctrl = new AlertControl();
                    alertctrl.ShowPinButton = true;
                    alertctrl.ShowCloseButton = true;
                    alertctrl.ShowToolTips = true;
                    alertctrl.FormShowingEffect = AlertFormShowingEffect.SlideHorizontal;
                    alertctrl.FormLocation = AlertFormLocation.BottomRight;
                    alertctrl.FormDisplaySpeed = AlertFormDisplaySpeed.Moderate;
                    alertctrl.AppearanceText.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    alertctrl.AppearanceText.Options.UseFont = true;
                    alertctrl.AppearanceHotTrackedText.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    alertctrl.AppearanceHotTrackedText.Options.UseFont = true;
                    alertctrl.AppearanceHotTrackedText.ForeColor = Color.DodgerBlue;
                    alertctrl.AppearanceHotTrackedText.Options.UseForeColor = true;
                    alertctrl.AutoHeight = true;
                    alertctrl.AppearanceCaption.TextOptions.HAlignment = HorzAlignment.Center;
                    alertctrl.AppearanceCaption.TextOptions.VAlignment = VertAlignment.Bottom;
                    alertctrl.AppearanceCaption.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    alertctrl.AppearanceCaption.Options.UseTextOptions = true;
                }
                alertctrl.AutoFormDelay = autoCloseSec * 1000;

                if (skinName != null && skinName != alertctrl.LookAndFeel.SkinName)
                    alertctrl.LookAndFeel.SetSkinStyle(skinName);

                alertctrl.AlertClick += AlertClick;
                alertctrl.FormClosing += AlertClosing;
                AlertClick = null;
                AlertClosing = null;
                alertctrl.Show(FormMessage.Instance, "[" + title + "]", msg + "\n\n");
                return DialogResult.OK;
            }
            else
            {
                if (skinName != null && skinName != FormMessage.Instance.LookAndFeel.SkinName)
                    FormMessage.Instance.LookAndFeel.SetSkinStyle(skinName);

                if (BtnClick != null)
                {
                    FormMessage.Instance.BtnOkClick = BtnClick;
                    FormMessage.Instance.BtnCancelClick = BtnClick;
                }
                else
                {
                    FormMessage.Instance.BtnOkClick = BtnOkClick;
                    FormMessage.Instance.BtnCancelClick = BtnCancelClick;
                }
                BtnOkClick = null;
                BtnCancelClick = null;
                BtnClick = null;

                dr = FormMessage.Instance.ShowDialog(msg, title, icon, btn, owner);
                return dr;
            }
        }


        /// <summary>
        /// 消息提示框
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="title">消息标题</param>
        public static DialogResult Show(string msg, string title, MessageIcon icon, MessageButton btn, bool flow = false, int closeSec = 10, IWin32Window owner = null)
        {
            DialogResult dr = DialogResult.Cancel;
            if (DSCommon.SyncContext != null && DSCommon.SyncContext != SynchronizationContext.Current)
                DSCommon.SyncContext.Send(x => { dr = ShowWindow(x); }, new object[] { msg, title, icon, btn, owner, flow, closeSec }); //抛到UI线程执行
            else
                return ShowWindow(new object[] { msg, title, icon, btn, owner, flow, closeSec });
            return dr;
        }

        public static DialogResult Show(string msg, string title, MessageIcon icon, bool flow = false, int closeSec = 10, IWin32Window owner = null)
        {
            return Show(msg, title, icon, MessageButton.None, flow, closeSec, owner);
        }
        public static DialogResult Show(string msg, string title, MessageButton btn, bool flow = false, int closeSec = 10, IWin32Window owner = null)
        {
            return Show(msg, title, MessageIcon.None, btn, flow, closeSec, owner);
        }
        public static DialogResult Show(string msg, string title, bool flow = false, int closeSec = 10, IWin32Window owner = null)
        {
            return Show(msg, title, MessageIcon.None, flow, closeSec, owner);
        }

        public static DialogResult Show(string msg, MessageIcon icon, MessageButton btn, bool flow = false, int closeSec = 10, IWin32Window owner = null)
        {
            return Show(msg, null, icon, btn, flow, closeSec, owner);
        }
        public static DialogResult Show(string msg, MessageIcon icon, bool flow = false, int closeSec = 10, IWin32Window owner = null)
        {
            return Show(msg, icon, MessageButton.None, flow, closeSec, owner);//调用另一个方法处理
        }
        public static DialogResult Show(string msg, MessageButton btn, bool flow = false, int closeSec = 10, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.None, btn, flow, closeSec, owner);
        }
        public static DialogResult Show(string msg, bool flow = false, int closeSec = 10, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.None, flow, closeSec, owner);
        }
        public static DialogResult ShowSuccess(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Check, MessageButton.Ok, false, 10, owner);
        }
        public static DialogResult ShowInfo(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Info, MessageButton.Ok, false, 10, owner);
        }
        public static DialogResult ShowError(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Error, MessageButton.Ok, false, 10, owner);
        }
        public static DialogResult ShowWarning(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Warning, MessageButton.Ok, false, 10, owner);
        }
        public static DialogResult ShowWarning2(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Warning2, MessageButton.Ok, false, 10, owner);
        }
        public static DialogResult ShowQuestion(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Question, MessageButton.OkCancel, false, 10, owner);
        }

        public static void ShowDefaultWaitFrom(Form parentForm, string caption = null, string description = null)
        {
            if (caption == null) caption = "请稍后...";
            if (description == null) description = "数据加载中...";
            SplashScreenManager.SkinName = "Sharp";
            SplashScreenManager.ShowDefaultWaitForm(parentForm, false, false, caption, description);
        }

        public static void CloseDefaultWaitForm()
        {
            SplashScreenManager.CloseDefaultWaitForm();
        }
    }
}
