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
                clsTools.Jpg2Bmp(dir + "\\" + path + ".jpg", dir + "\\" + path + ".bmp");
                while (SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, dir + "\\" + path + ".bmp", 1) == 0)
                {
                    num++;
                    System.Threading.Thread.Sleep(15000);
                    if (num > 5)
                    {
                        break;
                    }
                }
                //删除转换后的BMP文件
                if (File.Exists(dir + "\\" + path + ".bmp"))
                {
                    //如果存在则删除
                    File.Delete(dir + "\\" + path + ".bmp");
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
                //启动注册表操作
                ClsRegedit clsRegeditContral = new ClsRegedit("ChangePicture");
                string html = clsHTML.GetHtml("http://cn.bing.com");
                Match result = Regex.Match(html, "g_img={url: \"(.*?)\",id:");
                html = Regex.Replace(Regex.Replace(Regex.Replace(result.Value, "g_img={url: \"", ""), "\",id:", ""), "\\\\", "");
                //下载图片
                
                if (clsRegeditContral.GetKey("Proxy") == "1")
                {
                    WebRequest request = WebRequest.Create("http://cn.bing.com" + html);
                    //启动代理
                    //读取IE默认代理
                    if (clsRegeditContral.GetKey("ReadIEProxy") == "1")
                    {
                        WebProxy gProxy = WebProxy.GetDefaultProxy();
                        request.Proxy = gProxy;
                    }
                    else
                    {
                        int d = 0;
                        //将端口号转换为int类型
                        int.TryParse(clsRegeditContral.GetKey("proxyPort"), out d);
                        WebProxy gProxy = new WebProxy(clsRegeditContral.GetKey("proxyHost"), d);
                        request.Proxy = gProxy;
                    }
                    WebResponse response = request.GetResponse();
                    Stream reader = response.GetResponseStream();
                    FileStream writer = new FileStream(dir + "\\" + path + ".jpg", FileMode.OpenOrCreate, FileAccess.Write);
                    byte[] buff = new byte[512];
                    int c = 0; //实际读取的字节数
                    while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                    {
                        writer.Write(buff, 0, c);
                    }
                    writer.Close();
                    writer.Dispose();
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                }
                else
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(html, dir + "\\" + path + ".jpg");
                }


                if (File.Exists(dir + "\\" + path + ".jpg"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception err)
            {
                return false;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            clsTools.Jpg2Bmp(dir + "\\" + path + ".jpg", dir + "\\" + path + ".bmp");
            if (SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, dir + "\\" + path + ".bmp", 1) == 0)
            {
                MessageBox.Show("设置失败");
            }
            else
            {
                MessageBox.Show("完成");
            }
            //删除转换后的BMP文件
            if (File.Exists(dir + "\\" + path + ".bmp"))
            {
                //如果存在则删除
                File.Delete(dir + "\\" + path + ".bmp");
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
            //初始化界面图标状态
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
            if (clsRegeditContral.GetKey("Proxy") == "1")
            {
                isProxy.Checked = true;
                //判断是否读取IE代理
                if (clsRegeditContral.GetKey("ReadIEProxy") == "1")
                {
                    readIEProxy.Checked = true;
                }
                else
                {
                    hostAddressPort.Text = clsRegeditContral.GetKey("proxyPort");
                    hostAddressText.Text = clsRegeditContral.GetKey("proxyHost");
                }
            }




            this.Height = 450;
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
            if (txtFold.Text != System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures)&& txtFold.Text !="")
            {

                clsRegeditContral.SetValue("fold", txtFold.Text);
            }
            else
            {
                clsRegeditContral.DelValue("fold");
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

            //保存代理信息
            if (isProxy.Checked == true)
            {
                clsRegeditContral.SetValue("Proxy", "1");
                //读取IE设置
                if (readIEProxy.Checked == true)
                {
                    clsRegeditContral.SetValue("ReadIEProxy", "1");
                }
                else
                {
                    clsRegeditContral.SetValue("ReadIEProxy", "0");
                    //代理信息输入不正确
                    if (hostAddressText.Text == "" || hostAddressPort.Text == "")
                    {
                        MessageBox.Show("代理信息不得为空");
                        return;
                    } 
                }
                clsRegeditContral.SetValue("proxyHost", hostAddressText.Text);
                clsRegeditContral.SetValue("proxyPort", hostAddressPort.Text);
            }
            else
            {
                clsRegeditContral.SetValue("Proxy", "0");
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
            try
            {
                System.Diagnostics.Process.Start("http://blazenur.com/archives/25.html");
            }
            catch
            {
                MessageBox.Show("浏览器启动失败");
            }
            
        }

        private void isProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (isProxy.Checked == true)
            {
                //用户单机选中时触发
                hostAddress.Visible = true;
                hostAddressPort.Visible = true;
                hostAddressText.Visible = true;
                hostPort.Visible = true;
                readIEProxy.Visible = true;
            }
            else
            {
                hostAddress.Visible = false;
                hostAddressPort.Visible = false;
                hostAddressText.Visible = false;
                hostPort.Visible = false;
                readIEProxy.Visible = false;
            }
        }

        private void readIEProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (readIEProxy.Checked == true)
            {
                //用户单机选中时触发
                hostAddressPort.Enabled = false;
                hostAddressText.Enabled = false;
            }
            else
            {
                hostAddressPort.Enabled = true;
                hostAddressText.Enabled = true;
            }
        }
    }
}
