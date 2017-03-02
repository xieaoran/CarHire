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
    public partial class DetailInfoForm : Form
    {
        public DetailInfoForm()
        {
            InitializeComponent();
        }
        public DetailInfoForm (string User_ID,string Car_ID,string Order_ID,string Comment_ID)
        {
            InitializeComponent();
            CarRelatedLabel.Text = SQLHelper.ReadCar(Car_ID);
            UserRelatedLabel.Text = SQLHelper.ReadUser(User_ID);
            OrderRelatedLabel.Text = SQLHelper.ReadOrder(Order_ID);
            if (Comment_ID != "")
                CommentLabel.Text = SQLHelper.ReadComment(Comment_ID);
            else
                CommentLabel.Text = "还没有评论信息";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void DetailInfoForm_Load(object sender, EventArgs e)
        {

        }
    }
}
