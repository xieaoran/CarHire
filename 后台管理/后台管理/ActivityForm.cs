using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 后台管理
{
    public partial class ActivityForm : Form
    {
        int tIndex = -1;
        string _imageFile;

        string tID = "";
        public ActivityForm()
        {
            InitializeComponent();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 选择本地文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            openImageDialog.FileName = "";
            openImageDialog.ShowDialog();
            if (!File.Exists(openImageDialog.FileName)) return;
            _imageFile = openImageDialog.FileName;
            textBox_ImgPath.Text = HashHelper.HashFile(_imageFile) + "." + _imageFile.Split('.').Last();
            button4.Text = "重新选择";
            Image a=Image .FromStream(openImageDialog.OpenFile());
            pictureBox1.Image = a;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckFull())
            {
                MessageBox.Show("请确保所有基本信息均已录入！");
                return;
            }
            else
            {
                WaitingLabel.Show();
                SQLHelper.InsertData("Activities",
                    textBox_Name.Text,
                    "/Images/" + textBox_ImgPath.Text,
                    textBox_BasicInfo.Text);
                FTPHelper.AddFile(textBox_ImgPath.Text, File.OpenRead(_imageFile));
                ManageHelper.RefreshServer();
                WaitingLabel.Hide();
                MessageBox.Show("录入成功！");
            }
        }

        private bool CheckFull()
        {
            return textBox_Name.Text.Length > 0 
                && textBox_ImgPath.Text.Length > 0 
                && textBox_BasicInfo.Text.Length > 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tIndex == -1)
                return;
            WaitingLabel.Show();

            SQLHelper.DeleteData("Activities", "ID", tID);

            WaitingLabel.Hide();
            SQLHelper.ReadActivities(dataGridView1);
            MessageBox.Show("删除成功！");
            tIndex = -1;
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            SQLHelper.ReadActivities(dataGridView1);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            tIndex = e.RowIndex;
            if (tIndex >= 0)
            {
                object value = dataGridView1["ID_Activity", tIndex].Value;
                tID = value != null ? value.ToString() : "-1";
                button2.Enabled = tID != "-1";
            }
        }
    }
}
