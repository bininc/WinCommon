using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevSkin;

namespace DevControl
{
    public partial class UCYearSelector : UCBase
    {
        private int _minYear;
        private int _maxYear;

        public UCYearSelector()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                FillYear();
                EditValue = DateTime.Now.Year;
            }
        }

        public int MinYear
        {
            get { return _minYear; }
            set
            {
                if (_minYear != value)
                {
                    _minYear = value;
                    FillYear();
                }
            }
        }

        public int MaxYear
        {
            get { return _maxYear; }
            set
            {
                if (_maxYear != value)
                {
                    _maxYear = value;
                    FillYear();
                }
            }
        }

        private void FillYear()
        {
            List<Year> listYears = UCYearMonthRangeSelector.GetYearRange(_maxYear, _minYear);
            DSCommon.BindImageComboBox(cmbYear, listYears, x => true, "description", "year");
        }

        protected override void OnFirstLoad()
        {


            base.OnFirstLoad();
        }

        public override object EditValue
        {
            get { return cmbYear.EditValue; }
            set { cmbYear.EditValue = value; }
        }

        public int Year
        {
            get { return Convert.ToInt32(EditValue); }
            set { EditValue = value; }
        }
    }
}
