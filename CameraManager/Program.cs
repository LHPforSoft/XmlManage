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
            XmlManage xmlMg = new XmlManage();
            xmlMg.ConfigInit();
            List<string> getValue = xmlMg.GetValue("Client", "CPath");
            for (int i = 0; i < getValue.Count; i++)
            {
                Console.WriteLine("得到的值：" + getValue[i].ToString());
            }

            xmlMg.setXmlValue("Client", "CVbr", "2.1.0.12");
            Console.ReadKey();
        }
    }
}
