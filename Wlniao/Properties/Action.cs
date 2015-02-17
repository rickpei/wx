using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
namespace Wlniao
{
    public class DataAction
    {
        private static string _DllName = "";
        private static string _NameSpace = "";
        /// <summary>
        /// 程序集名称
        /// </summary>
        private static string DllName
        {
            get
            {
                if (string.IsNullOrEmpty(_DllName))
                {
                    _DllName = System.Web.Configuration.WebConfigurationManager.AppSettings["DllName"];
                    if (string.IsNullOrEmpty(_DllName))
                    {
                        _DllName = "Wlniao.IO";
                    }
                }
                return _DllName;
            }
        }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        private static string NameSpace
        {
            get
            {
                if (string.IsNullOrEmpty(_NameSpace))
                {
                    _NameSpace = System.Web.Configuration.WebConfigurationManager.AppSettings["NameSpace"];
                    if (string.IsNullOrEmpty(_NameSpace))
                    {
                        _NameSpace = "Wlniao.IO";
                    }
                }
                return _NameSpace;
            }
        }
        public static T Run<T>(string account, string method, params KeyValue[] param)
        {
            if (!string.IsNullOrEmpty(NameSpace))
            {
                method = NameSpace + "." + method;
            }
            String classname = method.Substring(0, method.LastIndexOf('.'));        //获取类名
            String methodname = method.Substring(method.LastIndexOf('.') + 1);      //获取方法名
            ActionBase action;          //声明一个方法
            try
            {
                Type type = Type.GetType(String.Format("{0}, {1}", classname, DllName), false, true);
                action = (ActionBase)Activator.CreateInstance(type);
                action.Account = account;
                action.Params = param;
                var obj = type.InvokeMember(methodname, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase, null, action, new object[] { });
                T result = (T)Convert.ChangeType(obj, typeof(T));
                return result;
            }
            catch { }
            return default(T);
        }
    }
    /// <summary>
    /// 数据访问层的基类
    /// </summary>
    public class ActionBase
    {
        public String Account;
        public KeyValue[] Params;

        /// <summary>
        /// 获得参数对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected Object GetParam(string key)
        {
            if (Params == null)
            {
                return null;
            }
            foreach (KeyValue p in Params)
            {
                if (p.Key.ToLower() == key.ToLower())
                {
                    return p.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 获得整形参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数值</returns>
        protected String GetStrParam(string key)
        {
            try
            {
                return GetParam(key).ToString();
            }
            catch { return ""; }
        }
        /// <summary>
        /// 获得整形参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数值</returns>
        protected Int32 GetIntParam(string key)
        {
            return Convert.ToInt32(GetParam(key));
        }

        /// <summary>
        /// 获得整形参数，如果没有则返回默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected int GetIntParam(string key, int defaultValue)
        {
            return Convert.ToInt32(GetParam(key));
        }

        /// <summary>
        /// 检查需要的参数是否存在
        /// </summary>
        /// <param name="paramArray">参数数组字符串</param>
        /// <returns></returns>
        protected bool CheckRequiredParams(string paramArray)
        {
            string[] parms = paramArray.Split(',');
            for (int i = 0; i < parms.Length; i++)
            {
                if (GetParam(parms[i]) == null || GetParam(parms[i]).ToString().Trim() == string.Empty)
                    return false;
            }
            return true;
        }
    }
    /// <summary>
    /// 数据访问层的基类
    /// </summary>
    public class Action : ActionBase
    {
        private static string _dataPath = "";
        public static string ExtName = ".data";
        public static string DataDir
        {
            get
            {
                if (string.IsNullOrEmpty(_dataPath))
                {
                    _dataPath = System.Web.Configuration.WebConfigurationManager.AppSettings["DataPath"];
                    if (string.IsNullOrEmpty(_dataPath))
                    {
                        _dataPath = AppDomain.CurrentDomain.BaseDirectory + "Data";
                    }
                    if (!_dataPath.EndsWith("\\"))
                    {
                        _dataPath += "\\";
                    }
                }
                return _dataPath;
            }
        }
        protected static string GetBasePath(String Account)
        {
            string path = "" + DataDir;
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            if (!string.IsNullOrEmpty(Account))
            {
                path += "UsersData\\" + Account;
            }
            else
            {
                path += "BaseData";
            }
            return path;
        }

        protected static string JoinPath(string Account, string FileName)
        {
            return GetBasePath(Account) + "\\" + FileName + ExtName;
        }
        protected static Boolean Exists(string FilePath)
        {
            return file.Exists(FilePath.Replace("\\\\", "\\"));
        }
        protected static string Read(string FilePath)
        {
            return file.Read(FilePath.Replace("\\\\", "\\"));
        }
        protected static string GuidContent(string Account, string guid)
        {
            try
            {
                return file.Read(JoinPath(Account,"GuidContent\\" + guid));
            }
            catch
            {
                return "";
            }
        }
        protected static void GuidContent(string Account, string guid, string content)
        {
            file.Write(JoinPath(Account,"GuidContent\\" + guid), content);
        }
        protected static void Write(string FilePath, string Content)
        {
            file.Write(FilePath.Replace("\\\\", "\\"), Content);
        }
        protected static void Append(string FilePath, string Content)
        {
            FilePath = FilePath.Replace("\\\\", "\\");
            if (file.Exists(FilePath))
            {
                file.Append(FilePath, Content);
            }
            else
            {
                file.Write(FilePath, Content, true);
            }
        }
    }

}
