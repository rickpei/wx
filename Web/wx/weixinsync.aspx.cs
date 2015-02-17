using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class WeixinSync : PageLogin
    {
        protected string msg = "";
        protected string _script = "";
        protected string _account = "";
        protected string weixinmpaccount = "";
        protected string weixinmppassword = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _script = "";
            _account = GetAccountGuid();
            Wlniao.Model.ServiceWeixin weixin = Wlniao.ServiceWeixin.Get(_account);
            if (weixin == null)
            {
                weixin = new Wlniao.Model.ServiceWeixin();
            }
            weixinmpaccount = Request["weixinmpaccount"];
            weixinmppassword = Request["weixinmppassword"];
            if (string.IsNullOrEmpty(weixinmpaccount))
            {
                weixinmpaccount = weixin.WeixinMpAccount;
            }
            if (string.IsNullOrEmpty(weixinmppassword))
            {
                weixinmppassword = weixin.WeixinMpPassword;
            }
            try
            {
                if (string.IsNullOrEmpty(weixinmpaccount) && string.IsNullOrEmpty(weixinmppassword) || Request["method"]!="save")
                {
                }
                else if (string.IsNullOrEmpty(weixinmpaccount) || string.IsNullOrEmpty(weixinmppassword))
                {
                    msg = "公众帐号登录名或密码未填写！";
                    _script = "<script>parent.showTips('" + msg + "',5);</script>";
                }
                else if (!string.IsNullOrEmpty(weixinmpaccount) && !string.IsNullOrEmpty(weixinmppassword))
                {
                    if (weixinmppassword.Length != 32)
                    {
                        weixinmppassword = Encryptor.Md5Encryptor32(weixinmppassword).ToUpper();
                        Result result = Wlniao.ServiceWeixin.SetMPAccount(_account, weixinmpaccount, weixinmppassword);
                        if (!result.IsValid)
                        {
                            msg = result.Errors[0];
                        }
                        _script = "<script>parent.showTips('您的同步密码已保存',4);</script>";
                    }
                    else
                    {
                        _script = "<script>parent.showTips('您的帐号和密码均未修改，无需保存',4);</script>";
                    }
                }
            }
            catch { }
        }
    }
}