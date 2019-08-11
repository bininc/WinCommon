using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using DevSkin;

namespace DevControl
{
    public partial class UCTreeListSelector<T> : UCBase where T : class
    {
        private bool allowCheckBox = false; //是否显示选择框
        private PopupContainerEdit popupcEdit;//需要绑定的控件
        Dictionary<object, object> dicValues = null;
        public object unSelectedid = null;  //不能选择的节点ID
        private bool _specialMode;  //特殊模式（选中子节点父节点就选中）

        /// <summary>
        /// 需要绑定的控件
        /// </summary>
        public PopupContainerEdit PopupcEdit
        {
            get { return popupcEdit; }
            set
            {
                popupcEdit = value;
                if (popupcEdit != null)
                {
                    PopupContainerControl pcc = new PopupContainerControl();
                    pcc.Controls.Add(this);
                    this.Dock = DockStyle.Fill;
                    pcc.Size = new Size(popupcEdit.Width, 200);
                    popupcEdit.Properties.PopupControl = pcc;
                    popupcEdit.Closed += popupcEdit_Closed;
                }
            }
        }

        void popupcEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (popupcEdit != null)
            {
                popupcEdit.Text = GetText();
                popupcEdit.Tag = GetEditValue();
            }
            dicValues = null;
        }


        public UCTreeListSelector(bool specialMode = true)
        {
            _specialMode = specialMode;
            InitializeComponent();
        }

        public void InitData(PopupContainerEdit popupcEdit, IEnumerable<T> dataSource, string keyFieldName, string parentFieldName, string previewFieldName, bool showCheckBox = false)
        {
            PopupcEdit = popupcEdit;
            allowCheckBox = showCheckBox;
            DSCommon.SetTreeList(treeList1, showCheckBox);
            if (showCheckBox)
                treeList1.AfterCheckNode += treeList1_AfterCheckNode;
            else
            {
                treeList1.FocusedNodeChanged += treeList1_FocusedNodeChanged;
                //treeList1.AfterFocusNode += treeList1_AfterFocusNode;
            }

            treeList1.KeyFieldName = keyFieldName;
            treeList1.ParentFieldName = parentFieldName;
            treeList1.PreviewFieldName = previewFieldName;
            if (!string.IsNullOrWhiteSpace(previewFieldName))
            {
                TreeListColumn tc = new TreeListColumn { Name = previewFieldName, FieldName = previewFieldName, VisibleIndex = 0 };
                tc.OptionsColumn.AllowEdit = false;
                treeList1.Columns.Add(tc);
            }
            treeList1.DataSource = dataSource;
            treeList1.ExpandAll();
        }

        public T SelectedData
        {
            get
            {
                T data = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as T;
                return data;
            }
        }

        void treeList1_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            popupcEdit.ClosePopup();
        }

        void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.OldNode != null)
            {
                T data = treeList1.GetDataRecordByNode(e.Node) as T;
                PropertyInfo fieldInfo = data?.GetType().GetProperty(treeList1.KeyFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                object newid = fieldInfo?.GetValue(data, null);
                T data2 = treeList1.GetDataRecordByNode(e.OldNode) as T;
                object oldid = fieldInfo?.GetValue(data2, null);
                if (newid == unSelectedid || newid?.ToString() == unSelectedid?.ToString())
                {
                    SetChecked(oldid);
                }
            }
            if (e.OldNode == null)
                popupcEdit_Closed(null, null);
            else
                popupcEdit.ClosePopup();
        }

        void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if(setCheckeding) return;DSCommon.SetTreeListCheckedChildNodes(e.Node, e.Node.CheckState);
            DSCommon.SetTreeListCheckedParentNodes(e.Node, e.Node.CheckState, _specialMode);
        }

        public string GetEditValue()
        {
            if (allowCheckBox)
            {
                if (dicValues == null)
                {
                    dicValues = DSCommon.GetTreeListCheckedKeyValue<T>(treeList1);
                }
                StringBuilder sb = new StringBuilder();
                int i = 1;
                foreach (object item in dicValues.Keys)
                {
                    if (item != null)
                    {
                        sb.AppendFormat("{0}", item);
                    }
                    if (i < dicValues.Count)
                    {
                        sb.Append(",");
                    }
                    i++;
                }
                return sb.ToString();
            }
            else
            {
                T data = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as T;
                if (data != null)
                {
                    PropertyInfo propertyInfo = data.GetType().GetProperty(treeList1.KeyFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    object val = propertyInfo.GetValue(data, null);
                    return val?.ToString();
                }
            }
            return null;
        }

        public string GetText()
        {
            if (allowCheckBox)
            {
                if (dicValues == null)
                {
                    dicValues = DSCommon.GetTreeListCheckedKeyValue<T>(treeList1);
                }
                StringBuilder sb = new StringBuilder();
                int i = 1;
                foreach (object item in dicValues.Values)
                {
                    if (item != null)
                    {
                        sb.AppendFormat("{0}", item);
                    }
                    if (i < dicValues.Count)
                    {
                        sb.Append(",");
                    }
                    i++;
                }
                return sb.ToString();
            }
            else
            {
                T data = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as T;
                if (data != null)
                {
                    PropertyInfo propertyInfo = data.GetType().GetProperty(treeList1.PreviewFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    object name = propertyInfo.GetValue(data, null);
                    return name?.ToString();
                }

            }

            return null;
        }

        private bool setCheckeding = false;
        /// <summary>
        /// 设置状态选中
        /// </summary>
        /// <param name="key"></param>
        public void SetChecked(object key, bool isFocus = true)
        {
            setCheckeding = true;
            string keyStr = key as string;
            if (allowCheckBox)
                isFocus = false;

            {
                if (keyStr == null)
                {
                    DSCommon.SetTreeListNodeChecked<T>(treeList1, key, isFocus);
                }
                else
                {
                    string[] keys = keyStr.Split(',');
                    for (int i = 0; i < keys.Length; i++)
                    {
                        DSCommon.SetTreeListNodeChecked<T>(treeList1, keys[i], isFocus);
                    }
                }
            }
            //else
            {

            }
            popupcEdit_Closed(null, null);
            setCheckeding = false;
        }

    }
}
