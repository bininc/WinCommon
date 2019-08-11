using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevSkin
{
    public class XPanel : Panel
    {
        #region Constructors
        public XPanel() : base()
        {
            setStyles();    //解决闪烁问题
        }
        #endregion

        #region Private Methods
        private void setStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }
        #endregion

    }
}
