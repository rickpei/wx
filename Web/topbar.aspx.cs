using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class Topbar : System.Web.UI.Page
    {
        protected string ShowName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string account = "";
                try
                {
                    account = Request.Cookies["login_account"].Value;
                }
                catch { }
                if (!string.IsNullOrEmpty(account))
                {
                    ShowName = "（" + account + "）";
                }
                else
                {
                    Response.Clear();
                    Response.End();
                }
            }
        }
    }
}