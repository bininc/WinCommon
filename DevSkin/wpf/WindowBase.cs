using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace DevSkin.wpf
{
    public class WindowBase : Window
    {
        protected WindowInteropHelper _interopHelper;
        public WindowBase() : base()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Loaded += WindowBase_Loaded;
            }
        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            _interopHelper = new WindowInteropHelper(this);
            OnLoad(e);
        }

        public IntPtr Handle => _interopHelper.Handle;

        protected virtual void OnLoad(RoutedEventArgs e)
        {

        }
    }
}
