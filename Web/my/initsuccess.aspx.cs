using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.My
{
    public partial class InitSuccess : PageLogin
    {
        protected string _website = "";
        protected string _account = "";
        protected string weixinname = "";
        protected string weixinaccount = "";
        protected string weixintoken = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            Wlniao.Model.ServiceWeixin weixin = ServiceWeixin.Get(_account);
            weixinaccount = weixin.FristAccount;
            weixinname = weixin.WeixinName;
            weixintoken = weixin.Token;
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