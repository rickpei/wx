using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.CMS
{
    public partial class Nav : PageLogin
    {
        protected string _account = "";
        protected string _styleList = "";
        protected string msg = "";
        protected string _scripttips = "";
        protected string _dataurl = Oss.DataUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            if (string.IsNullOrEmpty(Request["style"]))
            {
                Wlniao.Model.MiniSite site = Wlniao.MiniSite.Get(_account);
                if (site == null)
                {
                    Wlniao.MiniSite.SetMiniNav(_account, 0);
                    Response.Redirect("nav.aspx");
                }
                else
                {
                    List<System.Data.KeyValue> xplist = new List<System.Data.KeyValue>();
                    xplist.Add(System.Data.KeyValue.Create("关闭微导航", 0));
                    xplist.Add(System.Data.KeyValue.Create("风格一", 1));
                    xplist.Add(System.Data.KeyValue.Create("风格二", 2));
                    foreach (var item in xplist)
                    {
                        if (item.Value == site.MiniNav.ToString())
                        {
                            _styleList += "<option value=\"" + item.Value + "\" selected=\"selected\">" + item.Key + "</option>";
                        }
                        else
                        {
                            _styleList += "<option value=\"" + item.Value + "\">" + item.Key + "</option>";
                        }
                    }
                }
            }
            else
            {
                try
                {
                    Wlniao.MiniSite.SetMiniNav(_account, int.Parse(Request["style"]));
                    if (Request["style"] == "0")
                    {
                        _scripttips = "<script>parent.showTips('恭喜你,微导航功能已关闭',4,'/cms/nav.aspx');</script>";
                    }
                    else
                    {
                        _scripttips = "<script>parent.showTips('恭喜你,微导航风格应用成功',4,'/cms/navset.aspx');</script>";
                    }
                }
                catch(Exception ex)
                {
                    msg = "错误：" + ex.Message;
                }
            }
        }
    }
}