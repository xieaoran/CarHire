using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace 后台管理
{
    public partial class Form1 : Form
    {
        WelcomeForm welcomeForm;
        CarsForm carsForm;
        DetailInfoForm detailForm;
        OrderForm orderForm;
        ActivityForm activityForm;
        CarManagement carManagement;

        System.Windows.Forms.Timer timer;
        public Form1()
        {
            welcomeForm = new WelcomeForm();
            welcomeForm.Show();
            InitializeComponent();
            this.Load += Form1_Load;
            this.Shown += Form1_Shown;
            this.Disposed += Form1_Disposed;
            SQLHelper.Connect();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1 * 30 * 1000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            RefreshForm();
        }

        #region 窗体事件合辑
        void Form1_Shown(object sender, EventArgs e)
        {
            welcomeForm.Hide();
        }
        void Form1_Load(object sender, EventArgs e)
        {
            SQLHelper.ReadOrders(dataGridView_CarHire);
            FTPHelper.Connect();
            ManageHelper.Verify();
        }
        void Form1_Disposed(object sender, EventArgs e)
        {
            SQLHelper.Close();
            Application.Exit();
        }
        /// <summary>
        /// 处理分支窗口关闭的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Disposed(object sender, EventArgs e)
        {
            this.Show();
            RefreshForm();
        }
        /// <summary>
        /// 分支窗口载入完毕后，主窗口消失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OtherForm_Load(object sender, EventArgs e)
        {
            WaitingLabel.Hide();
            this.Hide();
        }

        #endregion

        #region 按钮事件合辑
        private void button_AddCar_Click(object sender, EventArgs e)
        {
            carsForm = new CarsForm();
            this.Hide();
            carsForm.Disposed += Form_Disposed;
            Application.DoEvents();
        }
        private void button_OrderManagement_Click(object sender, EventArgs e)
        {
            WaitingLabel.Show();
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    orderForm = new OrderForm(int.Parse(GetID("ID")), int.Parse(GetID("Condition")), "select");
                    break;
                case 1:
                    orderForm = new OrderForm(int.Parse(GetID("ID_Plane")), int.Parse(GetID("Condition_Plane")), "plane");
                    break;
            }
            orderForm.Shown += OtherForm_Load;
            orderForm.Disposed += Form_Disposed;
            orderForm.ParentShowEvent += ParentShow;
        }

        void ParentShow()
        {
            this.WaitingLabel.Show();
            this.Show();
        }
        private void button_Detail_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                WaitingLabel.Show();
                Refresh();
                detailForm = new DetailInfoForm(
                    GetID("User_ID"), GetID("Car_ID"), GetID("ID"), GetID("Comment_ID"));
                detailForm.Shown += OtherForm_Load;
                detailForm.Disposed += Form_Disposed;
                detailForm.Show();
            }
            else
            {
                MessageBox.Show(
                    SQLHelper.ReadAirport(GetID("Airport_ID")) + "\r\n"
                    + SQLHelper.ReadFlightOrder(GetID("ID_Plane")) + "\r\n"
                    + SQLHelper.ReadUser(GetID("User_ID_Plane")),
                    "详细信息");
            }
        }
        private void button_RefreshServer_Click(object sender, EventArgs e)
        {
            WaitingLabel.Show();
            ManageHelper.RefreshServer();
            WaitingLabel.Hide();
        }
        #endregion

        #region 数据框窗体组件事件
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshForm();
        }
        private int RowIndex = -1;
        private void dataGridView_CarHire_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
        }

        private void dataGridView_Flight_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
        }
        #endregion

        #region 一些杂项
        private void RefreshForm()
        {
            timer.Enabled = false;
            WaitingLabel.Show();
            Refresh();
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    SQLHelper.ReadOrders(dataGridView_CarHire);
                    break;
                case 1:
                    SQLHelper.ReadPlaneOrders(dataGridView_Flight);
                    break;
            }
            WaitingLabel.Hide();
            Refresh();
            timer.Enabled = true;
        }       
        private string GetID(string name)
        {
            if (RowIndex != -1)
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    return dataGridView_CarHire[name, RowIndex].Value.ToString();
                }
                else
                {
                    return dataGridView_Flight[name, RowIndex].Value.ToString();
                }
            }
            else
                return null;
        }
        #endregion

        private void button_AddActivity_Click(object sender, EventArgs e)
        {
            WaitingLabel.Show();
            activityForm = new ActivityForm();
            this.Hide();
            activityForm.Disposed += Form_Disposed;
            Application.DoEvents();
            WaitingLabel.Hide();
        }

        private void button_CarManagement_Click(object sender, EventArgs e)
        {
            WaitingLabel.Show();
            carManagement = new CarManagement();
            this.Hide();
            carManagement.Disposed += Form_Disposed;
            Application.DoEvents();
            WaitingLabel.Hide();
        }
    }
}
