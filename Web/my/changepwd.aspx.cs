using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.My
{
    public partial class ChangePwd : Wlniao.PageLogin
    {
        protected string msg = "";
        protected string oldpassword = "";
        protected string newpassword = "";
        protected string repassword = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oldpassword = Request["oldpassword"];
                newpassword = Request["newpassword"];
                repassword = Request["repassword"];
                if (string.IsNullOrEmpty(oldpassword))
                {
                    msg = "";
                }
                else if (string.IsNullOrEmpty(newpassword))
                {
                    msg = "请填写一个新密码";
                }
                else if (newpassword !=repassword)
                {
                    msg = "两次输入的密码不一致";
                }
                else
                {
                    Result result = Wlniao.Account.ChangePwd(GetAccountGuid(), newpassword, oldpassword);
                    if (result.IsValid)
                    {
                        Response.Redirect("changepwdsuccess.aspx");
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