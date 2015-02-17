using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class ResponseMsg : PageLogin
    {
        protected string _account = "";
        protected string _script = "";
        protected string welcomemsg = "";
        protected string defaultmsg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _account = GetAccountGuid();
                Wlniao.Model.ServiceWeixin weixin = Wlniao.ServiceWeixin.Get(_account);
                if (weixin == null)
                {
                    Response.Redirect("setting.aspx");
                }
                else
                {
                    if (Request["method"] == "save")
                    {
                        welcomemsg = Request["welcome"];
                        defaultmsg = Request["default"];
                        Wlniao.ServiceWeixin.SetWelcomeOrDefault(_account, welcomemsg, defaultmsg);
                        _script = "<script>parent.showTips('恭喜你,操作已保存',4);</script>";
                    }
                    else
                    {
                        welcomemsg = weixin.WelcomeMsg;
                        defaultmsg = weixin.DefaultMsg;
                    }
                }
            }
        }

    }
}