using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
namespace Wlniao
{
    /// <summary>
    /// Oss访问（阿里云）
    /// </summary>
    public class Oss
    {
        public class file
        {
            public static bool Exists(string key)
            {
                try
                {
                    key = PathHelper.Map(key);
                    return System.file.Exists(key);
                }
                catch { return false; }
            }
            public static string ReadStr(string key)
            {
                try
                {
                    key = PathHelper.Map(key);
                    return System.file.Read(key);
                }
                catch { return ""; }
            }
        }


        private static string dataUrl = null;

        public static string DataUrl
        {
            get
            {
                if (dataUrl == null)
                {
                    dataUrl = cfgHelper.GetAppSettings("DataUrl");
                    if (string.IsNullOrEmpty(dataUrl))
                    {
                        dataUrl = "";
                    }
                }
                return dataUrl;
            }
        }

        public static bool Exists(string key)
        {
            try
            {
                key = PathHelper.Map(key);
                return System.file.Exists(key);
            }
            catch { return false; }
        }
        public static string ReadStr(string key)
        {
            try
            {
                key = PathHelper.Map(key);
                return System.file.Read(key);
            }
            catch { return ""; }
        }
        public static void Upload(string key, System.IO.Stream content)
        {
            try
            {
                key = PathHelper.Map(key);
                //return System.file.WriteByte(key, content);
            }
            catch { }
        }
        public static void WriteStr(string key, string str)
        {
            try
            {
                key = PathHelper.Map(key);
                System.file.Write(key,str);
            }
            catch
            {
            }
        }
        public static string[] GetFiles(string key)
        {
            try
            {
                key = PathHelper.Map(key);
                string[] files = System.IO.Directory.GetFiles(key);
                return files;
            }
            catch
            {
                return new string[] { };
            }
        }
        public static void Delete(string key)
        {
            try
            {
                key = PathHelper.Map(key);
                System.file.Delete(key);
            }
            catch
            {
            }
        }
        public static void MoveTo(string sourceKey, string destinationKey)
        {
            try
            {
                sourceKey = PathHelper.Map(sourceKey);
                destinationKey = PathHelper.Map(destinationKey);
                System.file.Move(sourceKey, destinationKey);
            }
            catch
            {
            }
        }
        public static void Copy(string sourceKey, string destinationKey)
        {
            try
            {
                sourceKey = PathHelper.Map(sourceKey);
                destinationKey = PathHelper.Map(destinationKey);
                System.file.Copy(sourceKey, destinationKey);
            }
            catch
            {
            }
        }
    }
}
