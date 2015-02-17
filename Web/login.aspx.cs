using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class Login : System.Web.UI.Page
    {
        protected string msg = "";
        protected string username = "";
        protected string password = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                username = Request["username"];
                password = Request["password"];
                if (string.IsNullOrEmpty(username))
                {
                    msg = "";

                    try
                    {
                        if (!System.Data.KvTableUtil.GetBool("install"))
                        {
                            Wlniao.Model.DB.Account manage = new Wlniao.Model.DB.Account();
                            manage.AccountUserName = "demo";
                            manage.AccountPassword = Encryptor.Md5Encryptor32("123456", 5);
                            manage.AccountMobile = "";
                            manage.AccountJoinTime = DateTime.Now;
                            manage.save();
                            System.Data.KvTableUtil.Save("install", "true");
                        }
                    }
                    catch (Exception ex) { }
                }
                else if (string.IsNullOrEmpty(password))
                {
                    msg = "请填写登录密码";
                }
                else
                {
                    username = username.Trim();
                    password = password.Trim();
                    Result result = Wlniao.Account.CheckLogin(username, password);
                    if (result.IsValid)
                    {
                        Response.Cookies["login_account"].Value = username;
                        Response.Redirect("/default.aspx");
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