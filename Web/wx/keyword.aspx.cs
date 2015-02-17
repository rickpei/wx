using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class KeyWord : PageLogin
    {
        protected string _account = "";
        protected string keyword = "";
        protected string description = "";
        protected string msgtype = "";
        protected string msgmode = "";
        protected string config = "";
        protected string _script = "";
        protected string _scriptint = "";
        protected string _dataurl = Oss.DataUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            string kw = Request["kw"];
            keyword = Request["keyword"];
            description = Request["description"];
            msgtype = Request["msgtype"];
            msgmode = Request["msgmode"];
            config = Request["config"];
            if (string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(kw))
            {
            }
            else
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.Trim();
                }
                if (!string.IsNullOrEmpty(kw))
                {
                    kw = kw.Trim();
                }
                if (Request["method"] == "del")
                {
                    Wlniao.KeyWord.Del(_account, kw);
                    //Response.Redirect("keywords.aspx");
                    _script = "parent.showTips('恭喜你，自动回复规则已删除',4,'/wx/keywords.aspx');";
                }
                else if (Request["method"] == "welcome")
                {
                    if (Wlniao.ServiceWeixin.SetWelcome(_account, "Link:" + kw).IsValid)
                    {
                        //Response.Redirect("keywords.aspx");
                        _script = "parent.showTips('恭喜你，当前规则已设置为首次关注的提醒内容',4,'/wx/keywords.aspx');";
                    }
                }
                else if (Request["method"] == "default")
                {
                    if (Wlniao.ServiceWeixin.SetDefault(_account, "Link:" + kw).IsValid)
                    {
                        //Response.Redirect("keywords.aspx");
                        _script = "parent.showTips('恭喜你，当前规则已设置为默认回复',4,'/wx/keywords.aspx');";
                    }
                }
                else
                {
                    Wlniao.Model.KeyWord model = Wlniao.KeyWord.Get(_account, kw);
                    if (Request["method"] == "save" && !string.IsNullOrEmpty(keyword))
                    {
                        Result rlt = Wlniao.KeyWord.Set(_account, false, keyword, msgtype, description, config, msgmode, kw);
                        if (rlt.IsValid)
                        {
                            //Response.Redirect("keyword.aspx?kw=" + keyword);
                            _script = "parent.showTips('恭喜你，您设置的自动回复规则保存成功',4,'/wx/keywords.aspx?kw=" + strUtil.UrlEncode(keyword) + "');";
                        }
                        else
                        {
                            _script = "parent.showTips('" + rlt.Errors[0] + "',5);";
                        }
                    }
                    else if (model != null)
                    {
                        keyword = model.KeyWords;
                        description = model.Description;
                        msgtype = model.MsgType;
                        msgmode = model.IsHas ? "has" : "equal";
                        config = model.Config;
                    }
                    if (msgtype == "basic")
                    {
                        try
                        {
                            string[] msgs = config.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string msg in msgs)
                            {
                                _scriptint += "txtItemAdd('" + msg.Replace("\'", "\\\'") + "');\n";
                            }
                        }
                        catch { }
                    }
                    else if (msgtype == "api")
                    {
                        try
                        {
                            if (config.ToLower().StartsWith("api:"))
                            {
                                _scriptint += "apiItemSet('" + config.Substring(4) + "');\n";
                            }
                        }
                        catch { }
                    }
                    else if (msgtype == "news")
                    {
                        try
                        {
                            string newstitle = "";
                            string newsdesc = "";
                            string newspicurl = "";
                            string newspiclink = "";
                            string newscontent = "";
                            string[] msgs = config.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                            int i = 0;
                            foreach (string msg in msgs)
                            {
                                string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                                try
                                {
                                    newstitle = kv[0].Replace("'", "\\'");
                                }
                                catch { newstitle = ""; }
                                try
                                {
                                    newsdesc = kv[1].Replace("'", "\\'");
                                }
                                catch { newsdesc = ""; }
                                try
                                {
                                    newspicurl = kv[2].Replace("\\", "/").Replace("'", "\\'");
                                }
                                catch { newspicurl = ""; }
                                try
                                {
                                    newspiclink = kv[3].Replace("\\", "/").Replace("'", "\\'");
                                }
                                catch { newspiclink = ""; }
                                try
                                {
                                    newscontent = kv[4].Replace("'", "\\'");
                                }
                                catch { newscontent = ""; }
                                if (i == 0)
                                {
                                    _scriptint += "newsMainSet('" + newstitle + "','" + newsdesc + "','" + newspicurl + "','" + newspiclink + "','" + newscontent + "');\n";
                                }
                                else
                                {
                                    _scriptint += "newsItemAdd('" + newstitle + "','" + newsdesc + "','" + newspicurl + "','" + newspiclink + "','" + newscontent + "');\n";
                                }
                                i++;
                            }
                        }
                        catch { }
                    }
                    else if (msgtype == "music")
                    {
                        try
                        {
                            string musictitle = "";
                            string musicurl = "";
                            string musichdurl = "";
                            string musicdesc = "";
                            string[] msgs = config.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string msg in msgs)
                            {
                                string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                                if (kv[0] == "musictitle")
                                {
                                    try
                                    {
                                        musictitle = kv[1].Replace("'", "\\'");
                                    }
                                    catch { musictitle = ""; }
                                }
                                else if (kv[0] == "musicurl")
                                {
                                    try
                                    {
                                        musicurl = kv[1].Replace("\\", "/").Replace("'", "\\'");
                                    }
                                    catch { musicurl = ""; }
                                }
                                else if (kv[0] == "musichdurl")
                                {
                                    try
                                    {
                                        musichdurl = kv[1].Replace("\\", "/").Replace("'", "\\'");
                                    }
                                    catch { musichdurl = ""; }
                                }
                                else if (kv[0] == "musicdesc")
                                {
                                    try
                                    {
                                        musicdesc = kv[1].Replace("'", "\\'");
                                    }
                                    catch { musicdesc = ""; }
                                }
                            }
                            _scriptint += "initMusic('" + musictitle + "','" + (musicurl.StartsWith("http://") ? musicurl : Oss.DataUrl + "/" + musicurl) + "','" + (musichdurl.StartsWith("http://") ? musichdurl : Oss.DataUrl + "/" + musichdurl) + "','" + musicdesc + "');";
                        }
                        catch { }
                    }
                }
            }
        }

    }
}