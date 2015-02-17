using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.My
{
    public partial class Create : System.Web.UI.Page
    {
        protected string msg = "";
        protected string username = "";
        protected string password = "";
        protected string repassword = "";
        protected string email = "";
        protected string mobile = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                username = Request["username"];
                password = Request["password"];
                repassword = Request["repassword"];
                email = Request["email"];
                mobile = Request["mobile"];
                if (string.IsNullOrEmpty(username))
                {
                    msg = "";
                }
                else if (string.IsNullOrEmpty(password))
                {
                    msg = "请填设置登录密码";
                }
                else if (password!=repassword)
                {
                    msg = "两次输入的密码不一致";
                }
                else
                {
                    mobile = username;
                    Result result = Wlniao.Account.Add(username, password, email, mobile);
                    if (result.IsValid)
                    {
                        Session["Account"] = username;
                        Response.Redirect("createsuccess.aspx");
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