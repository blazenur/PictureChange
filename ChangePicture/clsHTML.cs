using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ChangePicture
{
    class clsHTML
    {
       
            private static int FailCount = 0; //记录下载失败的次数

            public static string GetHtml(string url) //传入要下载的网址
            {
                string str = string.Empty;
                try
                {
                //使用代理
                //初始化注册表操作
                ClsRegedit clsRegeditContral = new ClsRegedit("ChangePicture");
                System.Net.WebRequest request = System.Net.WebRequest.Create(url);
                if (clsRegeditContral.GetKey("Proxy") == "1")
                {
                    //启动代理
                    //读取IE默认代理
                    if (clsRegeditContral.GetKey("ReadIEProxy") == "1")
                    {
                        WebProxy gProxy = WebProxy.GetDefaultProxy();
                        request.Proxy = gProxy;
                    }
                    else
                    {
                        int c = 0;
                        //将端口号转换为int类型
                        int.TryParse(clsRegeditContral.GetKey("proxyPort"), out c);
                        WebProxy gProxy = new WebProxy(clsRegeditContral.GetKey("proxyHost"), c);
                        request.Proxy = gProxy;
                    }
                }
                    request.Timeout = 10000; //下载超时时间
                    request.Headers.Set("Pragma", "no-cache");
                    System.Net.WebResponse response = request.GetResponse();
                    System.IO.Stream streamReceive = response.GetResponseStream();
                    Encoding encoding = Encoding.GetEncoding("utf-8");//utf-8 网页文字编码
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(streamReceive, encoding);
                    str = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                catch (Exception ex)
                {
                    FailCount++;

                    if (FailCount > 5)
                    {
                        var result = System.Windows.Forms.MessageBox.Show("已下载失败" + FailCount + "次，是否要继续尝试？" + Environment.NewLine + ex.Message, "数据下载异常", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            str = GetHtml(url);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("下载HTML失败" + Environment.NewLine + ex.ToString(), "下载HTML失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        str = "";
                           
                        }
                    }
                    else
                    {
                        str = GetHtml(url);
                    }
                }

                FailCount = 0; //如果能执行到这一步就表示下载终于成功了
                return str;
            }

        }
    }
