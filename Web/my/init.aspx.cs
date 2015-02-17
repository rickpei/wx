using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.My
{
    public partial class Init : PageLogin
    {
        protected string msg = "";
        protected string weixinname = "";
        protected string weixinaccount = "";
        protected string weixintoken = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["do"]))
                {
                    weixinname = Request["weixinname"];
                    weixinaccount = Request["weixinaccount"];
                    weixintoken = Request["weixintoken"];
                    var account = GetAccountGuid();
                    if (string.IsNullOrEmpty(weixinname))
                    {
                        weixinname = account + " 的公众帐号";
                    }
                    if (string.IsNullOrEmpty(weixintoken))
                    {
                        weixintoken = Encryptor.Md5Encryptor32(account).ToLower();
                    }
                    Result result = Wlniao.ServiceWeixin.Save(account, weixinname, weixinaccount, weixintoken);
                    if (result.IsValid)
                    {
                        Response.Redirect("initsuccess.aspx");
                    }
                    else
                    {
                        msg = result.Errors[0];
                    }
                }
            }
        }

    }
}