using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CameraManager
{
    /// <summary>
    /// 文本操作的方法
    /// </summary>
    public class TxtManage
    {
        public enum ErrCode
        {
            操作完成 = 1,
            字符串超长 = 2,
            字符串不合法 = 3,
            指定物理路径不存在 = 4,
            指定物理位置已经存在具有相同名称的物理文件或文件夹 = 5,
            在指定物理位置不具备相应的物理操作权限 = 6,
            物理文件写入失败 = 7,
            指定物理文件不存在 = 8,
            指定物理文件获取错误 = 9
        }
        public int a;

        public TxtManage()
        {

        }

        public void CreatTxt(string NewFile)
        {
            if (File.Exists(NewFile))
            {
                File.Delete(NewFile);
            }
            File.Create(NewFile).Close();
        }

        /// <summary>
        /// 读取当前路径下文本中的内容，并返回一个数组，存放各行的内容
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string[] ReadText(string file)
        {
            string fs = file;
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            String line;
            line = sr.ReadToEnd();
            sr.Close();
            string[] s1 = line.Trim().Split('\n');
            return s1;

        }

        /// <summary>
        /// 保存当前路径下的内容
        /// </summary>
        /// <param name="SaveFile"></param>
        /// <param name="TxtContent"></param>
        public void SaveTxt(string SaveFile, string TxtContent)
        {
            if (File.Exists(SaveFile))  //存在文件即删除
            {
                File.Delete(SaveFile);
            }
            FileStream fs = new FileStream(SaveFile, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            //string Name = System.IO.Path.GetFileName(saveFile);
            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.End);
            string[] S = TxtContent.Trim().Split('\n');
            int length = S.Length;
            for (int i = 0; i < length; i++)
            {
                sw.WriteLine(S[i]);
            }
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// 删除某个Txt文本文件
        /// </summary>
        /// <param name="DelFile"></param>
        public void DeleteTxt(string DelFile)
        {
            if (File.Exists(DelFile))  //存在文件即删除
            {
                File.Delete(DelFile);
            }
        }

        /// <summary>
        /// 删除文件夹以及子文件夹，并返回操作结果。
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public int DeleteDir(string dirPath)
        {
            ErrCode ec = ErrCode.操作完成;
            try
            {
                Directory.Delete(dirPath, true);
            }
            catch (System.IO.PathTooLongException)
            {
                ec = ErrCode.字符串超长;
            }
            catch (System.ArgumentException)
            {
                ec = ErrCode.字符串不合法;
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                ec = ErrCode.指定物理路径不存在;
            }
            catch (System.IO.IOException)
            {
                ec = ErrCode.指定物理位置已经存在具有相同名称的物理文件或文件夹;
            }
            catch (System.UnauthorizedAccessException)
            {
                ec = ErrCode.在指定物理位置不具备相应的物理操作权限;
            }
            return (int)ec;
        }

        /// <summary>
        /// 清除指定文件夹下的所有文件和子文件夹。
        /// </summary>
        /// <param name="dirPath">指定文件夹。</param>
        /// <returns>错误代码。</returns>
        public int ClearDir(string dirPath)
        {
            ErrCode ec = ErrCode.操作完成;

            try
            {
                // 删除文件
                foreach (string file in Directory.GetFiles(dirPath))
                {
                    File.Delete(file);
                }

                // 删除子目录
                foreach (string dir in Directory.GetDirectories(dirPath))
                {
                    Directory.Delete(dir, true);
                }
            }
            catch (System.IO.PathTooLongException)
            {
                ec = ErrCode.字符串超长;
            }
            catch (System.ArgumentException)
            {
                ec = ErrCode.字符串不合法;
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                ec = ErrCode.指定物理路径不存在;
            }
            catch (System.IO.IOException)
            {
                ec = ErrCode.指定物理位置已经存在具有相同名称的物理文件或文件夹;
            }
            catch (System.UnauthorizedAccessException)
            {
                ec = ErrCode.在指定物理位置不具备相应的物理操作权限;
            }

            return (int)ec;
        }

        /// <summary>
        /// 创建文件夹。
        /// </summary>
        /// <param name="dirPath">要创建的文件夹路径。</param>
        /// <returns>错误代码。</returns>
        public static int CreateDir(string dirPath)
        {
            ErrCode ec = ErrCode.操作完成;

            // 分解路径
            string[] ps = dirPath.Split('\\');

            // 逐级创建
            string ph = ps[0];

            for (int i = 1; i < ps.Length; i++)
            {
                ph += @"\" + ps[i];

                if (ExistsDir(ph) == false) // 该文件夹不存在
                {
                    // 创建
                    try
                    {
                        Directory.CreateDirectory(ph);
                    }
                    catch (System.IO.PathTooLongException)
                    {
                        ec = ErrCode.字符串超长;
                    }
                    catch (System.ArgumentException)
                    {
                        ec = ErrCode.字符串不合法;
                    }
                    catch (System.IO.DirectoryNotFoundException)
                    {
                        ec = ErrCode.指定物理路径不存在;
                    }
                    catch (System.IO.IOException)
                    {
                        ec = ErrCode.指定物理位置已经存在具有相同名称的物理文件或文件夹;
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        ec = ErrCode.在指定物理位置不具备相应的物理操作权限;
                    }
                    catch (System.NotSupportedException)
                    {
                        ec = ErrCode.指定物理路径不存在;
                    }
                }
            }
            return (int)ec;
        }

        /// <summary>
        /// 检测指定文件夹路径是否存在。
        /// </summary>
        /// <param name="dirPath">文件夹路径。</param>
        /// <returns>是否存在。</returns>
        public static bool ExistsDir(string dirPath)
        {
            return Directory.Exists(dirPath);
        }

        /// <summary>
        /// 检测指定文件路径是否存在。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <returns>是否存在。</returns>
        public bool ExistsFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 根据给定文件全路径名称获取文件读取流，并返回错误代码。
        /// </summary>
        /// <param name="filePath">给定文件全路径名称。</param>
        /// <param name="fileStream">输出文件读取流。</param>
        /// <returns>错误代码。</returns>
        public static int GetFileReadStream(string filePath, out FileStream fileStream)
        {
            int ec = ErrCode.操作完成.GetHashCode();
            fileStream = null;

            try
            {
                // 打开文件
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                fileStream.Position = 0;
            }
            catch (System.ArgumentNullException)
            {
                ec = ErrCode.指定物理文件不存在.GetHashCode();
            }
            catch (System.ArgumentException)
            {
                ec = ErrCode.指定物理文件不存在.GetHashCode();
            }
            catch (System.NotSupportedException)
            {
                ec = ErrCode.指定物理文件不存在.GetHashCode();
            }
            catch (System.IO.FileNotFoundException)
            {
                ec = ErrCode.指定物理文件不存在.GetHashCode();
            }
            catch (System.IO.IOException)
            {
                ec = ErrCode.指定物理文件获取错误.GetHashCode();
            }
            catch (System.Security.SecurityException)
            {
                ec = ErrCode.在指定物理位置不具备相应的物理操作权限.GetHashCode();
            }
            catch (System.UnauthorizedAccessException)
            {
                ec = ErrCode.在指定物理位置不具备相应的物理操作权限.GetHashCode();
            }

            return ec;
        }

    }

   
}
