using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace ChangePicture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] args = null;
        public Form1(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        string dir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
        string path = "ChangePicture";
        bool Read2Close = false;
        private void ImageLoad()
        {

            while (clsTools.IsNetReady() == false)
            {
                System.Threading.Thread.Sleep(15000);
            }
            while (DownloadImg() == false)
            {
                System.Threading.Thread.Sleep(15000);
            }
            pbPicture.Image = Image.FromFile(dir + "\\" + path + ".jpg");
            btnApply.Text = "应用壁纸";
            labLoading.Visible = false;
            btnApply.Enabled = true;
            //若为静默启动
            
            if (args != null)
            {

                int num = 0;
                while (SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, dir + "\\" + path + ".bmp", 1) == 0)
                {
                    num++;
                    System.Threading.Thread.Sleep(15000);
                    if (num > 5)
                    {
                        break;
                    }
                }
                Read2Close = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClsRegedit clsRegeditContral = new ClsRegedit("ChangePicture");
            //判断图片保存地址是否修改
            if (clsRegeditContral.IsKeyExit("fold"))
            {

                dir = clsRegeditContral.GetKey("fold");
            }
            //判断图片保存格式
            if (clsRegeditContral.IsKeyExit("path"))
            {
                path = clsTools.PathFilter(clsRegeditContral.GetKey("path"));
            }
            //释放注册表操作
            clsRegeditContral = null;

            Control.CheckForIllegalCrossThreadCalls = false;//允许其他线程访问控件
            Thread th = new Thread(ImageLoad);
            th.Start();
            if (args != null)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }

        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
        static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, string lpvParam, Int32 fuWinIni);//////lpvParam要设置成string
        private const int SPI_SETDESKWALLPAPER = 20;

        //下载图片

        private bool DownloadImg()
        {
            //获取图片
            //获取当前用户图片文件夹
            try
            {
                string html = clsHTML.GetHtml("http://cn.bing.com");
                Match result = Regex.Match(html, "g_img={url: \"(.*?)\",id:");
                html = Regex.Replace(Regex.Replace(Regex.Replace(result.Value, "g_img={url: \"", ""), "\",id:", ""), "\\\\", "");
                WebClient client = new WebClient();
                client.DownloadFile(html, dir + "\\" + path + ".jpg");
                if (File.Exists(dir + "\\" + path + ".jpg"))
                {
                    clsTools.Jpg2Bmp(dir + "\\" + path + ".jpg", dir + "\\" + path + ".bmp");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, dir + "\\" + path + ".bmp", 1) == 0)
            {
                MessageBox.Show("设置失败");
            }
            else
            {
                MessageBox.Show("完成");
            }
            btnApply.Enabled = true;
        }



        private void labSetting_Click(object sender, EventArgs e)
        {
            //初始化保存地址
            txtFold.Text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            //初始化开机启动选项
            string[] subkeyNames;
            RegistryKey hkml = Registry.CurrentUser;
            RegistryKey SubKey = hkml.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion", true).CreateSubKey("Run");
            subkeyNames = SubKey.GetValueNames();
            //取得该项下所有键值的名称的序列，并传递给预定的数组中
            foreach (string keyName in subkeyNames)
            {
                if (keyName == "ChangePicture") //判断键值的名称
                {
                    cbAutoRun.Checked = true;
                }
            }
            //释放
            SubKey = hkml = null;
            //初始化注册表
            ClsRegedit clsRegeditContral = new ClsRegedit("ChangePicture");
            if (clsRegeditContral.IsKeyExit("fold"))
            {
                txtFold.Text = clsRegeditContral.GetKey("fold");
            }
            if (clsRegeditContral.IsKeyExit("path"))
            {
                cbSaveAs.Checked = true;
                txtPath.Visible = true;
                txtPath.Text = clsRegeditContral.GetKey("path");
            }
            this.Height = 400;
            labSetting.Visible = false;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            nfiTips.Visible = false;
        }

        private void nfiTips_DoubleClick(object sender, EventArgs e)
        {

            //停止计时器
            tmClose.Enabled = false;
            if (this.ShowInTaskbar == false)
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.Activate();

            }
            else
            {
                this.Hide();
                this.ShowInTaskbar = false;
                tmClose.Enabled = true;
            }

        }

        private void tmClose_Tick(object sender, EventArgs e)
        {
            //结束程序
            if (Read2Close==true)
            {
                nfiTips.Visible = false;
                Application.Exit();
            }
            

        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            nfiTips.Visible = false;
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsRegedit clsRegeditContral = new ClsRegedit("ChangePicture");
            //判断图片保存地址是否修改
            if (txtFold.Text != System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures))
            {

                clsRegeditContral.SetValue("fold", txtFold.Text);
            }

            if (cbSaveAs.Checked == true)
            {

                if (txtPath.Text == "")
                {
                    MessageBox.Show("壁纸保存格式不得为空");
                    return;
                }
                clsRegeditContral.SetValue("path", txtPath.Text);
            }
            else
            {
                clsRegeditContral.DelValue("path");
            }
            //判断开机启动
            if (cbAutoRun.Checked == true)
            {
                //开机启动
                try
                {
                    RegistryKey key = Registry.CurrentUser;
                    RegistryKey SubKey = key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion", true).CreateSubKey("Run");
                    SubKey.SetValue("ChangePicture", Application.ExecutablePath + " -e");
                }
                catch { }

            }
            else
            {
                //非开机自启
                try
                {
                    RegistryKey key = Registry.CurrentUser;
                    RegistryKey SubKey = key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion", true).CreateSubKey("Run");
                    SubKey.DeleteValue("ChangePicture");
                }
                catch { }
            }
            //关闭设置窗口
            MessageBox.Show("部分设置重启程序后生效");
            this.Height = 205;
            labSetting.Visible = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            fbdFold.ShowDialog();
            txtFold.Text = fbdFold.SelectedPath;
        }

        private void cbSaveAs_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSaveAs.Checked == true)
            {
                txtPath.Visible = true;
            }
            else
            {
                txtPath.Visible = false;
            }
        }

        private void labweb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://blazenur.com/archives/25.html");
        }
    }
}
