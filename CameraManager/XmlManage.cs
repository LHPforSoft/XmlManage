using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CameraManager
{
    public class XmlManage
    {
        private static XmlDocument xmlConfig;
        private static string xmlFilePath = string.Empty;

        /// <summary>
        /// 读取生成程序路径下的Option.xml文件，得到所有的节点
        /// </summary>
        public void ConfigInit(string XmlPath)
        {
            if (XmlPath == "")
            {
                xmlFilePath = Environment.CurrentDirectory + "\\Option.xml";
                xmlConfig = new XmlDocument();
                xmlConfig.Load(xmlFilePath);//第二种方式
                                            //xmlConfig = new XmlDocument();
                                            //xmlPath = Assembly.GetExecutingAssembly().CodeBase;
                                            //Int32 i = xmlPath.LastIndexOf("/");
                                            //xmlPath = xmlPath.Remove(i);
                                            //xmlPath = xmlPath + @"/abc.xml";
            }
            else
            {
                xmlFilePath = XmlPath;
            }


        }
        /// <summary>
        /// 返回XMl文件指定元素的指定属性值
        /// </summary>
        /// <param name="xmlElement">指定元素</param>
        /// <param name="xmlAttribute">指定属性</param>
        /// <returns></returns>
        public List<string> GetXmlValue(string xmlElement, string xmlAttribute)
        {
            List<string> reList = new List<string>();
            try
            {
                //两种方式获得Client节点下的信息
                XmlNodeList clientList = xmlConfig.GetElementsByTagName(xmlElement);// doc.SelectNodes("/ClientModel/Client");
                if (clientList != null)
                {
                    foreach (XmlNode client in clientList)
                    {
                        reList.Add(client.Attributes[xmlAttribute].Value);
                    }
                }
                return reList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 设置XMl文件指定元素的指定属性的值 
        /// </summary>
        /// <param name="xmlElement">指定元素</param>
        /// <param name="xmlAttribute">指定属性</param>
        /// <param name="xmlValue">指定值</param>
        public void SetXmlValue(string xmlElement, string xmlAttribute, string xmlValue)
        {
            XmlNodeList list = xmlConfig.GetElementsByTagName(xmlElement);
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Attributes.Count; j++)
                {
                    if (list[i].Attributes[j].Name == xmlAttribute)
                    {
                        list[i].Attributes[j].Value = xmlValue;
                    }
                }
            }
            try
            {
                //保存上面的修改　　  
                xmlConfig.Save(xmlFilePath);//或者如下
                                            //StreamWriter swriter = new StreamWriter("Option.xml");
                                            //XmlTextWriter xw = new XmlTextWriter(swriter);
                                            //xw.Formatting = Formatting.Indented;
                                            //xmlConfig.WriteTo(xw);
                                            //xw.Close();
                                            //swriter.Close(); 
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
