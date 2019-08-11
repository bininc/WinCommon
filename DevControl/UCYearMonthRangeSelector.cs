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
    public partial class UCYearMonthRangeSelector : UCBase
    {
        public UCYearMonthRangeSelector()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                List<Year> listYears = GetYearRange();
                DSCommon.BindImageComboBox(cmbBeginYear, listYears, x => true, "description", "year");
                DSCommon.BindImageComboBox(cmbEndYear, listYears, y => true, "description", "year");
                List<Month> listMonths = GetMonthRange();
                DSCommon.BindImageComboBox(cmbBeginMonth, listMonths, m => true, "description", "month");
                DSCommon.BindImageComboBox(cmbEndMonth, listMonths, m => true, "description", "month");
                BeginDate = DateTimeHelper.DefaultDateTime;
                EndDate = DateTime.Now;
            }
        }

        public DateTime BeginDate
        {
            get
            {
                if (cmbBeginYear.EditValue == null || cmbBeginMonth.EditValue == null) return DateTimeHelper.DefaultDateTime;
                int year = (int)cmbBeginYear.EditValue;
                int month = (int)cmbBeginMonth.EditValue;
                return new DateTime(year, month, 1);
            }
            set
            {
                if (DesignMode) return;
                cmbBeginYear.EditValue = value.Year;
                cmbBeginMonth.EditValue = value.Month;
            }
        }

        public DateTime EndDate
        {
            get
            {
                if (cmbEndYear.EditValue == null || cmbEndMonth.EditValue == null) return DateTimeHelper.MonthEnd;

                int year = (int)cmbEndYear.EditValue;
                int month = (int)cmbEndMonth.EditValue;
                return new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999);
            }
            set
            {
                if (DesignMode) return;
                cmbEndYear.EditValue = value.Year;
                cmbEndMonth.EditValue = value.Month;
            }
        }


        public override object EditValue
        {
            get
            {
                if (DesignMode) return null;

                if (BeginDate == DateTimeHelper.DefaultDateTime && EndDate == DateTimeHelper.MonthEnd) return null;

                return BeginDate.ToTimestamp() + "-" + EndDate.ToTimestamp();
            }
            set
            {
                if (DesignMode) return;

                if (value != null && value.ToString().Contains("-"))
                {
                    string[] array = value.ToString().Split('-');
                    ulong beginstamp = Convert.ToUInt64(array[0]);
                    ulong endstamp = Convert.ToUInt64(array[1]);
                    BeginDate = DateTimeHelper.TimestampToDateTime(beginstamp);
                    EndDate = DateTimeHelper.TimestampToDateTime(endstamp);
                }
                if (value == null)
                {
                    BeginDate = DateTimeHelper.DefaultDateTime;
                    EndDate = DateTime.Now;
                }
            }
        }

        public static List<Year> GetYearRange(int maxYear = 0, int minYear = 0)
        {
            if (maxYear == 0) maxYear = DateTime.Now.Year;
            if (minYear == 0) minYear = 1970;

            List<Year> list = new List<Year>();
            for (int i = minYear; i <= maxYear; i++)
            {
                list.Add(new Year() { year = i, description = i + "年" });
            }
            return list;
        }

        public static List<Month> GetMonthRange()
        {
            List<Month> list = new List<Month>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(new Month() { month = i, description = i + "月" });
            }
            return list;
        }

        private void cmbBeginYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class Year
    {
        public int year { get; set; }
        public string description { get; set; }
    }

    public class Month
    {
        public int month { get; set; }
        public string description { get; set; }
    }
}
