using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CameraManager
{
    public class MethodOfKernel32
    {
        

        #region 对dll文件的操作
        /// <summary>
        /// 加载bin\Debug文件夹下的DLL文件
        /// </summary>
        /// <param name="lpLibFileName">dll文件的名字</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "LoadModule")]
        public static extern int LoadModule([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);


        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        public static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        public static extern bool FreeLibrary(int hModule);
        #endregion
    }
}
