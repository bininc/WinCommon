using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Brushes = System.Drawing.Brushes;

namespace DevSkin
{
    public static class DSCommon
    {

        public static string Default_skin_name = "HybridApp" /*"Office 2013";*//*"Office 2016 Colorful"*/; // 默认皮肤名字
        public static string Default_Wpf_skin_name = Theme.Office2007BlueName;

        private static SynchronizationContext _syncContext;
        /// <summary>
        /// 同步上下文
        /// </summary>
        public static SynchronizationContext SyncContext
        {
            get
            {
                //if (_syncContext == null)
                    //throw new Exception("对象为初始化，请调用DevSkin.DSCommon.Init()进行初始化操作");
                return _syncContext;
            }
            private set { _syncContext = value; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mainThreadContext">主线程</param>
        /// <param name="devSkinName">皮肤名称</param>
        public static void Init(SynchronizationContext mainThreadContext, string devSkinName = null)
        {
            if (mainThreadContext != null)
                SyncContext = mainThreadContext;
            if (devSkinName != null)
                SetSkin(devSkinName);
        }

        /// <summary>
        /// 跨线程访问控件(扩展方法)
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="method">执行的委托</param>
        public static void CrossThreadCalls(this Control ctrl, Action method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method)); //没有委托返回
            if (ctrl.IsHandleCreated)
            {
                if (ctrl.InvokeRequired)
                    ctrl.Invoke(method);
                else
                    method.Invoke();
            }
            else
            {
                ctrl.HandleCreated += (object sender, EventArgs e) =>
                {
                    if (ctrl.InvokeRequired)
                        ctrl.Invoke(method);
                    else
                        method.Invoke();
                };
            }


        }
        /// <summary>
        /// 跨线程访问控件 异步 (扩展方法)
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="method">执行的方法</param>
        /// <param name="longTimeMethod">需要长时间执行的方法</param>
        public static Task CrossThreadCallsAsync(this Control ctrl, Func<object> longTimeMethod, Action<object> method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method)); //没有委托返回
            try
            {
                Task task = new Task(() =>
                 {
                     object obj = null;
                     if (longTimeMethod != null)
                         obj = longTimeMethod();
                     if (ctrl.IsHandleCreated)
                         ctrl.Invoke(method, obj);
                     else
                     {
                         ctrl.HandleCreated += (object sender, EventArgs e) =>
                         {
                             ctrl.Invoke(method, obj);
                         };
                     }
                 });
                task.Start();
                return task;
            }
            catch (Exception ex)
            {
                throw new Exception("CrossThreadCallsSync异常", ex);
            }
        }

        /// <summary>
        /// 设置皮肤
        /// </summary>
        /// <param name="skinName">皮肤名字</param>
        public static void SetSkin(string skinName = null)
        {
            if (string.IsNullOrWhiteSpace(skinName))
            {
                skinName = Default_skin_name;
            }
            UserLookAndFeel.Default.SkinName = skinName;
        }

        public static void SetSkin(this XtraForm form, string skinName)
        {
            if (string.IsNullOrWhiteSpace(skinName)) return;
            form.LookAndFeel.SetSkinStyle(skinName);
        }

        /// <summary>
        /// 显示等待Panel
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="getDataMethod"></param>
        /// <param name="getDataCompleteMethod"></param>
        /// <param name="waitingMsg"></param>
        public static void ShowWaitingPanel(this Control ctrl, Func<object> getDataMethod, Action<object> getDataCompleteMethod, string waitingMsg = "数据加载中")
        {
            if (ctrl == null) return;
            WaitingPanel.ShowPanel(ctrl, getDataMethod, getDataCompleteMethod, waitingMsg);
        }

        /// <summary>
        /// 显示等待Panel
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="waitingMsg"></param>
        public static void ShowWaitingPanel(this Control ctrl, string waitingMsg = "数据加载中")
        {
            ShowWaitingPanel(ctrl, null, null, waitingMsg);
        }

        /// <summary>
        /// 隐藏等待Panel
        /// </summary>
        /// <param name="ctrl"></param>
        public static void HideWaitingPanel(this Control ctrl)
        {
            if (ctrl == null) return;
            WaitingPanel.HidePanel(ctrl);
        }

        /// <summary>
        /// 设置GridContrl相关属性
        /// </summary>
        /// <param name="gridControl">GridControl控件</param>
        /// <param name="emptyStr">空数据提示串</param>
        /// <param name="startRow">开始显示行</param>
        public static void SetGridControl(GridControl gridControl, string emptyStr = "没有数据", int startRow = 0)
        {
            if (emptyStr == null) emptyStr = "没有数据";
            if (gridControl == null || gridControl.MainView == null) return;

            GridView mainView = (GridView)gridControl.MainView;
            mainView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;  //选中整行
            mainView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;   //选中整行
            mainView.OptionsSelection.EnableAppearanceFocusedCell = false;  //禁止选中列
            mainView.OptionsBehavior.Editable = false;  //禁止编辑
            mainView.OptionsView.ShowGroupPanel = false;    //禁止分组面板
            mainView.OptionsView.ColumnAutoWidth = false;   //显示横向滚动条
            mainView.OptionsCustomization.AllowFilter = false;  //禁止过滤
            mainView.OptionsMenu.EnableColumnMenu = false;  //禁用列右键菜单
            mainView.OptionsCustomization.AllowQuickHideColumns = false;    //禁止隐藏列
            //mainView.OptionsCustomization.AllowColumnMoving = false;    //列头禁止移动
            //mainView.OptionsCustomization.AllowSort = false;    //列头禁止排序
            //mainView.OptionsCustomization.AllowColumnResizing = false;    //禁止各列头改变列宽
            // 无数据时显示
            mainView.CustomDrawEmptyForeground += (object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e) =>
            {
                BindingSource bindingSource = gridControl.DataSource as BindingSource;
                if (bindingSource == null || bindingSource.Count == 0)
                {
                    string str = emptyStr;
                    Font f = new Font("宋体", 12, FontStyle.Bold);
                    Rectangle r = new Rectangle(e.Bounds.Left + 10, e.Bounds.Top + 30, e.Bounds.Right, e.Bounds.Height);
                    e.Graphics.DrawString(str, f, Brushes.Black, r);
                }
            };
            //显示行序号
            mainView.IndicatorWidth = 45;
            mainView.CustomDrawRowIndicator += (object sender, RowIndicatorCustomDrawEventArgs e) =>
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= startRow)
                {
                    e.Info.DisplayText = (e.RowHandle + 1 - startRow).ToString();
                }
            };
            mainView.Appearance.HeaderPanel.Options.UseTextOptions = true;  //设置标题样式
            mainView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //标题文本居中
            for (int i = 0; i < mainView.Columns.Count; i++)
            {
                var col = mainView.Columns[i];
                if (col.OptionsColumn.FixedWidth)   //固定列 居中显示
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
        }
        /// <summary>
        /// 设置列名和列绑定
        /// </summary>
        public static void SetGridControlColumnsBind(GridControl _gridControl, Dictionary<string, string> _columsBind)
        {
            if (_gridControl == null || _gridControl.MainView == null || _columsBind == null || _columsBind.Count <= 0) return;

            GridView mainView = (GridView)_gridControl.MainView;
            foreach (KeyValuePair<string, string> item in _columsBind)
            {
                GridColumn gcol = new GridColumn();
                gcol.Name = "gc" + item.Key;
                gcol.Caption = item.Value;
                gcol.FieldName = item.Key;
                gcol.Visible = true;
                mainView.Columns.Add(gcol);
            }
        }

        /// <summary>
        /// 绑定ComboBox数据项
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="source"></param>
        /// <param name="displayField"></param>
        /// <param name="valueField"></param>
        public static void BindImageComboBox<T>(ImageComboBoxEdit cmb, IEnumerable<T> source, Func<T, bool> filterExpression, string displayField, string valueField, bool showTipItem = false, object selectedVal = null)
        {
            if (source == null || !source.Any() || cmb == null || string.IsNullOrWhiteSpace(displayField) ||
                string.IsNullOrWhiteSpace(valueField))
            {
                cmb?.Properties.Items.Clear();
                return;
            }

            cmb.Properties.Items.Clear();
            if (showTipItem)
            {
                ImageComboBoxItem itemtemp = new ImageComboBoxItem();
                itemtemp.Value = null;
                itemtemp.Description = "请选择...";
                cmb.Properties.Items.Add(itemtemp);
            }
            IEnumerable<T> rows = filterExpression == null ? source : source.Where(filterExpression);

            Type type = typeof(T);
            PropertyInfo valuefieldInfo = type.GetProperty(valueField, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo displayfieldInfo = type.GetProperty(displayField, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            ImageComboBoxItem selectedItem = null;
            foreach (T row in rows)
            {
                ImageComboBoxItem itemtemp1 = new ImageComboBoxItem();
                itemtemp1.Value = valuefieldInfo.GetValue(row, null);

                if (selectedVal != null && (itemtemp1.Value == selectedVal || itemtemp1.Value?.ToString() == selectedVal.ToString()))
                {
                    selectedItem = itemtemp1;
                }

                if (displayfieldInfo.PropertyType.FullName == "System.String")
                    itemtemp1.Description = displayfieldInfo.GetValue(row, null)?.ToString().Trim();
                else
                    itemtemp1.Description = displayfieldInfo.GetValue(row, null)?.ToString();
                cmb.Properties.Items.Add(itemtemp1);
            }

            if (selectedItem != null)
            {
                cmb.SelectedItem = selectedItem;
            }
            else if (cmb.Properties.Items.Count > 0)
            {
                if (cmb.SelectedIndex != 0)
                    cmb.SelectedIndex = 0;
            }
            else
                cmb.SelectedIndex = -1;
        }
        /// <summary>
        /// 绑定ComboBox数据项
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="source"></param>
        /// <param name="displayField"></param>
        /// <param name="valueField"></param>
        public static void BindImageComboBox<T>(RepositoryItemImageComboBox cmb, IEnumerable<T> source, Func<T, bool> filterExpression, string displayField, string valueField)
        {
            if (source == null || !source.Any() || cmb == null || string.IsNullOrWhiteSpace(displayField) ||
                string.IsNullOrWhiteSpace(valueField))
            {
                cmb?.Items.Clear();
                return;
            }

            cmb.Items.Clear();

            IEnumerable<T> rows = source.Where(filterExpression);

            Type type = typeof(T);
            PropertyInfo valuefieldInfo = type.GetProperty(valueField, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo displayfieldInfo = type.GetProperty(displayField, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (T row in rows)
            {
                ImageComboBoxItem itemtemp1 = new ImageComboBoxItem();

                itemtemp1.Value = valuefieldInfo?.GetValue(row, null);
                itemtemp1.Description = displayfieldInfo?.GetValue(row, null)?.ToString();
                cmb.Items.Add(itemtemp1);
            }
        }

        /// <summary>
        /// 设置TreeList相关选项
        /// </summary>
        /// <param name="treelist"></param>
        public static void SetTreeList(TreeList treelist, bool allowCheckbox = false)
        {
            if (treelist == null) return;
            //treelist.LookAndFeel.UseDefaultLookAndFeel = false;
            //treelist.LookAndFeel.UseWindowsXPTheme = true;
            treelist.AllowDrop = false;
            treelist.OptionsBehavior.Editable = !allowCheckbox;
            treelist.OptionsView.AnimationType = DevExpress.XtraTreeList.TreeListAnimationType.AnimateAllContent;
            treelist.OptionsBehavior.KeepSelectedOnClick = true;
            treelist.OptionsBehavior.AutoPopulateColumns = false;
            treelist.OptionsSelection.EnableAppearanceFocusedCell = false;
            treelist.OptionsPrint.PrintTree = true;
            treelist.OptionsPrint.PrintTreeButtons = true;
            treelist.OptionsView.ShowCheckBoxes = allowCheckbox;
        }

        /// <summary>
        /// 选择某一节点时,该节点的子节点全部选择  取消某一节点时,该节点的子节点全部取消选择
        /// </summary>
        /// <param name="node"></param>
        /// <param name="state"></param>
        public static void SetTreeListCheckedChildNodes(TreeListNode node, CheckState checkState)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = checkState;
                SetTreeListCheckedChildNodes(node.Nodes[i], checkState);
            }
        }

        /// <summary>
        /// 某节点的子节点全部选择时,该节点选择   某节点的子节点未全部选择时,该节点不选择
        /// </summary>
        /// <param name="node"></param>
        /// <param name="checkState"></param>
        /// <param name="special">特殊模式（只要子节点有任何一个选中 父节点便选中）</param>
        public static void SetTreeListCheckedParentNodes(TreeListNode node, CheckState checkState, bool special = false)
        {
            if (node.ParentNode != null)
            {
                CheckState parentCheckState = node.ParentNode.CheckState;

                foreach (TreeListNode item in node.ParentNode.Nodes)
                {
                    if (special)
                    {
                        if (checkState == CheckState.Checked || item.CheckState == CheckState.Checked)   //当前节点选中或者父节点子节点中有任何一个选中 则父节点选中
                        {
                            parentCheckState = CheckState.Checked;
                            break;
                        }
                    }
                    else
                        if (!checkState.Equals(item.CheckState))//只要任意一个与其选中状态不一样即父节点状态不全选
                    {
                        parentCheckState = CheckState.Unchecked;
                        break;
                    }
                    parentCheckState = checkState;//否则（该节点的兄弟节点选中状态都相同），则父节点选中状态为该节点的选中状态
                }

                node.ParentNode.CheckState = parentCheckState;
                SetTreeListCheckedParentNodes(node.ParentNode, checkState, special);//遍历上级节点
            }
        }

        /// <summary>
        /// 获取选择状态的数据主键值集合
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="node">需要获取得节点</param>
        public static Dictionary<object, object> GetTreeListCheckedKeyValue<T>(TreeList tree, TreeListNode node) where T : class
        {
            Dictionary<object, object> list = new Dictionary<object, object>();
            if (node == null) return list;//递归终止

            if (node.CheckState == CheckState.Checked)
            {
                if (typeof(T) == typeof(DataTable))
                {
                    DataRowView drv = tree.GetDataRecordByNode(node) as DataRowView;
                    if (drv != null)
                    {
                        object name = drv[tree.PreviewFieldName];
                        object val = drv[tree.KeyFieldName];
                        list.Add(val, name);
                    }
                }
                else
                {
                    T data = tree.GetDataRecordByNode(node) as T;
                    if (data != null)
                    {
                        PropertyInfo propertyInfo = data.GetType().GetProperty(tree.PreviewFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        object name = propertyInfo.GetValue(data, null);
                        propertyInfo = data.GetType().GetProperty(tree.KeyFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        object val = propertyInfo.GetValue(data, null);
                        list.Add(val, name);
                    }
                }
            }
            foreach (TreeListNode item in node.Nodes)
            {
                if (item.CheckState == CheckState.Checked && !item.HasChildren)
                {
                    if (typeof(T) == typeof(DataTable))
                    {
                        DataRowView drv = tree.GetDataRecordByNode(item) as DataRowView;
                        if (drv != null)
                        {
                            object name = drv[tree.PreviewFieldName];
                            object val = drv[tree.KeyFieldName];
                            list.Add(val, name);
                        }
                    }
                    else
                    {
                        T data = tree.GetDataRecordByNode(item) as T;
                        if (data != null)
                        {
                            PropertyInfo propertyInfo = data.GetType().GetProperty(tree.PreviewFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            object name = propertyInfo.GetValue(data, null);
                            propertyInfo = data.GetType().GetProperty(tree.KeyFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            object val = propertyInfo.GetValue(data, null);
                            list.Add(val, name);
                        }
                    }
                }
                if (item.HasChildren)
                    foreach (var dic in GetTreeListCheckedKeyValue<T>(tree, item))
                    {
                        list.Add(dic.Key, dic.Value);
                    }

            }

            return list;
        }
        /// <summary>
        /// 获取TreeList已选择的主键值集合
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <returns></returns>
        public static Dictionary<object, object> GetTreeListCheckedKeyValue<T>(TreeList tree) where T : class
        {
            Dictionary<object, object> list = new Dictionary<object, object>();
            if (tree == null) return list;

            foreach (TreeListNode item in tree.Nodes)
            {
                foreach (var dic in GetTreeListCheckedKeyValue<T>(tree, item))
                {
                    list.Add(dic.Key, dic.Value);
                }
            }
            return list;
        }
        /// <summary>
        /// 选中TreeList
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <param name="isFocus"></param>
        public static void SetTreeListNodeChecked<T>(TreeList tree, object key, TreeListNode node, bool isFocus = true) where T : class
        {
            T data = tree.GetDataRecordByNode(node) as T;
            if (data != null)
            {
                if (key != null)
                {
                    PropertyInfo propertyInfo = data.GetType().GetProperty(tree.KeyFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    object name = propertyInfo.GetValue(data, null);
                    if (name.Equals(key) || name.ToString() == key.ToString())
                    {
                        if (isFocus)
                            node.Selected = true;
                        else
                            node.Checked = true;
                    }
                }
                else
                {
                    if (isFocus)
                        node.Selected = false;
                    else
                        node.Checked = false;
                }
            }

            foreach (TreeListNode item in node.Nodes)
            {
                data = tree.GetDataRecordByNode(item) as T;
                if (data != null)
                {
                    if (key != null)
                    {
                        PropertyInfo propertyInfo = data.GetType().GetProperty(tree.KeyFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        object name = propertyInfo.GetValue(data, null);
                        if ((name.Equals(key) || name.ToString() == key.ToString()) && !item.HasChildren)
                        {
                            if (isFocus)
                                item.Selected = true;
                            else
                                item.Checked = true;
                        }
                    }
                    else
                    {
                        if (isFocus)
                            item.Selected = false;
                        else
                            item.Checked = false;
                    }
                }

                if (item.HasChildren)
                    SetTreeListNodeChecked<T>(tree, key, item, isFocus);

            }
        }
        /// <summary>
        /// 选中TreeList
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="key"></param>
        /// <param name="isFocus"></param>
        public static void SetTreeListNodeChecked<T>(TreeList tree, object key, bool isFocus = true) where T : class
        {
            foreach (TreeListNode item in tree.Nodes)
            {
                SetTreeListNodeChecked<T>(tree, key, item, isFocus);
            }
        }

        /// <summary>
        /// 查找子控件
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="searchChildren"></param>
        /// <returns></returns>
        public static List<Control> FindControl(Control parentControl, bool searchChildren = true)
        {
            if (parentControl == null) return null;

            List<Control> list = new List<Control>();
            foreach (Control control in parentControl.Controls)
            {
                list.Add(control);
                if (control.HasChildren && searchChildren)
                    list.AddRange(FindControl(control, true));
            }
            return list;
        }

        /// <summary>
        /// 获得程序版本号
        /// </summary>
        /// <returns></returns>
        public static string ApplicationVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }


        /// <summary>
        /// 将消息加到RichTextBox中
        /// </summary>
        /// <param name="msg"></param>
        public static void AddMsgToRichTextBox(RichTextBox rtxt, string msg, bool addTime = true, bool addLine = true)
        {
            if (rtxt == null) return;
            rtxt.AddMsg(msg, addTime, addLine);
        }

        /// <summary>
        /// 将消息加到RichTextBox中 扩展方法
        /// </summary>
        /// <param name="msg"></param>
        public static void AddMsg(this RichTextBox rtxt, string msg, bool addTime = true, bool addLine = true)
        {
            if (rtxt == null) return;

            if (msg == null) msg = "";
            StringBuilder sendMsg = new StringBuilder();
            if (addLine)
                sendMsg.AppendFormat("[{0}] ", DateTime.Now);

            sendMsg.Append(msg);

            if (addLine)
                sendMsg.Append(Environment.NewLine);

            if (rtxt.IsHandleCreated)
                rtxt.Invoke(new Action(() =>
                {
                    rtxt.AppendText(sendMsg.ToString());
                    rtxt.ScrollToCaret();
                }));
            else
            {
                rtxt.HandleCreated += (s, e) =>
                {
                    rtxt.Invoke(new Action(() =>
                    {
                        rtxt.AppendText(sendMsg.ToString());
                        rtxt.ScrollToCaret();
                    }));
                };
            }
        }
    }
}
