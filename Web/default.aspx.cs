using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class _Default : PageLogin
    {
        protected string _applist = "";
        protected string _tools = "";
        protected string _account;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _account = GetAccountGuid();
                var account = Wlniao.Model.DB.Account.findByField("AccountUserName", _account);
                #region 加载系统APP
                List<Wlniao.Model.DB.WebackApp> webackapps = db.find<Wlniao.Model.DB.WebackApp>("IsPrivate<0").list();
                if (webackapps != null && webackapps.Count > 0)
                {
                    string icons = "static/icons/app.png";
                    foreach (var item in webackapps)
                    {
                        try
                        {
                            Wlniao.Model.DB.UserApp userapp = Wlniao.App.GetUserApp(account.Id, item.Id);
                            if (userapp == null)
                            {
                                Wlniao.App.UserNewApp(_account, item.Id, DateTime.Now.AddYears(10).ToString("yyyy-MM-dd HH:mm:ss"));
                                userapp = Wlniao.App.GetUserApp(account.Id, item.Id);
                            }
                            if (!string.IsNullOrEmpty(item.AppIcons) && item.AppIcons != "#")
                            {
                                icons = item.AppIcons;
                            }
                            string sessionkey = userapp.Token + userapp.RandStr.ToLower();
                            string onclick = "";
                            if (item.AppUrl.Contains("?"))
                            {
                                onclick = "showMainFrame('" + item.AppUrl + "&sessionkey=" + sessionkey + "')";
                            }
                            else
                            {
                                onclick = "showMainFrame('" + item.AppUrl + "?sessionkey=" + sessionkey + "')";
                            }
                            _tools += string.Format("<li><a href=\"#\" onclick=\"{1}\"><div class=\"toll_img\"><img src=\"{2}\" /><h1>{0}</h1></div><div class=\"toll_info\"><p>{3}</p></div></a></li>", item.AppName, onclick, icons, item.AppDescription);
                        }
                        catch { }
                    }
                }
                #endregion

                #region 加载用户APP
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                List<Wlniao.Model.DB.UserApp> applist = db.find<Wlniao.Model.DB.UserApp>("AccountId=" + account.Id + " order by Id desc").list();
                if (applist != null && applist.Count > 0)
                {
                    string icons = "static/icons/app.png";
                    foreach (var userapp in applist)
                    {
                        try
                        {
                            Wlniao.Model.DB.WebackApp item = db.findById<Wlniao.Model.DB.WebackApp>(userapp.AppId);
                            if (item.IsPrivate >= 0)
                            {
                                string sessionkey = userapp.Token + userapp.RandStr.ToLower();
                                string onclick = "";
                                if (!string.IsNullOrEmpty(item.AppIcons) && item.AppIcons != "#")
                                {
                                    icons = item.AppIcons;
                                }
                                if (item.AppUrl.Contains("?"))
                                {
                                    onclick = "showMainFrame('" + item.AppUrl + "&sessionkey=" + sessionkey + "')";
                                }
                                else
                                {
                                    onclick = "showMainFrame('" + item.AppUrl + "?sessionkey=" + sessionkey + "')";
                                }
                                sb.AppendFormat("<li style=\"background-color:#83BD83;\"><a href=\"#\" onclick=\"{1}\"><div class=\"toll_img\"><img src=\"{2}\" /></div><div class=\"toll_info\"><p>{0}</p></div></a></li>", item.AppName, onclick, icons);
                            }
                        }
                        catch { }
                    }
                    _applist = sb.ToString();
                }
                else
                {
                    _applist = "<div style=\"text-align:center;color:#999;width:600px; height:168px; padding:58px;\">您暂未添加任何应用！<a href=\"#\" onclick=\"showMainFrame('my/addapp.aspx');\">立即添加</a></div>";
                }
                #endregion
            }
        }

    }
}