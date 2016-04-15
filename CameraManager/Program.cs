using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //对xml文件的操作
            XmlManage xmlMg = new XmlManage();
            xmlMg.ConfigInit("");
            List<string> getValue = xmlMg.GetXmlValue("Client", "CPath");
            for (int i = 0; i < getValue.Count; i++)
            {
                Console.WriteLine("得到的值：" + getValue[i].ToString());
            }
            //Console.WriteLine("请输入版本号：");
            //string s = Console.ReadLine();
            xmlMg.SetXmlValue("Client", "CVbr", "2.1.0.12");


            //对Config文件的操作
            //Config文件中的appSetting部分的操作
            string configPath = "";
            ConfigManage cfgMg = new ConfigManage(configPath);
            string s = cfgMg.GetAppSectionOfAppConfig("Department");
            Console.WriteLine(s);
            cfgMg.AddAppSectionOfAppConfig("nnn", "123");
            cfgMg.UpdateAppSectionConfig();
            s = cfgMg.GetAppSectionOfAppConfig("Department");
            Console.WriteLine(s);

            //Config文件中的connectionStrings部分的操作
            var va = cfgMg.GetConnectionStringsConfig("conJxcBook");
            Console.WriteLine(va);
            // string
            string name = "as";
            string conString = "sd";
            string providerName = "asd";
            cfgMg.AddConnectionStringsConfig(name, conString, providerName);
            va = cfgMg.GetConnectionStringsConfig("conJxcBook");
            Console.WriteLine(va);

            //
            //path为ini文件的物理路径
            IniManage ini = new IniManage("");//""这里实际应该是inipath路径
            //读取ini文件的所有段落名
            byte[] allSection = ini.IniReadKeys(null, null);

            //通过如下方式转换byte[] 类型为string[]数组类型
            string[] allSectionList, sectionList;
            ASCIIEncoding ascii = new ASCIIEncoding();

            //编码并获取所有的段落名称
            string allsections = ascii.GetString(allSection);
            allSectionList = allsections.Split(new char[1] { '\0' });
            for (int i = 0; i < 254; i++)
            {
                if (allSectionList[i] != "")
                {
                    Console.WriteLine("所有段落的名称" + allSectionList[i]);
                }
                else
                {
                    break;
                }
            }

            //读取ini文件personal段落的所有键名，返回byte[]类型
            byte[] sectionByte = ini.IniReadKeys("score", null);
            //编码并获取当前段落下的key名称
            string sections = ascii.GetString(sectionByte);
            //获取key的数组
            sectionList = sections.Split(new char[1] { '\0' });
            for (int i = 0; i < 254; i++)
            {
                if (sectionList[i] != "")
                {
                    Console.WriteLine("score段落下的所有key名称" + sectionList[i]);
                }
                else
                {
                    break;
                }
            }

            //读取ini文件evideo段落的MODEL键的值
            string model = ini.IniReadValue("evideo", "MODEL");
            Console.WriteLine("evideo段落下的 MODEL值：" + model);
            //将值eth0写入ini文件evideo段落的DEVICE键
            ini.IniWriteValue("evideo", "DEVICE1", "eth2");

            ////删除ini文件下personal段落下的所有键
            //ini.IniWriteValue("personal", null, null);

            //删除ini文件下所有段落------未实现
            ini.IniWriteValue(null, null, null);
            Console.ReadKey();
        }
    }
}
