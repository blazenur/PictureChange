
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace ChangePicture
{
    class clsTools
    {
        /// <summary>
        /// 根据语法规则生成文件名称
        /// </summary>
        /// <param name="SaveName"></param>
        /// <returns></returns>
        public static string PathFilter(string SaveName)
        {
           return Regex.Replace(Regex.Replace(Regex.Replace(SaveName, "yy", DateTime.Now.ToString("yyyy")), "mm", DateTime.Now.ToString("MM")), "dd", DateTime.Now.ToString("dd"));
        }



        /// <summary>
        /// JPG转BMP
        /// </summary>
        /// <param name="jpg"></param>
        /// <param name="bmp"></param>
        public static void Jpg2Bmp(string jpg,string bmp)
        {
            Bitmap bitmap = null;
            Bitmap bitmap2 = null;
            try
            {
                bitmap = new Bitmap(jpg);
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format16bppArgb1555);
                bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, data.Stride, PixelFormat.Format16bppArgb1555, data.Scan0);
                bitmap2.Save(bmp, ImageFormat.Bmp);
                bitmap.UnlockBits(data);
            }
            finally
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (bitmap2 != null)
                {
                    bitmap2.Dispose();
                }
            }
            
        }


        /// <summary>
        /// 检测网络连接状态
        /// </summary>
        public static bool IsNetReady()
        {
            bool success;
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                //测试网络连接：目标计算机为192.168.1.1(可以换成你所需要的目标地址）
                //如果网络连接成功，PING就应该有返回；否则，网络连接有问题
                PingReply reply = pingSender.Send("www.baidu.com", timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }
    }
}
