using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Xpf.Core;
using System.Windows.Interop;
using System.Windows.Media;

namespace DevSkin.wpf
{
    public class DevWindowBase : DXWindow
    {
        protected WindowInteropHelper _interopHelper;
        protected IntPtr Handle => _interopHelper.Handle;
        public bool isClosed { get; private set; }
        public DevWindowBase()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SizeToContent = SizeToContent.WidthAndHeight;
            TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
            TextOptions.SetTextRenderingMode(this, TextRenderingMode.ClearType);
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (!DesignerProperties.GetIsInDesignMode(this))
                _interopHelper = new WindowInteropHelper(this);
            OnLoad(e);
        }

        protected virtual void OnLoad(EventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            isClosed = !e.Cancel;
        }
    }
}
