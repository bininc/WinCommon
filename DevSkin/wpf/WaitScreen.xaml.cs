using System.Windows;
using System.Windows.Controls;

namespace DevSkin.wpf
{
    public partial class WaitScreen : UserControl
    {
        public WaitScreen()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty LoadingMsgProperty = DependencyProperty.Register(
            "LoadingMsg", typeof(string), typeof(WaitScreen), new PropertyMetadata("数据加载中..."));

        public string LoadingMsg
        {
            get { return (string)GetValue(LoadingMsgProperty); }
            set { SetValue(LoadingMsgProperty, value); }
        }
    }
}
