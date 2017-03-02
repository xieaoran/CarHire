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
    public partial class CarManagement : Form
    {
        int tIndex = -1;
        public CarManagement()
        {
            InitializeComponent();
            this.Show();
        }

        private void CarManagement_Load(object sender, EventArgs e)
        {
            SQLHelper.ReadCars(dataGridView1);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            tIndex = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tIndex != -1)
            {
                WaitingLabel.Show();
                if ((bool)dataGridView1["Enabled_CarManagement", tIndex].Value)
                {
                    SQLHelper.UpdateData("Cars", "Enabled", "0", "ID", dataGridView1["ID_CarManagement", tIndex].Value.ToString());
                }
                else
                {
                    SQLHelper.UpdateData("Cars", "Enabled", "1", "ID", dataGridView1["ID_CarManagement", tIndex].Value.ToString());
                }
                SQLHelper.ReadCars(dataGridView1);
                dataGridView1.Refresh();
                MessageBox.Show("更改状态成功！");
                WaitingLabel.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
