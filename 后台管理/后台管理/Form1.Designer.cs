namespace 后台管理
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_AddCar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.WaitingLabel = new System.Windows.Forms.Label();
            this.dataGridView_CarHire = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Condition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTimeCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTimeStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AliPayNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NeedDriver = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NeedManualConfirm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Car_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Store_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView_Flight = new System.Windows.Forms.DataGridView();
            this.ID_Plane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Condition_Plane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTimeCreated_Plane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTimePlane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note_Plane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Airport_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_ID_Plane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_AddActivity = new System.Windows.Forms.Button();
            this.button_OrderManagement = new System.Windows.Forms.Button();
            this.button_Detail = new System.Windows.Forms.Button();
            this.button_RefreshServer = new System.Windows.Forms.Button();
            this.button_CarManagement = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CarHire)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Flight)).BeginInit();
            this.SuspendLayout();
            // 
            // button_AddCar
            // 
            this.button_AddCar.Location = new System.Drawing.Point(7, 562);
            this.button_AddCar.Name = "button_AddCar";
            this.button_AddCar.Size = new System.Drawing.Size(118, 41);
            this.button_AddCar.TabIndex = 3;
            this.button_AddCar.Text = "添加车辆";
            this.button_AddCar.UseVisualStyleBackColor = true;
            this.button_AddCar.Click += new System.EventHandler(this.button_AddCar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 556);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.WaitingLabel);
            this.tabPage1.Controls.Add(this.dataGridView_CarHire);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 527);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "租车订单";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // WaitingLabel
            // 
            this.WaitingLabel.AutoSize = true;
            this.WaitingLabel.BackColor = System.Drawing.Color.White;
            this.WaitingLabel.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WaitingLabel.Location = new System.Drawing.Point(57, 187);
            this.WaitingLabel.Name = "WaitingLabel";
            this.WaitingLabel.Size = new System.Drawing.Size(668, 180);
            this.WaitingLabel.TabIndex = 9;
            this.WaitingLabel.Text = "程序正在努力加载中\r\n坐和放宽";
            this.WaitingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WaitingLabel.Visible = false;
            // 
            // dataGridView_CarHire
            // 
            this.dataGridView_CarHire.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CarHire.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Condition,
            this.DateTimeCreated,
            this.DateTimeStart,
            this.AliPayNumber,
            this.NeedDriver,
            this.NeedManualConfirm,
            this.Note,
            this.User_ID,
            this.Car_ID,
            this.Store_ID,
            this.Comment_ID});
            this.dataGridView_CarHire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_CarHire.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_CarHire.MultiSelect = false;
            this.dataGridView_CarHire.Name = "dataGridView_CarHire";
            this.dataGridView_CarHire.ReadOnly = true;
            this.dataGridView_CarHire.RowTemplate.Height = 27;
            this.dataGridView_CarHire.Size = new System.Drawing.Size(786, 521);
            this.dataGridView_CarHire.TabIndex = 0;
            this.dataGridView_CarHire.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CarHire_RowEnter);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Condition
            // 
            this.Condition.HeaderText = "订单状态";
            this.Condition.Name = "Condition";
            this.Condition.ReadOnly = true;
            // 
            // DateTimeCreated
            // 
            this.DateTimeCreated.HeaderText = "订单创建日期";
            this.DateTimeCreated.Name = "DateTimeCreated";
            this.DateTimeCreated.ReadOnly = true;
            // 
            // DateTimeStart
            // 
            this.DateTimeStart.HeaderText = "预约取车日期";
            this.DateTimeStart.Name = "DateTimeStart";
            this.DateTimeStart.ReadOnly = true;
            // 
            // AliPayNumber
            // 
            this.AliPayNumber.HeaderText = "订单号";
            this.AliPayNumber.Name = "AliPayNumber";
            this.AliPayNumber.ReadOnly = true;
            // 
            // NeedDriver
            // 
            this.NeedDriver.HeaderText = "需要司机";
            this.NeedDriver.Name = "NeedDriver";
            this.NeedDriver.ReadOnly = true;
            // 
            // NeedManualConfirm
            // 
            this.NeedManualConfirm.HeaderText = "人工确认";
            this.NeedManualConfirm.Name = "NeedManualConfirm";
            this.NeedManualConfirm.ReadOnly = true;
            // 
            // Note
            // 
            this.Note.HeaderText = "备注";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            // 
            // User_ID
            // 
            this.User_ID.HeaderText = "客户ID";
            this.User_ID.Name = "User_ID";
            this.User_ID.ReadOnly = true;
            // 
            // Car_ID
            // 
            this.Car_ID.HeaderText = "车辆ID";
            this.Car_ID.Name = "Car_ID";
            this.Car_ID.ReadOnly = true;
            // 
            // Store_ID
            // 
            this.Store_ID.HeaderText = "店面ID";
            this.Store_ID.Name = "Store_ID";
            this.Store_ID.ReadOnly = true;
            // 
            // Comment_ID
            // 
            this.Comment_ID.HeaderText = "评论ID";
            this.Comment_ID.Name = "Comment_ID";
            this.Comment_ID.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_Flight);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "接机订单";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Flight
            // 
            this.dataGridView_Flight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Flight.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_Plane,
            this.Condition_Plane,
            this.DateTimeCreated_Plane,
            this.DateTimePlane,
            this.Note_Plane,
            this.Airport_ID,
            this.User_ID_Plane});
            this.dataGridView_Flight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Flight.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Flight.MultiSelect = false;
            this.dataGridView_Flight.Name = "dataGridView_Flight";
            this.dataGridView_Flight.ReadOnly = true;
            this.dataGridView_Flight.RowTemplate.Height = 27;
            this.dataGridView_Flight.Size = new System.Drawing.Size(786, 521);
            this.dataGridView_Flight.TabIndex = 0;
            this.dataGridView_Flight.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Flight_RowEnter);
            // 
            // ID_Plane
            // 
            this.ID_Plane.HeaderText = "ID";
            this.ID_Plane.Name = "ID_Plane";
            this.ID_Plane.ReadOnly = true;
            // 
            // Condition_Plane
            // 
            this.Condition_Plane.HeaderText = "订单状态";
            this.Condition_Plane.Name = "Condition_Plane";
            this.Condition_Plane.ReadOnly = true;
            // 
            // DateTimeCreated_Plane
            // 
            this.DateTimeCreated_Plane.HeaderText = "订单创建时间";
            this.DateTimeCreated_Plane.Name = "DateTimeCreated_Plane";
            this.DateTimeCreated_Plane.ReadOnly = true;
            // 
            // DateTimePlane
            // 
            this.DateTimePlane.HeaderText = "预约接机时间";
            this.DateTimePlane.Name = "DateTimePlane";
            this.DateTimePlane.ReadOnly = true;
            // 
            // Note_Plane
            // 
            this.Note_Plane.HeaderText = "备注";
            this.Note_Plane.Name = "Note_Plane";
            this.Note_Plane.ReadOnly = true;
            // 
            // Airport_ID
            // 
            this.Airport_ID.HeaderText = "机场ID";
            this.Airport_ID.Name = "Airport_ID";
            this.Airport_ID.ReadOnly = true;
            // 
            // User_ID_Plane
            // 
            this.User_ID_Plane.HeaderText = "客户ID";
            this.User_ID_Plane.Name = "User_ID_Plane";
            this.User_ID_Plane.ReadOnly = true;
            // 
            // button_AddActivity
            // 
            this.button_AddActivity.Location = new System.Drawing.Point(675, 562);
            this.button_AddActivity.Name = "button_AddActivity";
            this.button_AddActivity.Size = new System.Drawing.Size(118, 41);
            this.button_AddActivity.TabIndex = 5;
            this.button_AddActivity.Text = "添加活动";
            this.button_AddActivity.UseVisualStyleBackColor = true;
            this.button_AddActivity.Click += new System.EventHandler(this.button_AddActivity_Click);
            // 
            // button_OrderManagement
            // 
            this.button_OrderManagement.Location = new System.Drawing.Point(379, 562);
            this.button_OrderManagement.Name = "button_OrderManagement";
            this.button_OrderManagement.Size = new System.Drawing.Size(118, 41);
            this.button_OrderManagement.TabIndex = 6;
            this.button_OrderManagement.Text = "处理订单";
            this.button_OrderManagement.UseVisualStyleBackColor = true;
            this.button_OrderManagement.Click += new System.EventHandler(this.button_OrderManagement_Click);
            // 
            // button_Detail
            // 
            this.button_Detail.Location = new System.Drawing.Point(503, 562);
            this.button_Detail.Name = "button_Detail";
            this.button_Detail.Size = new System.Drawing.Size(118, 41);
            this.button_Detail.TabIndex = 8;
            this.button_Detail.Text = "查询详细信息";
            this.button_Detail.UseVisualStyleBackColor = true;
            this.button_Detail.Click += new System.EventHandler(this.button_Detail_Click);
            // 
            // button_RefreshServer
            // 
            this.button_RefreshServer.Location = new System.Drawing.Point(255, 562);
            this.button_RefreshServer.Name = "button_RefreshServer";
            this.button_RefreshServer.Size = new System.Drawing.Size(118, 41);
            this.button_RefreshServer.TabIndex = 9;
            this.button_RefreshServer.Text = "刷新服务器";
            this.button_RefreshServer.UseVisualStyleBackColor = true;
            this.button_RefreshServer.Click += new System.EventHandler(this.button_RefreshServer_Click);
            // 
            // button_CarManagement
            // 
            this.button_CarManagement.Location = new System.Drawing.Point(131, 562);
            this.button_CarManagement.Name = "button_CarManagement";
            this.button_CarManagement.Size = new System.Drawing.Size(118, 41);
            this.button_CarManagement.TabIndex = 10;
            this.button_CarManagement.Text = "管理车辆";
            this.button_CarManagement.UseVisualStyleBackColor = true;
            this.button_CarManagement.Click += new System.EventHandler(this.button_CarManagement_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 615);
            this.Controls.Add(this.button_CarManagement);
            this.Controls.Add(this.button_RefreshServer);
            this.Controls.Add(this.button_Detail);
            this.Controls.Add(this.button_OrderManagement);
            this.Controls.Add(this.button_AddActivity);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_AddCar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "泓驰租车后台管理";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CarHire)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Flight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_AddCar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView_CarHire;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView_Flight;
        private System.Windows.Forms.Button button_AddActivity;
        private System.Windows.Forms.Button button_OrderManagement;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_Plane;
        private System.Windows.Forms.DataGridViewTextBoxColumn Condition_Plane;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTimeCreated_Plane;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTimePlane;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note_Plane;
        private System.Windows.Forms.DataGridViewTextBoxColumn Airport_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_ID_Plane;
        private System.Windows.Forms.Button button_Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Condition;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTimeCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTimeStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn AliPayNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NeedDriver;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NeedManualConfirm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Car_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Store_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment_ID;
        private System.Windows.Forms.Label WaitingLabel;
        private System.Windows.Forms.Button button_RefreshServer;
        private System.Windows.Forms.Button button_CarManagement;
    }
}

