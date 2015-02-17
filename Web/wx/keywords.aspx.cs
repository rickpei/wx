using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class Keywords : PageLogin
    {
        protected string _ListStr = "";
        protected string _PageBar = "";
        protected string _Keyword = "";
        protected string fansCount = "0";
        protected DataPage<Wlniao.Model.KeyWord> pager = new DataPage<Model.KeyWord>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pageindex = 1;
                try
                {
                    pageindex = Convert.ToInt32(Request["page"]);
                    if (pageindex <= 0)
                    {
                        pageindex = 1;
                    }
                }
                catch { pageindex = 1; }
                int show = 0;
                if (Request["show"] == "1")
                {
                    show = 1;
                }
                else if (Request["show"] == "2")
                {
                    show = 2;
                }
                pager = Wlniao.KeyWord.GetPage(GetAccountGuid(), pageindex, 10, show);
                if (pager != null)
                {
                    fansCount = pager.RecordCount.ToString();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<ul>");
                    //sb.Append("<li>类型&nbsp;&nbsp;关键字&nbsp;&nbsp;规则的描述<em>推送量</em></li>");
                    if (pager.Results != null)
                    {
                        foreach (var item in pager.Results)
                        {
                            try
                            {
                                switch (item.MsgType)
                                {
                                    case "basic":
                                        item.MsgType = "基本文字回复";
                                        break;
                                    case "news":
                                        item.MsgType = "混合图文回复";
                                        break;
                                    case "music":
                                        item.MsgType = "基本语音回复";
                                        break;
                                    case "menus":
                                        item.MsgType = "自定义会话菜单";
                                        break;
                                    default:
                                        item.MsgType = "其它";
                                        break;
                                }
                                //sb.AppendFormat("<li><a href=\"keyword.aspx?kw={0}\"><font style=\"color:#aaaaaa;font-size:10px;\">[{1}] </font>{0}<i>{2}</i></a><em>{3}</em></li>", item.KeyWords, item.MsgType, item.Description,0);

                                sb.AppendFormat("<div class=\"rule_item clearfix\"><div class=\"rule_content clearfix\"><div class=\"data fl\">" + item.KeyWords + " <span style=\"font-size:12px;\">（{1}）</span></div><div class=\"fr\"><font color=\"gray\">{3}</font>&nbsp;<font color=\"gray\">共{4}次推送</font>&nbsp;&nbsp;<a onclick=\"return confirm('您确定要将当前规则设置为关注时的推送内容吗？');return false;\" href=\"keyword.aspx?method=welcome&kw={0}\">关注推送</a>&nbsp;<a onclick=\"return confirm('您确定要将当前规则设置为默认回复吗？');return false;\" href=\"keyword.aspx?method=default&kw={0}\">设为默认</a>&nbsp;<a href=\"keyword.aspx?kw={0}\">编辑/查看</a>&nbsp;<a onclick=\"return confirm('删除规则将同时删除关键字与回复，确认吗？');return false;\" href=\"keyword.aspx?method=del&kw={0}\">删除</a></div></div><div class=\"rule_desc clearfix\" style=\"height:auto;line-height:25px;\">{2}</div></div>", strUtil.UrlEncode(item.KeyWords), item.MsgType, item.Description, item.IsHas ? "包含" : "等价", item.Push);
                            }
                            catch { }
                        }
                    }
                    //sb.Append("</ul>");
                    _ListStr = sb.ToString();


                    if (pager.PageCount > 1)
                    {
                        _PageBar += "<div class=\"page\">";
                        if (pager.Current == 1)
                        {
                            _PageBar += "<span><上一页</span>";
                        }
                        else
                        {
                            _PageBar += "<a href=\"keywords.aspx?page=" + (pager.Current - 1).ToString() + "\"><上一页></a>";
                        }
                        int i = 1;
                        for (int time = 0; time < 10 && i + time <= pager.PageCount; time++)
                        {
                            if ((i + time) == pager.Current)
                            {
                                _PageBar += "<em>" + (i + time) + "</em>";
                            }
                            else
                            {
                                _PageBar += "<a href=\"keywords.aspx?page=" + (i + time) + "\">" + (i + time) + "</a>";
                            }
                        }
                        if (pager.Current == pager.PageCount)
                        {
                            _PageBar += "<span>下一页></span>";
                        }
                        else
                        {
                            _PageBar += "<a href=\"keywords.aspx?page=" + (pager.Current + 1).ToString() + "\">下一页></a>";
                        }
                        _PageBar += "</div>";
                    }
                }
            }
        }

    }
}