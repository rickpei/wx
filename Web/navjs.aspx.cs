using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class navjs : System.Web.UI.Page
    {
        protected int _style = 0;
        protected List<Model.MiniNavLink> list = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string account = Session["MiniSiteAccount"].ToString();
                Wlniao.Model.MiniSite site = Wlniao.MiniSite.Get(account);
                _style = site.MiniNav;
                list = Wlniao.MiniSite.GetMiniNavLink(account);
                if (list == null)
                {
                    list = new List<Model.MiniNavLink>();
                }
                if (list.Count < 12)
                {
                    for (int i = list.Count + 1; i <= 12; i++)
                    {
                        Model.MiniNavLink mnl = new Model.MiniNavLink();
                        mnl.Id = i.ToString();
                        mnl.Src = "";
                        mnl.Title = "";
                        mnl.Type = "Link";
                        mnl.Value = "";
                        list.Add(mnl);
                    }
                }
                foreach (var item in list)
                {
                    switch (item.Type.ToLower())
                    {
                        case "tel":
                            item.Value = "tel:" + item.Value;
                            break;
                        case "addfriend":
                            item.Value = "weixin://addfriend/" + item.Value;
                            break;
                        default:
                            if (string.IsNullOrEmpty(item.Value))
                            {
                                item.Value = "#";
                            }
                            break;
                    }
                    if (string.IsNullOrEmpty(item.Title))
                    {
                        item.Title = "&nbsp;";
                        item.Src = "";
                        item.Value = "#";
                    }
                }
            }
            catch { _style = 0; }

        }
    }
}