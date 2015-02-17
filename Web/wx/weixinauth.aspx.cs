using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class WeixinAuth : PageLogin
    {
        protected string msg = "";
        protected string _script = "";
        protected string _account = "";
        protected string appid = "";
        protected string appsecret = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _script = "";
            _account = GetAccountGuid();
            Wlniao.Model.ServiceWeixin weixin = Wlniao.ServiceWeixin.Get(_account);
            if (weixin == null)
            {
                weixin = new Wlniao.Model.ServiceWeixin();
            }
            appid = Request["appid"];
            appsecret = Request["appsecret"];
            if (string.IsNullOrEmpty(appid))
            {
                appid = weixin.WeixinMpAppkey;
            }
            if (string.IsNullOrEmpty(appsecret))
            {
                appsecret = weixin.WeixinMpSecret;
            }
            try
            {
                if (string.IsNullOrEmpty(appid) && string.IsNullOrEmpty(appsecret) || Request["method"] != "save")
                {
                }
                else if (string.IsNullOrEmpty(appid) || string.IsNullOrEmpty(appsecret))
                {
                    msg = "AppId或AppSecret未填写！";
                    _script = "<script>parent.showTips('" + msg + "',5);</script>";
                }
                else if (!string.IsNullOrEmpty(appid) && !string.IsNullOrEmpty(appsecret))
                {
                    string token = "";
                    Result result = Web.Class.MP.Init(appid, appsecret, out token);
                    if (result.IsValid)
                    {
                        result.Join(Wlniao.ServiceWeixin.SetMPAppkey(_account, appid, appsecret));
                        _script = "<script>parent.showTips('恭喜你,授权信息已通过验证并保存',4);</script>";
                    }
                    else
                    {
                        msg = result.Errors[0];
                        msg = "AppId或AppSecret错误，设置未保存！";
                        _script = "<script>parent.showTips('" + msg + "',5);</script>";
                    }
                }
            }
            catch { }
        }
    }
}