using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao
{
    public class ServiceWeixin
    {
        public static Result Save(String account, String name = "", String fristaccount = "", String yixinhao = "", String token = "")
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/ServiceWeixin.data";
                Wlniao.Model.ServiceWeixin serviceWx = Get(account);
                if (serviceWx == null)
                {
                    serviceWx = new Wlniao.Model.ServiceWeixin();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    serviceWx.WeixinName = name;
                }
                if (!string.IsNullOrEmpty(fristaccount))
                {
                    serviceWx.FristAccount = fristaccount;
                }
                if (!string.IsNullOrEmpty(yixinhao))
                {
                    serviceWx.Yixinhao = yixinhao;
                }
                if (!string.IsNullOrEmpty(token))
                {
                    serviceWx.Token = token;
                }
                Oss.WriteStr(keyPath, Json.ToStringEx(serviceWx));
            }
            catch (Exception ex)
            {
                result.Add("错误：" + ex.Message);
            }
            return result;
        }
        public static Wlniao.Model.ServiceWeixin Get(String account)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/ServiceWeixin.data";
                return Json.ToObject<Wlniao.Model.ServiceWeixin>(Oss.ReadStr(keyPath));
            }
            catch (Exception ex){}
            return null;
        }
        public static Result SetWelcome(String account, String msg)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/ServiceWeixin.data";
                Wlniao.Model.ServiceWeixin serviceWx = Get(account);
                serviceWx.WelcomeMsg = msg;
                Oss.WriteStr(keyPath, Json.ToStringEx(serviceWx));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Result SetDefault(String account, String msg)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/ServiceWeixin.data";
                Wlniao.Model.ServiceWeixin serviceWx = Get(account);
                serviceWx.DefaultMsg = msg;
                Oss.WriteStr(keyPath, Json.ToStringEx(serviceWx));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Result SetWelcomeOrDefault(String account, String welcomemsg, String defaultmsg)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/ServiceWeixin.data";
                Wlniao.Model.ServiceWeixin serviceWx = Get(account);
                serviceWx.WelcomeMsg = welcomemsg;
                serviceWx.DefaultMsg = defaultmsg;
                Oss.WriteStr(keyPath, Json.ToStringEx(serviceWx));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Result SetMPAccount(String account, String mpaccount, string mppassword)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/ServiceWeixin.data";
                Wlniao.Model.ServiceWeixin serviceWx = Get(account);
                serviceWx.WeixinMpAccount = mpaccount;
                serviceWx.WeixinMpPassword = mppassword;
                Oss.WriteStr(keyPath, Json.ToStringEx(serviceWx));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Result SetMPAppkey(String account, String mpappkey, string mpsecret)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/ServiceWeixin.data";
                Wlniao.Model.ServiceWeixin serviceWx = Get(account);
                serviceWx.WeixinMpAppkey = mpappkey;
                serviceWx.WeixinMpSecret = mpsecret;
                Oss.WriteStr(keyPath, Json.ToStringEx(serviceWx));
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static int[] GetCount(String account,string day="")
        {
            int[] ints = new int[] { 0, 0, 0, 0 };

            if (string.IsNullOrEmpty(day))
            {
                day = DateTime.Now.ToString("yyMMdd");
            }
            try
            {
                ints[0] = Convert.ToInt32(Oss.ReadStr("UsersData/" + account + "/Count/Fans/" + day + ".data"));
            }
            catch { }
            try
            {
                ints[1] = Convert.ToInt32(Oss.ReadStr("UsersData/" + account + "/Count/Fans/Total.data"));
            }
            catch { }
            try
            {
                ints[2] = Convert.ToInt32(Oss.ReadStr("UsersData/" + account + "/Count/Response/" + day + ".data"));
            }
            catch { }
            try
            {
                ints[3] = Convert.ToInt32(Oss.ReadStr("UsersData/" + account + "/Count/Response/Total.data"));
            }
            catch { }

            return ints;
        }
    }
}
