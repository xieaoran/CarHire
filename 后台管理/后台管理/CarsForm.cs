using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 后台管理
{
    public partial class CarsForm : Form
    {
        private string _imageFile;
        private string _detailImageFile1;
        private string _detailImageFile2;

        public CarsForm()
        {
            InitializeComponent();
            this.Show();
        }

        private void CarsForm_Load(object sender, EventArgs e)
        {
            comboBox_Recommendation.SelectedIndex = 0;
            Console_WriteLine("窗体加载完毕");
        }

        private bool VerifyInput()
        {
            return textBox_Deposit.Text != ""
                && textBox_Detail1.Text != ""
                && textBox_Detail2.Text != ""
                && textBox_ImgPath.Text != ""
                && textBox_BasicInfo.Text != ""
                && textBox_Name.Text != ""
                && textBox_Oilcost.Text != ""
                && textBox_Price.Text != ""
                && textBox_Speed.Text != ""
                && textBox_DetailImagePath1.Text != ""
                && textBox_DetailImagePath2.Text != ""
                && _imageFile != ""
                && _detailImageFile1 != ""
                && _detailImageFile2 != "";
        }


        private void Console_WriteLine(string value)
        {
            var t = textBox_Info.Text.Split('\n');
            if (t.Length > 12)
            {
                textBox_Info.Text = "";
            }
            textBox_Info.Text += value + "\r\n";
        }

        private void button_Submit_Click(object sender, EventArgs e)
        {
            if (VerifyInput())
            {
                button_Submit.Hide();
                button_SubmitConfirm.Show();
                Console_WriteLine("请再次确认所有信息均已填写正确，然后点击“确认提交”按钮");
            }
            else
            {
                Console_WriteLine("请确保所有输入框均有填写数据！");
            }
        }

        private void button_SubmitConfirm_Click(object sender, EventArgs e)
        {
            WaitingLabel.Visible = true;
            SQLHelper.InsertData("Cars",
                "true",
                comboBox_Recommendation.SelectedIndex.ToString(),
                textBox_Detail1.Text,
                "/Images/" + textBox_DetailImagePath1.Text,
                textBox_Detail2.Text,
                "/Images/" + textBox_DetailImagePath2.Text,
                textBox_Speed.Text,
                textBox_Oilcost.Text,
                textBox_Price.Text,
                textBox_Deposit.Text,
                textBox_Name.Text,
                "/Images/" + textBox_ImgPath.Text,
                textBox_BasicInfo.Text
                );
            FTPHelper.AddFile(textBox_ImgPath.Text, File.OpenRead(_imageFile));
            FTPHelper.AddFile(textBox_DetailImagePath1.Text, File.OpenRead(_detailImageFile1));
            FTPHelper.AddFile(textBox_DetailImagePath2.Text, File.OpenRead(_detailImageFile2));
            ManageHelper.RefreshServer();
            WaitingLabel.Visible = false;
            ResetForm();
            button_SubmitConfirm.Hide();
            button_Submit.Show();
            Console_WriteLine("车型添加成功，请继续添加下一个车型。");
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            ResetForm();
            Console_WriteLine("各项输入均已重置");
        }

        private void ResetForm()
        {
            textBox_Deposit.Text =
                textBox_Detail1.Text =
                textBox_Detail2.Text =
                textBox_ImgPath.Text =
                textBox_BasicInfo.Text =
                textBox_Name.Text =
                textBox_Oilcost.Text =
                textBox_Price.Text =
                textBox_Speed.Text =
                textBox_DetailImagePath1.Text =
                textBox_DetailImagePath2.Text =
                _imageFile =
                _detailImageFile1 =
                _detailImageFile2 = "";
            comboBox_Recommendation.SelectedIndex = 0;
            selectImage.Text =
                selectImage1.Text =
                selectImage2.Text = "选择文件";
        }

        private void selectImage_Click(object sender, EventArgs e)
        {
            openImageDialog.FileName = "";
            openImageDialog.ShowDialog();
            if (!File.Exists(openImageDialog.FileName)) return;
            _imageFile = openImageDialog.FileName;
            textBox_ImgPath.Text = HashHelper.HashFile(_imageFile) + "." + _imageFile.Split('.').Last();
            selectImage.Text = "重新选择";
        }

        private void selectImage1_Click(object sender, EventArgs e)
        {
            openImageDialog.FileName = "";
            openImageDialog.ShowDialog();
            if (!File.Exists(openImageDialog.FileName)) return;
            _detailImageFile1 = openImageDialog.FileName;
            textBox_DetailImagePath1.Text = HashHelper.HashFile(_detailImageFile1) + "." + _detailImageFile1.Split('.').Last();
            selectImage1.Text = "重新选择";
        }

        private void selectImage2_Click(object sender, EventArgs e)
        {
            openImageDialog.FileName = "";
            openImageDialog.ShowDialog();
            if (!File.Exists(openImageDialog.FileName)) return;
            _detailImageFile2 = openImageDialog.FileName;
            textBox_DetailImagePath2.Text = HashHelper.HashFile(_detailImageFile2) + "." + _detailImageFile2.Split('.').Last();
            selectImage2.Text = "重新选择";
        }
    }
}
