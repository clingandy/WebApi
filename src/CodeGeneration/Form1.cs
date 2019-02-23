using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CodeGeneration
{
    public partial class Form1 : Form
    {
        #region 属性

        /// <summary>
        /// 当期库
        /// </summary>
        private string _curDbName = "";
        /// <summary>
        /// 当期库表信息
        /// </summary>
        private Dictionary<string, string> _tableNameList = new Dictionary<string, string>();

        #endregion

        #region WinForm事件

        public Form1()
        {
            InitializeComponent();
        }

        private void _linkLogingDb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                LoaddingDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"数据库连接失败；错误信息：" + ex.Message);
            }

        }

        private void _cbDbList_TextChanged(object sender, EventArgs e)
        {
            GetTable();
        }

        private void _btnAddAreaName_Click(object sender, EventArgs e)
        {
            //要添加的节点名称为空，即文本框是否为空
            var value = _txtAreaName.Text.Trim();
            if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, "^[a-zA-z]+$") || value.Length < 5)
            {
                //MessageBox.Show(@"要添加的领域名称不能为空！并且只能是字母");
                return;
            }

            //添加根节点
            _treeList.Nodes.Add(value);
            _txtAreaName.Text = "";
        }

        private void _lbxTableList_DoubleClick(object sender, EventArgs e)
        {
            ToArea();
        }

        private void _treeList_DoubleClick(object sender, EventArgs e)
        {
            ToTable();
        }

        private void _btnToArea_Click(object sender, EventArgs e)
        {
            ToArea();
        }


        private void _btnToTable_Click(object sender, EventArgs e)
        {
            ToTable();
        }

        private void _btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                CreateFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region winform操作通用

        /// <summary>
        /// 添加到_treeList
        /// </summary>
        private void ToArea()
        {
            if (_treeList.SelectedNode == null || _lbxTableList.SelectedItem == null)
            {
                return;
            }

            if (_treeList.SelectedNode.Parent != null)
            {
                _treeList.SelectedNode.Parent.Nodes.Add(_lbxTableList.SelectedItem.ToString());
            }
            else
            {
                _treeList.SelectedNode.Nodes.Add(_lbxTableList.SelectedItem.ToString());
            }

            _treeList.ExpandAll();
            _lbxTableList.Items.Remove(_lbxTableList.SelectedItem);
        }

        /// <summary>
        /// 添加到_lbxTableList
        /// </summary>
        private void ToTable()
        {
            if (_treeList.SelectedNode?.Parent == null)
            {
                return;
            }
            _lbxTableList.Items.Add(_treeList.SelectedNode.Text);
            _treeList.Nodes.Remove(_treeList.SelectedNode);
        }

        #endregion

        #region 加载数据和生成文件

        /// <summary>
        /// 获取表列表和创建项目
        /// </summary>
        private void CreateFile()
        {
            if (string.IsNullOrWhiteSpace(_cbDbList.Text))
            {
                MessageBox.Show(@"请先选择数据库");
                return;
            }

            var tableNameList = _tableNameList;

            //创建文件
            var savePath = Path.Combine("D:\\ToDemo", _cbDbList.Text);
            CreateProjectFile.SetSaveFile(savePath);

            var primaryKeyList = new List<string>();    //主键名称
            var dicTableColumn = new Dictionary<string, List<ColumnEntity>>();    //表名+对应列信息
            foreach (var tableName in tableNameList)
            {
                var list = GetColumn(tableName.Key);
                primaryKeyList.Add(list.FirstOrDefault(t=> t.Key == "PRI")?.ColumnName);
                dicTableColumn.Add(tableName.Key , list);
            }

            // 合并表文件信息
            var dicArea = new Dictionary<string, List<string>>();   //合并后的名称+表名称列表
            foreach (TreeNode note in _treeList.Nodes)
            {
                var tempList = new List<string>();
                foreach (TreeNode cNote in note.Nodes)
                {
                    tempList.Add(cNote.Text);
                }
                dicArea.Add(note.Text, tempList);
            }

            CreateProjectFile.ToModel(dicTableColumn, _cbDbList.Text);
            CreateProjectFile.ToService(tableNameList, dicTableColumn, dicArea, _cbDbList.Text);
            CreateProjectFile.ToDomain(tableNameList, dicArea, _cbDbList.Text, primaryKeyList);
            CreateProjectFile.ToCore(tableNameList, dicArea, _cbDbList.Text, primaryKeyList);
            CreateProjectFile.ToWeb(tableNameList, dicArea, _cbDbList.Text, string.Format(MySqlHelper.DbConStr, _cbDbList.Text));
            CreateProjectFile.ToCsproj(_cbDbList.Text);
            CreateProjectFile.ToLib();

            System.Diagnostics.Process.Start("explorer.exe", savePath);
        }

        /// <summary>
        /// 获取数据库库名
        /// </summary>
        private void LoaddingDb()
        {
            MySqlHelper.GetMySqlCon(_txtServerIP.Text, _txtServerPort.Text, _txtMySqlLoginName.Text, _txtMySqlLoginPwd.Text);

            var dbList = new List<string>();
            using (var reader = MySqlHelper.ExecuteReader("SHOW DATABASES"))
            {
                while (reader.Read())
                {
                    dbList.Add(reader.GetString(0));
                }
            }

            _cbDbList.DataSource = dbList;
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        private void GetTable()
        {
            if (string.IsNullOrWhiteSpace(_cbDbList.Text))
            {
                MessageBox.Show(@"请先选择数据库");
                return;
            }
            if (_curDbName == _cbDbList.Text)
            {
                return;
            }
            _curDbName = _cbDbList.Text;

            //清空数据
            _lbxTableList.Items.Clear();

            //查询数据库中所有表名
            var sql = $"select table_name,table_comment from information_schema.tables where table_schema = '{_cbDbList.Text}' and table_type = 'base table'";
            var tableNameList = new Dictionary<string, string>();
            using (var reader = MySqlHelper.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    tableNameList[reader.GetString(0)] = reader.GetString(1);

                    _lbxTableList.Items.Add(reader.GetString(0));   //添加到listBox
                }
            }

            _tableNameList = tableNameList;
        }

        /// <summary>
        /// 获取表的字段列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private List<ColumnEntity> GetColumn(string tableName)
        {
            //select * from information_schema.columns where table_schema = 'crm_screw' and table_name = 'crm_client'
            //查询指定数据库中指定表的所有字段名column_name
            var sql = $"select column_name,is_nullable,column_default,data_type,column_key,extra,column_comment,table_schema, table_name from information_schema.columns where table_schema = '{_cbDbList.Text}' and table_name = '{tableName}'";
            var columnList = new List<ColumnEntity>();
            using (var reader = MySqlHelper.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    columnList.Add(new ColumnEntity
                    {
                        ColumnName = reader.GetString(0),
                        Nullable = reader.GetString(1),
                        Default = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        DataType = reader.GetString(3),
                        Key = reader.GetString(4),
                        Extra = reader.GetString(5),
                        ColumnComment = reader.GetString(6),
                        TableSchema = reader.GetString(7),
                        TableName = reader.GetString(8)
                    });
                }
            }

            return columnList;
        }


        #endregion

    }
}
