using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DevSkin.wpf
{
    public class DevWindowBox : DevWindowBase
    {
        public DevWindowBox()
        {
            ResizeMode = System.Windows.ResizeMode.NoResize;
            ShowIcon = false;
        }

        public bool? ShowDialog(Window owner)
        {
            if (owner != null)
                this.Owner = owner;
            return ShowDialog();
        }
    }
}
