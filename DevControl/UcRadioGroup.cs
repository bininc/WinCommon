using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommLiby;
using DevSkin;
using Newtonsoft.Json;

namespace DevControl
{

    /// <summary>
    /// ���ݸ���ί��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SelectedChangedHandler(object sender, EventArgs e);

    public partial class UcRadioGroup<T> : UCBase
    {

        #region �¼�
        /// <summary>
        /// ѡ����ı��¼�
        /// </summary>
        public event SelectedChangedHandler SelectedChanged = null;
        #endregion

        #region ���캯��
        public UcRadioGroup(string radioOptionsJsonStr, string valueJsonStr)
        {
            InitializeComponent();
            AutoSize = true;
            Dictionary<string, T> dic = null;
            object value = null;
            Dictionary<string, T> dicValue = null;
            if (!string.IsNullOrWhiteSpace(radioOptionsJsonStr))
                dic = JsonConvert.DeserializeObject<Dictionary<string, T>>(radioOptionsJsonStr);
            if (!string.IsNullOrWhiteSpace(valueJsonStr))
                value = CommLiby.Common_liby.GetValueFromJson<T>(valueJsonStr);

            if (dic != null)
                foreach (var item in dic)
                {
                    RadioButton rb = new RadioButton();
                    rb.AutoSize = true;
                    rb.Text = item.Key;
                    rb.Tag = item.Value;
                    if (item.Value.Equals(value))   //��ֵ�û�Ĭ��ѡ��
                        rb.Checked = true;
                    rb.CheckedChanged += (x, y) =>
                    {
                        if (SelectedChanged != null)
                            SelectedChanged(this, y);
                    };
                    flowLayoutPanel1.Controls.Add(rb);

                    if (typeof(T) == typeof(DateTime))    //����ģʽ
                    {
                        if (item.Value.Equals(new DateTime(9999, 12, 31)))   //ѡ������
                        {
                            DateTimePicker timePicker = new DateTimePicker();
                            timePicker.CustomFormat = "yyyy��MM��dd��";
                            timePicker.Format = DateTimePickerFormat.Custom;
                            timePicker.Width = 129;
                            timePicker.Margin = new Padding(0, 2, 20, 0);
                            timePicker.ValueChanged += (x, y) => { rb.Tag = timePicker.Value; };
                            if (value != null && (DateTime)value != default(DateTime))
                            {
                                rb.Tag = value;
                                rb.Checked = true;
                                timePicker.Value = (DateTime)rb.Tag;
                            }
                            else rb.Tag = timePicker.Value;
                            flowLayoutPanel1.Controls.Add(timePicker);
                        }
                    }
                }

        }
        #endregion

        #region ����
        /// <summary>
        /// ��ǰѡ�е�ֵ
        /// </summary>
        public T Value
        {
            get
            {
                return GetSelectRadioIndexValue<T>();
            }
            set
            {
                SetSelectRadio(value);
            }
        }

        public void SetSelectRadio(object value)
        {
            foreach (System.Windows.Forms.Control radioCol in flowLayoutPanel1.Controls)
            {
                if (radioCol is RadioButton)
                {
                    RadioButton rb = radioCol as RadioButton;
                    if (rb.Tag != null && rb.Tag.Equals(value))
                    {
                        rb.Checked = true;
                    }
                    else
                    {
                        rb.Checked = false;
                    }
                }
            }
        }

        private T GetSelectRadioIndexValue<T>()
        {
            foreach (System.Windows.Forms.Control rg in flowLayoutPanel1.Controls)
            {
                if (rg is RadioButton)
                {
                    RadioButton rb = rg as RadioButton;
                    if (rb.Checked)
                    {
                        if (rb.Tag != null)
                            return (T)rb.Tag;
                    }
                }
            }
            if (typeof(T) == typeof(float)) //float����
            {
                object f = -1;
                return (T)f;
            }
            else
                return default(T);
        }
        #endregion
    }
}
