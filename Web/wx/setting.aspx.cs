using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class Setting : PageLogin
    {
        protected string msg = "";
        protected string _script = "";
        protected string _website = "";
        protected string _account = "";
        protected string weixinname = "";
        protected string fristaccount = "";
        protected string yixinhao = "";
        protected string token = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _website = "";
            _account = GetAccountGuid();
            Wlniao.Model.ServiceWeixin weixin = Wlniao.ServiceWeixin.Get(_account);
            if (weixin == null)
            {
                weixin = new Wlniao.Model.ServiceWeixin();
            }
            weixinname = Request["weixinname"];
            fristaccount = Request["fristaccount"];
            yixinhao = Request["yixinhao"];
            token = Request["token"];
            if (string.IsNullOrEmpty(Request["method"]))
            {
                if (string.IsNullOrEmpty(weixinname))
                {
                    weixinname = weixin.WeixinName;
                }
                if (string.IsNullOrEmpty(fristaccount))
                {
                    fristaccount = weixin.FristAccount;
                }
                if (string.IsNullOrEmpty(yixinhao))
                {
                    yixinhao = weixin.Yixinhao;
                }
                if (string.IsNullOrEmpty(token))
                {
                    token = weixin.Token;
                }
            }
            if (!string.IsNullOrEmpty(Request["changetoken"]))
            {
                token = Encryptor.Md5Encryptor32(DateTime.Now.Ticks.ToString()).ToLower();
            }
            Result result = Wlniao.ServiceWeixin.Save(_account, weixinname, fristaccount, yixinhao, token);
            if (!result.IsValid)
            {
                msg = result.Errors[0];
                _script = "<script>parent.showTips('" + msg + "',5);</script>";
                //_script = "<script>parent.showTips('" + msg + "',5);self.location.href='setting.aspx';</script>";
            }
            else if (!string.IsNullOrEmpty(Request["changetoken"]))
            {
                _script = "<script>parent.showTips('恭喜你，已为您生成了新的Token',4,'/wx/setting.aspx');</script>";
            }
            else if (!string.IsNullOrEmpty(Request["method"]))
            {
                _script = "<script>parent.showTips('恭喜你，您的设置已保存',4);</script>";
                //_script = "<script>parent.showTips('您的设置已保存',4);self.location.href='setting.aspx';</script>";
            }
            if (Request.Url.Port == 80)
            {
                _website = Request.Url.Host;
            }
            else
            {
                _website = Request.Url.Host + ":" + Request.Url.Port;
            }
        }
    }
}