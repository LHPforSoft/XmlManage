using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CameraManager
{
    public class IniManage
    {
        public string iniPath;

        public IniManage(string IniPath)
        {
            if (IniPath == "")
            {
                this.iniPath = Environment.CurrentDirectory + "\\information.ini";

            }
            else
            {
                this.iniPath = IniPath;
            }
        }


        #region ini文件操作
        /// <summary>
        /// 读某路径下的ini文件中的section组或键值组
        /// </summary>
        /// <param name="section">要读取的段落名</param>
        /// <param name="key">要读取的键</param>
        /// <param name="defVal">读取异常的情况下的缺省值</param>
        /// <param name="retVal">key所对应的值，如果该key不存在则返回空值</param>
        /// <param name="size">值允许的大小</param>
        /// <param name="filePath">INI文件的完整路径和文件名</param>
        /// <returns>返回byte类型的section组或键值组</returns>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);


        /// <summary>
        /// 读某路径下的ini文件中的某个key键对应的value值
        /// </summary>
        /// <param name="section">要读取的段落名</param>
        /// <param name="key">要读取的键</param>
        /// <param name="defVal">读取异常的情况下的缺省值</param>
        /// <param name="retVal">key所对应的值，如果该key不存在则返回空值</param>
        /// <param name="size">值允许的大小</param>
        /// <param name="filePath">INI文件的完整路径和文件名</param>
        /// <returns>返回StringBuilder类型的键值</returns>
        [DllImport("kernel32.dll")]
        public static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);


        /// <summary>
        /// 写入ini文件一对键值
        /// </summary>
        /// <param name="section">要写入的段落名</param>
        /// <param name="key">要写入的键，如果该key存在则覆盖写入</param>
        /// <param name="val">key所对应的值</param>
        /// <param name="filePath">INI文件的完整路径和文件名</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        #endregion


        /// <summary>
        /// 读取INI文件返回的键的值
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <returns>返回的键的值</returns>
        public string IniReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.iniPath);
            return temp.ToString();
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section">段，格式[]</param>
        /// <param name="Key">键</param>
        /// <returns>返回byte类型的section组或键值组</returns>
        public byte[] IniReadKeys(string section, string key)
        {
            byte[] temp = new byte[255];
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.iniPath);
            return temp;
        }

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <param name="iValue">值</param>
        public void IniWriteValue(string section, string key, string iValue)
        {
            WritePrivateProfileString(section, key, iValue, this.iniPath);
        }       
    }
}