namespace CodeGeneration
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._linkLogingDb = new System.Windows.Forms.LinkLabel();
            this._txtMySqlLoginPwd = new System.Windows.Forms.TextBox();
            this._txtMySqlLoginName = new System.Windows.Forms.TextBox();
            this._txtServerPort = new System.Windows.Forms.TextBox();
            this._txtServerIP = new System.Windows.Forms.TextBox();
            this._cbDbList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._lbxTableList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._btnOk = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._btnAddAreaName = new System.Windows.Forms.Button();
            this._txtAreaName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._treeList = new System.Windows.Forms.TreeView();
            this._btnToArea = new System.Windows.Forms.Button();
            this._btnToTable = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _linkLogingDb
            // 
            this._linkLogingDb.AutoSize = true;
            this._linkLogingDb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._linkLogingDb.Location = new System.Drawing.Point(241, 114);
            this._linkLogingDb.Name = "_linkLogingDb";
            this._linkLogingDb.Size = new System.Drawing.Size(80, 16);
            this._linkLogingDb.TabIndex = 5;
            this._linkLogingDb.TabStop = true;
            this._linkLogingDb.Text = "登录MySql";
            this._linkLogingDb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkLogingDb_LinkClicked);
            // 
            // _txtMySqlLoginPwd
            // 
            this._txtMySqlLoginPwd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._txtMySqlLoginPwd.Location = new System.Drawing.Point(97, 73);
            this._txtMySqlLoginPwd.Name = "_txtMySqlLoginPwd";
            this._txtMySqlLoginPwd.Size = new System.Drawing.Size(126, 26);
            this._txtMySqlLoginPwd.TabIndex = 3;
            this._txtMySqlLoginPwd.Text = "123456";
            // 
            // _txtMySqlLoginName
            // 
            this._txtMySqlLoginName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._txtMySqlLoginName.Location = new System.Drawing.Point(97, 108);
            this._txtMySqlLoginName.Name = "_txtMySqlLoginName";
            this._txtMySqlLoginName.Size = new System.Drawing.Size(126, 26);
            this._txtMySqlLoginName.TabIndex = 4;
            this._txtMySqlLoginName.Text = "root";
            // 
            // _txtServerPort
            // 
            this._txtServerPort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._txtServerPort.Location = new System.Drawing.Point(241, 34);
            this._txtServerPort.Name = "_txtServerPort";
            this._txtServerPort.Size = new System.Drawing.Size(65, 26);
            this._txtServerPort.TabIndex = 2;
            this._txtServerPort.Text = "3307";
            // 
            // _txtServerIP
            // 
            this._txtServerIP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._txtServerIP.Location = new System.Drawing.Point(97, 34);
            this._txtServerIP.Name = "_txtServerIP";
            this._txtServerIP.Size = new System.Drawing.Size(126, 26);
            this._txtServerIP.TabIndex = 1;
            this._txtServerIP.Text = "127.0.0.1";
            // 
            // _cbDbList
            // 
            this._cbDbList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._cbDbList.FormattingEnabled = true;
            this._cbDbList.Location = new System.Drawing.Point(97, 144);
            this._cbDbList.Name = "_cbDbList";
            this._cbDbList.Size = new System.Drawing.Size(224, 24);
            this._cbDbList.TabIndex = 6;
            this._cbDbList.TextChanged += new System.EventHandler(this._cbDbList_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(25, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "数据库：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(41, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(41, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "账号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(223, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(839, 538);
            this.splitContainer1.SplitterDistance = 363;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 315);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "说明";
            // 
            // _lbxTableList
            // 
            this._lbxTableList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lbxTableList.FormattingEnabled = true;
            this._lbxTableList.ItemHeight = 14;
            this._lbxTableList.Location = new System.Drawing.Point(25, 68);
            this._lbxTableList.Name = "_lbxTableList";
            this._lbxTableList.Size = new System.Drawing.Size(176, 438);
            this._lbxTableList.TabIndex = 10;
            this._lbxTableList.DoubleClick += new System.EventHandler(this._lbxTableList_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this._cbDbList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._txtServerIP);
            this.groupBox1.Controls.Add(this._linkLogingDb);
            this.groupBox1.Controls.Add(this._txtMySqlLoginName);
            this.groupBox1.Controls.Add(this._txtServerPort);
            this.groupBox1.Controls.Add(this._txtMySqlLoginPwd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LoginMySql";
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._btnOk.Location = new System.Drawing.Point(342, 20);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(88, 33);
            this._btnOk.TabIndex = 9;
            this._btnOk.Text = "生成项目";
            this._btnOk.UseVisualStyleBackColor = true;
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._btnToTable);
            this.groupBox3.Controls.Add(this._btnToArea);
            this.groupBox3.Controls.Add(this._lbxTableList);
            this.groupBox3.Controls.Add(this._treeList);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this._txtAreaName);
            this.groupBox3.Controls.Add(this._btnAddAreaName);
            this.groupBox3.Controls.Add(this._btnOk);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(447, 514);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成项目";
            // 
            // _btnAddAreaName
            // 
            this._btnAddAreaName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._btnAddAreaName.Location = new System.Drawing.Point(244, 20);
            this._btnAddAreaName.Name = "_btnAddAreaName";
            this._btnAddAreaName.Size = new System.Drawing.Size(92, 33);
            this._btnAddAreaName.TabIndex = 8;
            this._btnAddAreaName.Text = "添加名称";
            this._btnAddAreaName.UseVisualStyleBackColor = true;
            this._btnAddAreaName.Click += new System.EventHandler(this._btnAddAreaName_Click);
            // 
            // _txtAreaName
            // 
            this._txtAreaName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._txtAreaName.Location = new System.Drawing.Point(105, 23);
            this._txtAreaName.Name = "_txtAreaName";
            this._txtAreaName.Size = new System.Drawing.Size(133, 26);
            this._txtAreaName.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(22, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 333;
            this.label7.Text = "合并名称:";
            // 
            // _treeList
            // 
            this._treeList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._treeList.Location = new System.Drawing.Point(261, 68);
            this._treeList.Name = "_treeList";
            this._treeList.ShowRootLines = false;
            this._treeList.Size = new System.Drawing.Size(169, 434);
            this._treeList.TabIndex = 11;
            this._treeList.DoubleClick += new System.EventHandler(this._treeList_DoubleClick);
            // 
            // _btnToArea
            // 
            this._btnToArea.Location = new System.Drawing.Point(207, 212);
            this._btnToArea.Name = "_btnToArea";
            this._btnToArea.Size = new System.Drawing.Size(48, 23);
            this._btnToArea.TabIndex = 12;
            this._btnToArea.Text = "》";
            this._btnToArea.UseVisualStyleBackColor = true;
            this._btnToArea.Click += new System.EventHandler(this._btnToArea_Click);
            // 
            // _btnToTable
            // 
            this._btnToTable.Location = new System.Drawing.Point(207, 269);
            this._btnToTable.Name = "_btnToTable";
            this._btnToTable.Size = new System.Drawing.Size(48, 23);
            this._btnToTable.TabIndex = 13;
            this._btnToTable.Text = "《";
            this._btnToTable.UseVisualStyleBackColor = true;
            this._btnToTable.Click += new System.EventHandler(this._btnToTable_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(311, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "1、选择数据库后可直接生成项目代码，一个表一个类文件";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(287, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "3、合并先需要添加合并后的名称，只支持英文字母，";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(203, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "2、合并指把数据表合并成一个类文件";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(299, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "4、选中合并名称后双击表名即可添加到合并文件结构下";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "   长度大于等于5个字符";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 159);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "项目说明：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 179);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "1、增删查改，查带条件";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "操作说明：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 202);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "2、本地日志功能";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 224);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(113, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "3、MemoryCache缓存";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 246);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(113, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "4、swagger接口文档";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(18, 269);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(0, 12);
            this.label18.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(18, 270);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(203, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "5、开发工具VS2017，NetCore2.2架构";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 538);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代码生成器（MySql数据库）";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _linkLogingDb;
        private System.Windows.Forms.TextBox _txtMySqlLoginPwd;
        private System.Windows.Forms.TextBox _txtMySqlLoginName;
        private System.Windows.Forms.TextBox _txtServerPort;
        private System.Windows.Forms.TextBox _txtServerIP;
        private System.Windows.Forms.ComboBox _cbDbList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _btnOk;
        private System.Windows.Forms.ListBox _lbxTableList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button _btnAddAreaName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _txtAreaName;
        private System.Windows.Forms.TreeView _treeList;
        private System.Windows.Forms.Button _btnToTable;
        private System.Windows.Forms.Button _btnToArea;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
    }
}

