namespace 后台管理
{
    partial class DetailInfoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OrderRelatedLabel = new System.Windows.Forms.Label();
            this.UserRelatedLabel = new System.Windows.Forms.Label();
            this.CarRelatedLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "订单：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "客户：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "车辆：";
            // 
            // OrderRelatedLabel
            // 
            this.OrderRelatedLabel.AutoSize = true;
            this.OrderRelatedLabel.Location = new System.Drawing.Point(83, 64);
            this.OrderRelatedLabel.Name = "OrderRelatedLabel";
            this.OrderRelatedLabel.Size = new System.Drawing.Size(41, 12);
            this.OrderRelatedLabel.TabIndex = 3;
            this.OrderRelatedLabel.Text = "订单：";
            // 
            // UserRelatedLabel
            // 
            this.UserRelatedLabel.AutoSize = true;
            this.UserRelatedLabel.Location = new System.Drawing.Point(83, 188);
            this.UserRelatedLabel.Name = "UserRelatedLabel";
            this.UserRelatedLabel.Size = new System.Drawing.Size(41, 12);
            this.UserRelatedLabel.TabIndex = 4;
            this.UserRelatedLabel.Text = "订单：";
            // 
            // CarRelatedLabel
            // 
            this.CarRelatedLabel.AutoSize = true;
            this.CarRelatedLabel.Location = new System.Drawing.Point(83, 315);
            this.CarRelatedLabel.Name = "CarRelatedLabel";
            this.CarRelatedLabel.Size = new System.Drawing.Size(41, 12);
            this.CarRelatedLabel.TabIndex = 5;
            this.CarRelatedLabel.Text = "订单：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 533);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 55);
            this.button1.TabIndex = 6;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CommentLabel);
            this.panel1.Controls.Add(this.label);
            this.panel1.Controls.Add(this.OrderRelatedLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CarRelatedLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.UserRelatedLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 515);
            this.panel1.TabIndex = 7;
            // 
            // CommentLabel
            // 
            this.CommentLabel.AutoSize = true;
            this.CommentLabel.Location = new System.Drawing.Point(83, 432);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(41, 12);
            this.CommentLabel.TabIndex = 7;
            this.CommentLabel.Text = "订单：";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(50, 417);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(41, 12);
            this.label.TabIndex = 6;
            this.label.Text = "评论：";
            // 
            // DetailInfoForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "DetailInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "详细信息";
            this.Load += new System.EventHandler(this.DetailInfoForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label OrderRelatedLabel;
        private System.Windows.Forms.Label UserRelatedLabel;
        private System.Windows.Forms.Label CarRelatedLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label CommentLabel;
        private System.Windows.Forms.Label label;
    }
}