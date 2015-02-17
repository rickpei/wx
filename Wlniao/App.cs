using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao
{
    public class App
    {
        public static Result NewApp(String appname, String appurl = "#", String appicons = "", float price = 0, bool isprivate = false, string privateaccount = "", Int32 sort = 0)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(appname))
            {
                result.Add("应用名称不能为空，请填写应用名称");
            }
            else if (string.IsNullOrEmpty(appurl))
            {
                result.Add("应用接口地址未填写，请填写");
            }
            else
            {
                try
                {
                    Wlniao.Model.DB.WebackApp app = new Wlniao.Model.DB.WebackApp();
                    app.PrivateAccount = "";
                    app.AppName = appname;
                    app.AppUrl = appurl;
                    app.AppIcons = appicons;
                    app.AppPrice = price;
                    app.IsPrivate = isprivate ? 1 : 0;
                    app.PrivateAccount = privateaccount;
                    app.AppSort = sort;
                    app.Appkey = Encryptor.Md5Encryptor32(appname + Rand.Number(5)).ToLower();
                    app.Secret = Encryptor.Md5Encryptor32(appname + Rand.Number(6)).ToLower();
                    app.JoinTime = DateTools.GetNow().ToString("yyyy年MM月dd日 HH时mm分ss秒");
                    app.insert();
                }
                catch (Exception ex)
                {
                    result.Add(ex.Message);
                }
            }
            return result;
        }
        public static Result NewBaseApp(String appname, String appurl = "#", String appicons = "", float price = 0, String description = "", Int32 sort = 0)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(appname))
            {
                result.Add("应用名称不能为空，请填写应用名称");
            }
            else if (string.IsNullOrEmpty(appurl))
            {
                result.Add("应用接口地址未填写，请填写");
            }
            else
            {
                try
                {
                    Wlniao.Model.DB.WebackApp app = new Wlniao.Model.DB.WebackApp();
                    app.PrivateAccount = "";
                    app.AppName = appname;
                    app.AppDescription = description;
                    app.AppUrl = appurl;
                    app.AppIcons = appicons;
                    app.AppPrice = price;
                    app.IsPrivate = -1;
                    app.PrivateAccount = "";
                    app.AppSort = sort;
                    app.Appkey = Encryptor.Md5Encryptor32(appname + Rand.Number(5)).ToLower();
                    app.Secret = Encryptor.Md5Encryptor32(appname + Rand.Number(6)).ToLower();
                    app.JoinTime = DateTools.GetNow().ToString("yyyy年MM月dd日 HH时mm分ss秒");
                    app.insert();
                }
                catch (Exception ex)
                {
                    result.Add(ex.Message);
                }
            }
            return result;
        }

        public static Wlniao.Model.DB.WebackApp GetApp(string appkey)
        {
            return Wlniao.Model.DB.WebackApp.findByField("Appkey", appkey);
        }
        public static Result UserNewApp(String username, int appid, String periodofvalidity = "", Int32 sort = 0)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("未指定要添加应用的用户");
            }
            else if (appid <= 0)
            {
                result.Add("未指定要添加的APP");
            }
            else
            {
                try
                {
                    var account = Account.Get(username);
                    var webackapp = db.findById<Wlniao.Model.DB.WebackApp>(appid);
                    Wlniao.Model.DB.UserApp ua = new Wlniao.Model.DB.UserApp();
                    ua.AccountId = account.Id;
                    ua.AppId = webackapp.Id;
                    ua.Token = Encryptor.Md5Encryptor32(webackapp.Appkey + username) + ua.AccountId.ToString().PadLeft(8, '0').ToUpper();
                    ua.AppSort = sort;
                    if (string.IsNullOrEmpty(periodofvalidity))
                    {
                        ua.PeriodOfValidity = DateTime.Now.AddMonths(3);
                    }
                    else
                    {
                        ua.PeriodOfValidity = Convert.ToDateTime(periodofvalidity);
                    }
                    ua.insert();
                }
                catch (Exception ex)
                {
                    result.Add(ex.Message);
                }
            }
            return result;
        }
        public static Wlniao.Model.DB.UserApp GetUserApp(int accountid, int appid)
        {
            var userapp = db.find<Wlniao.Model.DB.UserApp>("AccountId=" + accountid + " and AppId=" + appid).first();
            if (userapp != null && userapp.TokenOutTime < DateTime.Now)
            {
                userapp.RandStr = Rand.Str(12).ToUpper();
                userapp.TokenOutTime = DateTime.Now.AddMinutes(30);
                userapp.update(new string[] { "RandStr", "TokenOutTime" });
            }
            return userapp;
        }
        public static Wlniao.Model.DB.UserApp GetUserAppByToken(string token)
        {
            var userapp = db.find<Wlniao.Model.DB.UserApp>("Token='" + token.Substring(0, 40).ToUpper() + "' and RandStr='" + token.Substring(40, 12).ToUpper() + "'").first();
            //if (userapp != null)
            //{
            //    return null;
            //}
            //else
            //{
            //    userapp.TokenOutTime = DateTime.Now.AddMinutes(30);
            //    userapp.update("TokenOutTime");
            //}
            return userapp;
        }
    }
}
