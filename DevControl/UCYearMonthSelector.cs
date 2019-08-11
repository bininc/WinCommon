using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommLiby;
using DevSkin;

namespace DevControl
{
    public partial class UCYearMonthSelector : UCBase
    {
        public event EventHandler EditValueChanged;
        public UCYearMonthSelector()
        {
            InitializeComponent();
            cmbBeginYear.SelectedValueChanged += CmbBeginYear_SelectedValueChanged;
            cmbBeginMonth.SelectedValueChanged += CmbBeginYear_SelectedValueChanged;
        }

        public DateTime DefaultTime { get; set; }

        private void CmbBeginYear_SelectedValueChanged(object sender, EventArgs e)
        {
            DateTime newTime = GetSelectDateTime();
            if (newTime != _dateTime)
            {
                _dateTime = newTime;
                EditValueChanged?.Invoke(this, e);
            }
        }

        /// <summary>
        /// 显示更多日期
        /// </summary>
        public bool ShowMoreDate { get; set; }

        protected override void OnFirstLoad()
        {
            base.OnFirstLoad();
            if (!DesignMode)
            {
                List<Year> listYears = UCYearMonthRangeSelector.GetYearRange(ShowMoreDate ? DateTime.Now.Year + 50 : 0);
                DSCommon.BindImageComboBox(cmbBeginYear, listYears, x => true, "description", "year");

                List<Month> listMonths = UCYearMonthRangeSelector.GetMonthRange();
                DSCommon.BindImageComboBox(cmbBeginMonth, listMonths, m => true, "description", "month");

                if (DefaultTime != default(DateTime))
                    DateTime = DefaultTime;
                else
                    DateTime = DateTime.Now;
            }
        }

        private DateTime GetSelectDateTime()
        {
            if (cmbBeginYear.EditValue == null || cmbBeginMonth.EditValue == null) return DateTimeHelper.DefaultDateTime;
            return new DateTime((int)cmbBeginYear.EditValue, (int)cmbBeginMonth.EditValue, 1);
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get
            {
                DateTime newTime = GetSelectDateTime();
                if (newTime != _dateTime)
                    _dateTime = newTime;
                return _dateTime;
            }
            set
            {
                cmbBeginYear.EditValue = value.Year;
                cmbBeginMonth.EditValue = value.Month;
                _dateTime = value;
            }
        }

        public override object EditValue
        {
            get
            {
                if (DateTime == DateTimeHelper.DefaultDateTime) return null;
                return DateTime;
            }
            set
            {
                if (value == null) DateTime = DateTimeHelper.DefaultDateTime;
                if (value is DateTime)
                {
                    DateTime = (DateTime)value;
                }
                else
                {
                    DateTime = DateTimeHelper.DefaultDateTime;
                }
            }
        }
    }
}
