namespace 后台管理
{
    partial class CarManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ID_CarManagement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name_CarManagement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Enabled_CarManagement = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BasicInfo_CarManagement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaitingLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_CarManagement,
            this.Name_CarManagement,
            this.Enabled_CarManagement,
            this.BasicInfo_CarManagement});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(776, 526);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 544);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "更改车辆状态";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(661, 544);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 44);
            this.button2.TabIndex = 2;
            this.button2.Text = "退回主界面";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ID_CarManagement
            // 
            this.ID_CarManagement.HeaderText = "ID";
            this.ID_CarManagement.Name = "ID_CarManagement";
            this.ID_CarManagement.ReadOnly = true;
            // 
            // Name_CarManagement
            // 
            this.Name_CarManagement.HeaderText = "车辆名";
            this.Name_CarManagement.Name = "Name_CarManagement";
            this.Name_CarManagement.ReadOnly = true;
            // 
            // Enabled_CarManagement
            // 
            this.Enabled_CarManagement.HeaderText = "可用";
            this.Enabled_CarManagement.Name = "Enabled_CarManagement";
            this.Enabled_CarManagement.ReadOnly = true;
            // 
            // BasicInfo_CarManagement
            // 
            this.BasicInfo_CarManagement.HeaderText = "基本信息";
            this.BasicInfo_CarManagement.Name = "BasicInfo_CarManagement";
            this.BasicInfo_CarManagement.ReadOnly = true;
            // 
            // WaitingLabel
            // 
            this.WaitingLabel.AutoSize = true;
            this.WaitingLabel.BackColor = System.Drawing.Color.White;
            this.WaitingLabel.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WaitingLabel.Location = new System.Drawing.Point(66, 210);
            this.WaitingLabel.Name = "WaitingLabel";
            this.WaitingLabel.Size = new System.Drawing.Size(668, 180);
            this.WaitingLabel.TabIndex = 10;
            this.WaitingLabel.Text = "程序正在努力加载中\r\n坐和放宽";
            this.WaitingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WaitingLabel.Visible = false;
            // 
            // CarManagement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.WaitingLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CarManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CarManagement";
            this.Load += new System.EventHandler(this.CarManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_CarManagement;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name_CarManagement;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enabled_CarManagement;
        private System.Windows.Forms.DataGridViewTextBoxColumn BasicInfo_CarManagement;
        private System.Windows.Forms.Label WaitingLabel;
    }
}