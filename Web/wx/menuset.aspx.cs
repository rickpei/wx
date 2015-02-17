using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class Menuset : PageLogin
    {
        protected string treedata = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            treedata = Wlniao.WXMenu.GetTreeData(GetAccountGuid());
        }
    }

}