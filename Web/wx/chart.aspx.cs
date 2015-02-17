using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class Chart : PageLogin
    {
        protected string account;
        protected int[] ints = new int[] { 0, 0, 0, 0 };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    account = GetAccountGuid();
                    ints = Wlniao.ServiceWeixin.GetCount(account);
                }
                catch { }
                if (ints == null || ints.Length != 4)
                {
                    ints = new int[] { 0, 0, 0, 0 };
                }
            }
        }
    }
}