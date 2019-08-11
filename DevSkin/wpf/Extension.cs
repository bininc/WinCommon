using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DevSkin.wpf
{
    public static class Extension
    {
        /// <summary>
        /// 将元素置于最前
        /// </summary>
        /// <param name="element"></param>
        public static void BringToFront(this FrameworkElement element)
        {
            if (element == null) return;

            Canvas parent = element.Parent as Canvas;
            if (parent == null) return;

            var childrens = parent.Children.OfType<UIElement>().Where(x => x != element);
            if (childrens.Any())
            {
                var maxZ = childrens.Select(Canvas.GetZIndex).Max();
                Canvas.SetZIndex(element, maxZ + 1);
            }
        }
    }
}
