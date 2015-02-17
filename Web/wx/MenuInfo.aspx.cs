using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class MenuInfo:PageLogin
    {
        public string treeData = "";
        protected string title = "新增菜单";
        protected string nid = "menu_tree_1";
        protected string item_id = "0";
        protected string _KeyWordList = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            item_id = Request.QueryString["item_id"] ?? "0";
            title = item_id == "0" ? "新增菜单" : "编辑菜单";

            nid = Request.QueryString["nid"] ?? "menu_tree_1";

            treeData = Wlniao.WXMenu.GetTreeData(GetAccountGuid());
            try
            {
                DataPage<Wlniao.Model.KeyWord> pager = Wlniao.KeyWord.GetPage(GetAccountGuid(), 0, 20, 2);
                if (pager != null)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    if (pager.Results != null)
                    {
                        foreach (var item in pager.Results)
                        {
                            try
                            {
                                sb.AppendFormat("<option value=\"{0}\">{1}</option>", item.KeyWords, string.IsNullOrEmpty(item.Description) ? item.KeyWords : item.Description);
                            }
                            catch { }
                        }
                    }
                    _KeyWordList = sb.ToString();
                }
            }
            catch { }
        }
    }
}