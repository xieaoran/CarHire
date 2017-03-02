using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 后台管理
{
    public partial class OrderForm : Form
    {
        public delegate void ShowEventHandler();
        public event ShowEventHandler ParentShowEvent;

        private int _orderID, _orderCondition;
        private string _mode;
        public OrderForm(int orderID, int orderCondition, string mode)
        {
            InitializeComponent();
            _orderID = orderID;
            _orderCondition = orderCondition;
            _mode = mode;
            this.Show();
        }

        private void ReadOrder()
        {
            switch (_mode)
            {
                case "select":
                    toCondition.Items.Add("等待支付");
                    toCondition.Items.Add("支付完毕，等待确认");
                    toCondition.Items.Add("确认完毕，等待取车");
                    toCondition.Items.Add("取车完毕，等待还车");
                    toCondition.Items.Add("还车完毕，订单完成");
                    toCondition.Items.Add("已取消");
                    toCondition.SelectedIndex = _orderCondition;
                    currentCondition.Text = toCondition.Text;
                    orderID.Text = _orderID.ToString();
                    break;
                case "plane":
                    toCondition.Items.Add("等待确认");
                    toCondition.Items.Add("确认完毕，等待接机");
                    toCondition.Items.Add("接机完毕，订单完成");
                    toCondition.Items.Add("已取消");
                    toCondition.SelectedIndex = _orderCondition;
                    currentCondition.Text = toCondition.Text;
                    orderID.Text = _orderID.ToString();
                    break;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.Hide();
            ParentShowEvent();
            switch (_mode)
            {
                case "select":
                    ManageHelper.ChangeOrderCondition(_orderID, toCondition.SelectedIndex);
                    break;
                case "plane":
                    ManageHelper.ChangePlaneOrderCondition(_orderID, toCondition.SelectedIndex);
                    break;
            }
            this.Dispose();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            ReadOrder();
        }
    }
}
