using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace ChangePicture
{
    class ClsRegedit
    {
        RegistryKey key = Registry.CurrentUser;
        RegistryKey SubKey;
        public ClsRegedit(string SubName)
        {
            //打开注册表项，不存在则创建
            SubKey = key.OpenSubKey("SOFTWARE", true).CreateSubKey(SubName);
            
            //在HKEY_LOCAL_MACHINE\SOFTWARE下新建名为test的注册表项。如果已经存在则不影响！
        }

        /// <summary>
        /// 删除注册表项
        /// </summary>
        /// <param name="SubName"></param>
        public void DeleteSubKey(string SubName)
        {
            key.DeleteSubKey("SOFTWARE\\"+ SubName, true); //该方法无返回值，直接调用即可
        }

        /// <summary>
        /// 创建键值
        /// </summary>
        /// <param name="SubName">注册表项</param>
        /// <param name="KeyName">键名</param>
        /// <param name="KeyVal">键值</param>
        public void SetValue(string KeyName,string KeyVal)
        {

            SubKey.SetValue(KeyName, KeyVal);
            //在HKEY_LOCAL_MACHINE\SOFTWARE\test下创建一个名为“test”，值为“博客园”的键值。如果该键值原本已经存在，则会修改替换原来的键值，如果不存在则是创建该键值。
            // 注意：SetValue()还有第三个参数，主要是用于设置键值的类型，如：字符串，二进制，Dword等等~~默认是字符串。如：
            // software.SetValue("test", "0", RegistryValueKind.DWord); //二进制信息

        }

        /// <summary>
        /// 删除注册表值
        /// </summary>
        /// <param name="KeyName"></param>
        public void DelValue(string KeyName)
        {
            try
            {
                SubKey.DeleteValue(KeyName);
            }
            catch { }
           
        }

        /// <summary>
        /// 查询注册表项是否存在
        /// </summary>
        /// <param name="SubKey"></param>
        /// <returns></returns>
        public bool IsSubKeyExist(string SubKey)
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE");
            //RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);  
            subkeyNames = software.GetSubKeyNames();
            //取得该项下所有子项的名称的序列，并传递给预定的数组中  
            foreach (string keyName in subkeyNames)
            //遍历整个数组  
            {
                if (keyName == SubKey)
                //判断子项的名称  
                {
                    hkml.Close();
                    return true;
                }
            }
            hkml.Close();
            return false;
        }

        /// <summary>
        /// 判断注册表键值是否存在
        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public bool IsKeyExit(string KeyName)
        {
            string[] subkeyNames;

            //RegistryKey software = hkml.OpenSubKey("SOFTWARE\\test", true);
            subkeyNames = SubKey.GetValueNames();
            //取得该项下所有键值的名称的序列，并传递给预定的数组中
            foreach (string keyName in subkeyNames)
            {
                if (keyName == KeyName) //判断键值的名称
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取注册表键值
        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public string GetKey(string KeyName)
        {
            try
            {
                return SubKey.GetValue(KeyName).ToString();
            }
            catch
            {
                return "0";
            }
            
        }

        ~ClsRegedit()
        {
            key.Close();
            SubKey.Close();
        }
    }
}
