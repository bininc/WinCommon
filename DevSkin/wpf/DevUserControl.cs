using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace DevSkin.wpf
{
    public class DevUserControl : UserControl
    {
        protected HwndSource hwndSource;
        private Window _parentWindow;
        private bool _force;
        string _title;
        protected bool _loaded = false;
        private bool _loadedEvent = false;

        public bool Actived { get; set; }

        public DevUserControl()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.IsVisibleChanged += DevUserControl_IsVisibleChanged;
                Unloaded += DevUserControl_Unloaded;
                Loaded += DevUserControl_Loaded;
            }
        }

        private void DevUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_loadedEvent)
            {
                if (IsVisible && !_loaded)
                {
                    OnLoad(e);
                    _loaded = true;
                }
                _loadedEvent = true;
            }
        }

        private void DevUserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible && _loadedEvent)
            {
                if (!_loaded)
                {
                    OnLoad(new RoutedEventArgs());
                    _loaded = true;
                }
            }
        }

        protected Window ParentWindow
        {
            get { return _parentWindow; }
            set
            {
                if (_parentWindow != value)
                {
                    _parentWindow = value;
                    _parentWindow.Closing += _parentWindow_Closing;
                    _parentWindow.Closed += _parentWindow_Closed;
                    _parentWindow.ContentRendered += _parentWindow_Loaded;
                }
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                OnTitleChanged();
            }
        }

        private void _parentWindow_Loaded(object sender, EventArgs e)
        {
            hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }

        private void _parentWindow_Closed(object sender, EventArgs e)
        {
            Window win = (Window)sender;
            win.Content = null;
            OnFormClosed();
        }

        private void _parentWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_force)
                e.Cancel = !OnFormClosing();
            _force = false;
        }

        protected virtual bool OnFormClosing()
        {
            return true;
        }

        protected virtual void OnFormClosed() { }

        public virtual IntPtr Handle
        {
            get
            {
                if (hwndSource == null)
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        hwndSource = (HwndSource)PresentationSource.FromVisual(this);
                    }));

                return hwndSource.Handle;
            }
        }

        protected virtual void OnLoad(RoutedEventArgs e)
        {

        }

        private void DevUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            OnUnLoad(e);
            _loaded = false;
        }

        protected virtual void OnUnLoad(RoutedEventArgs e)
        {

        }

        private Window FindParentWindow(FrameworkElement ctrl)
        {
            if (ctrl != null && ctrl.Parent != null)
            {
                if (ctrl.Parent is Window)
                {
                    return (Window)ctrl.Parent;
                }
                else
                {
                    if (ctrl.Parent is FrameworkElement)
                        return FindParentWindow((FrameworkElement)ctrl.Parent);
                }
            }
            return null;
        }

        public virtual void OnParentChanged()
        {
            if (ParentWindow == null)
            {
                ParentWindow = FindParentWindow(this);
            }
        }

        public bool? ShowDialog(Window owner = null)
        {
            DevWindowBox window = new DevWindowBox();
            if (owner != null)
                window.Owner = owner;
            window.Content = this;
            if (!string.IsNullOrEmpty(_title))
                window.Title = _title;
            ParentWindow = window;

            return window.ShowDialog();
        }

        public bool? ShowPanel(Window owner = null)
        {
            DevWindowBox window = new DevWindowBox();
            window.WindowStyle = WindowStyle.None;
            window.Background = null;
            window.BorderThickness = new Thickness(0, 0, 0, 0);
            window.Topmost = true;
            if (owner != null)
                window.Owner = owner;        
            window.Content = this;
            if (!string.IsNullOrEmpty(_title))
                window.Title = _title;
            ParentWindow = window;

            return window.ShowDialog();
        }

        public void Show(Window owner = null, bool noMaxBtn = false)
        {
            DevWindowBase window = null;
            if (ParentWindow != null)
            {
                window = ParentWindow as DevWindowBase;
                if (window.isClosed)
                    window = null;
            }
            if (window == null)
            {
                window = new DevWindowBase();
                ParentWindow = window;
            }
            if (owner != null)
                window.Owner = owner;
            window.Content = this;
            if (!string.IsNullOrEmpty(_title))
                window.Title = _title;

            if (noMaxBtn)
            {
                window.ResizeMode = ResizeMode.CanMinimize;
            }

            if (window.IsLoaded)
            {
                window.WindowState = WindowState.Normal;
                window.Activate();
            }
            else
                window.Show();
        }

        protected virtual void OnTitleChanged()
        {

        }

        public bool? DialogResult
        {
            get
            {
                if (_parentWindow == null) return null;
                return _parentWindow.DialogResult;
            }
            set
            {
                if (_parentWindow != null)
                    _parentWindow.DialogResult = value;
            }
        }
    }
}
