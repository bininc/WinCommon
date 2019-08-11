using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DevSkin.wpf
{
    /// <summary>
    /// GifImage.xaml 的交互逻辑
    /// </summary>
    public partial class GifImage : UserControl
    {
        public GifImage()
        {
            DataContext = this;
            InitializeComponent();
        }

        public static readonly DependencyProperty RepeatBehaviorProperty = DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(GifImage), new PropertyMetadata(RepeatBehavior.Forever));

        public RepeatBehavior RepeatBehavior
        {
            get { return (RepeatBehavior) GetValue(RepeatBehaviorProperty); }
            set { SetValue(RepeatBehaviorProperty, value); }
        }

        public static readonly DependencyProperty ImgSourceProperty = DependencyProperty.Register("ImgSource", typeof(Uri), typeof(GifImage), new PropertyMetadata(default(Uri)));

        public Uri ImgSource
        {
            get { return (Uri)GetValue(ImgSourceProperty); }
            set { SetValue(ImgSourceProperty, value); }
        }
    }
}
