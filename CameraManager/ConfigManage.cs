using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace CameraManager
{
    public class ConfigManage
    {
        string FilePath = string.Empty;
        /// <summary>
        /// 程序使用者需要设置好Config文件的路径
        /// </summary>
        /// <param name="ConfigPath"></param>
        public ConfigManage(string ConfigPath)
        {
            if (ConfigPath == "")
            {
                System.IO.DirectoryInfo directoryInfo = Directory.GetParent(Environment.CurrentDirectory).Parent;
                FilePath = directoryInfo.FullName + "\\App.config";
            }
            else
            {
                FilePath = ConfigPath;
            }
        }

        #region         对Config中的appSettings节点的操作
        //读取appStrings配置节
        ///<summary>
        ///返回＊.config文件中appSettings配置节的value项
        ///</summary>
        ///<param name="strKey"></param>
        ///<returns></returns>
        public string GetAppSectionOfAppConfig(string strKey)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = FilePath;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == strKey)
                {
                    // return ConfigurationManager.AppSettings[strKey];
                    return config.AppSettings.Settings[strKey].Value.ToString();
                }
            }
            return null;
        }

        //更新AppConfig配置节
        ///<summary>
        ///在＊.exe.config文件中appSettings配置节增加一对键、值对
        ///</summary>
        ///<param name="newKey"></param>
        ///<param name="newValue"></param>
        public void AddAppSectionOfAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }
            // Open App.Config of executable           
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = FilePath;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            // You need to remove the old settings object before you can replace it
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            // Add an Application Setting.
            config.AppSettings.Settings.Add(newKey, newValue);
            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 更新appSettings节点的内容
        /// </summary>
        public void UpdateAppSectionConfig()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = FilePath; ;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            config.AppSettings.Settings["userName"].Value = "1";
            config.AppSettings.Settings["password"].Value = "1";
            config.AppSettings.Settings["Department"].Value = "1";
            config.AppSettings.Settings["returnValue"].Value = "1";
            config.AppSettings.Settings["pwdPattern"].Value = "1";
            config.AppSettings.Settings["userPattern"].Value = "1";
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion


        #region
        //读取connectionStrings配置节
        ///<summary>
        ///依据连接串名字connectionName返回数据连接字符串
        ///</summary>
        ///<param name="connectionName"></param>
        ///<returns></returns>
        public string GetConnectionStringsConfig(string connectionName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();
            //Console.WriteLine(connectionString);
            return connectionString;
        }

        // 更新connectionStrings配置节
        ///<summary>
        ///更新连接字符串
        ///</summary>
        ///<param name="newName">连接字符串名称</param>
        ///<param name="newConString">连接字符串内容</param>
        ///<param name="newProviderName">数据提供程序名称</param>
        public void AddConnectionStringsConfig(string newName, string newConString, string newProviderName)
        {
            //指定config文件读取           
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = FilePath; ;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            // Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            bool isModified = false;    //记录该连接串是否已经存在
                                        //如果要更改的连接串已经存在
            if (ConfigurationManager.ConnectionStrings[newName] != null)
            {
                isModified = true;
            }
            //新建一个连接字符串实例
            ConnectionStringSettings mySettings = new ConnectionStringSettings(newName, newConString, newProviderName);
            // 打开可执行的配置文件*.exe.config

            // 如果连接串已存在，首先删除它
            if (isModified)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(newName);
            }
            // 将新的连接串添加到配置文件中.
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            // 保存对配置文件所作的更改
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新载入配置文件的ConnectionStrings配置节
            ConfigurationManager.RefreshSection("connectionStrings");
        }
        #endregion
    }
}
