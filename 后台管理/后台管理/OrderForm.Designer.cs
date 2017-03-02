namespace 后台管理
{
    partial class OrderForm
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
            this.orderID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.currentCondition = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toCondition = new System.Windows.Forms.ComboBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "订单号";
            // 
            // orderID
            // 
            this.orderID.AutoSize = true;
            this.orderID.Location = new System.Drawing.Point(59, 13);
            this.orderID.Name = "orderID";
            this.orderID.Size = new System.Drawing.Size(0, 12);
            this.orderID.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "当前订单状态";
            // 
            // currentCondition
            // 
            this.currentCondition.AutoSize = true;
            this.currentCondition.Location = new System.Drawing.Point(95, 34);
            this.currentCondition.Name = "currentCondition";
            this.currentCondition.Size = new System.Drawing.Size(0, 12);
            this.currentCondition.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "将订单状态更改为";
            // 
            // toCondition
            // 
            this.toCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toCondition.FormattingEnabled = true;
            this.toCondition.Location = new System.Drawing.Point(119, 52);
            this.toCondition.Name = "toCondition";
            this.toCondition.Size = new System.Drawing.Size(153, 20);
            this.toCondition.TabIndex = 5;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(38, 81);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 6;
            this.ok.Text = "确认";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(166, 81);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 7;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 116);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.toCondition);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.currentCondition);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.orderID);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "OrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "订单处理";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label orderID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label currentCondition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox toCondition;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
    }
}