using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class wxapi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string acc = Request["a"];
            string qd = "wx";
            #region 获取帐号和渠道信息  开始
            if (string.IsNullOrEmpty(acc))
            {
                acc = Request["wx"];
            }
            try
            {
                string[] _t = acc.Split('#');
                acc = _t[0];
                if (!string.IsNullOrEmpty(_t[1]))
                {
                    qd = _t[1];
                }
            }
            catch { }
            #endregion 获取帐号和渠道信息 结束
            var account = Wlniao.Model.DB.Account.findByField("AccountUserName", acc);
            if (account != null)
            {
                Wlniao.Model.ServiceWeixin serverWx = Wlniao.ServiceWeixin.Get(account.AccountUserName);
                if (serverWx != null)
                {
                    try
                    {
                        #region 开始解析Post过来的数据
                        var document = new StreamReader(Request.InputStream).ReadToEnd();
                        //声明一个XMLDoc文档对象，LOAD（）xml字符串
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(document);
                        var serverAccount = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();
                        var clientOpenId = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                        string MsgType = doc.GetElementsByTagName("MsgType")[0].InnerText.Trim();
                        string MsgId = "", Event = "", Content = "";
                        try
                        {
                            MsgId = doc.GetElementsByTagName("MsgId")[0].InnerText;
                        }
                        catch { }
                        try
                        {
                            Content = doc.GetElementsByTagName("Content")[0].InnerText;
                        }
                        catch { }
                        try
                        {
                            Event = doc.GetElementsByTagName("Event")[0].InnerText;
                            if (string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Event))
                            {
                                if (Event.ToUpper() == "CLICK")
                                {
                                    Content = doc.GetElementsByTagName("EventKey")[0].InnerText;
                                }
                                else
                                {
                                    Content = Event;
                                }
                            }
                        }
                        catch { }
                        #endregion 数据解析结束

                        bool allowGoto = true;
                        string extUrl = "";
                    startDo:    //消息处理开始处
                        switch (MsgType.ToLower())
                        {
                            case "event":
                                if (Content == "subscribe")
                                {
                                    if (serverWx.WelcomeMsg.StartsWith("Link:") && allowGoto)
                                    {
                                        allowGoto = false;
                                        MsgType = "text";
                                        Content = serverWx.WelcomeMsg.Replace("Link:", "");
                                        goto startDo;   //跳转至消息处理开始处
                                    }
                                    else if (serverWx.WelcomeMsg.StartsWith("App:") && allowGoto)
                                    {
                                        allowGoto = false;
                                        MsgType = "app";
                                        extUrl = serverWx.WelcomeMsg.Replace("App:", "");
                                        goto startDo;   //跳转至消息处理开始处
                                    }
                                    else if (serverWx.WelcomeMsg.StartsWith("Api:") && allowGoto)
                                    {
                                        allowGoto = false;
                                        MsgType = "api";
                                        extUrl = serverWx.WelcomeMsg.Replace("Api:", "");
                                        goto startDo;   //跳转至消息处理开始处
                                    }
                                    else
                                    {
                                        Wlniao.WxApi.ResponseMsg(strUtil.HtmlDecode(serverWx.WelcomeMsg), clientOpenId, serverAccount);
                                    }
                                }
                                else
                                {
                                    allowGoto = true;
                                    MsgType = "text";
                                    goto startDo;
                                }
                                break;
                            case "text":
                                //Wlniao.Weixin.SaveRequest(account, clientUser, serverUser, MsgType, Content);
                                var keyword = Wlniao.KeyWord.GetByText(account.AccountUserName, Content, clientOpenId);
                                if (keyword == null)
                                {
                                    allowGoto = true;
                                    MsgType = "default";
                                    goto startDo;
                                }
                                else if (keyword.MsgType == "api")
                                {
                                    allowGoto = false;
                                    MsgType = "api";
                                    extUrl = keyword.Config.Replace("Api:", "");
                                    goto startDo;   //跳转至消息处理开始处
                                }
                                else if (keyword.MsgType == "basic")
                                {
                                    try
                                    {
                                        string[] msgs = keyword.Config.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                                        string msg = strUtil.HtmlDecode(msgs[new Random().Next(msgs.Length)]);
                                        if (msg.StartsWith("App:") && allowGoto)
                                        {
                                            allowGoto = false;
                                            MsgType = "app";
                                            extUrl = msg.Replace("App:", "");
                                            goto startDo;   //跳转至消息处理开始处
                                        }
                                        else if (msg.StartsWith("Api:") && allowGoto)
                                        {
                                            allowGoto = false;
                                            MsgType = "api";
                                            extUrl = msg.Replace("Api:", "");
                                            goto startDo;   //跳转至消息处理开始处
                                        }
                                        else
                                        {
                                            Wlniao.WxApi.ResponseMsg(msg.Replace("<br>", "\n").Replace("<br/>", "\n"), clientOpenId, serverAccount);
                                        }
                                    }
                                    catch { }
                                }
                                else if (keyword.MsgType == "music")
                                {
                                    #region 音乐回复
                                    string musictitle = "";
                                    string musicurl = "";
                                    string musichdurl = "";
                                    string musicdesc = "";
                                    string[] msgs = keyword.Config.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string msg in msgs)
                                    {
                                        string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                                        if (kv[0] == "musictitle")
                                        {
                                            try
                                            {
                                                musictitle = kv[1];
                                            }
                                            catch { musictitle = ""; }
                                        }
                                        else if (kv[0] == "musicurl")
                                        {
                                            try
                                            {
                                                musicurl = kv[1].Replace("\\", "/");
                                            }
                                            catch { musicurl = ""; }
                                        }
                                        else if (kv[0] == "musichdurl")
                                        {
                                            try
                                            {
                                                musichdurl = kv[1].Replace("\\", "/");
                                            }
                                            catch { musichdurl = ""; }
                                        }
                                        else if (kv[0] == "musicdesc")
                                        {
                                            try
                                            {
                                                musicdesc = kv[1];
                                            }
                                            catch { musicdesc = ""; }
                                        }
                                    }
                                    Wlniao.WxApi.ResponseMsg(Wlniao.WxApi.ResponseMusicMsg(musictitle, musicdesc, musicurl, musichdurl, clientOpenId, serverAccount), clientOpenId, serverAccount);
                                    #endregion
                                }
                                else if (keyword.MsgType == "news")
                                {
                                    #region 图文回复
                                    string newstitle = "";
                                    string newsdesc = "";
                                    string newspicurl = "";
                                    string newspiclink = "";
                                    string newscontent = "";
                                    string[] msgs = keyword.Config.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                                    int i = 0;
                                    List<RuleContent> listTemp = new List<RuleContent>();
                                    foreach (string msg in msgs)
                                    {
                                        string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                                        try
                                        {
                                            newstitle = strUtil.HtmlDecode(kv[0]).Replace("'", "\\'");
                                        }
                                        catch { newstitle = ""; }
                                        try
                                        {
                                            newsdesc = strUtil.HtmlDecode(kv[1]).Replace("'", "\\'");
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
                                            newscontent = strUtil.HtmlDecode(kv[4]).Replace("'", "\\'");
                                        }
                                        catch { newscontent = ""; }
                                        RuleContent rc = new RuleContent();
                                        rc.Title = newstitle;
                                        rc.ContentType = "pictext";
                                        if (string.IsNullOrEmpty(newspiclink) || newspiclink == "#")
                                        {
                                            newspiclink = "/cms.aspx?kw=" + strUtil.UrlEncode(keyword.KeyWords) + "&t=" + strUtil.UrlEncode(newstitle) + "&a=" + account.AccountUserName;
                                            newscontent = strUtil.Ellipsis(strUtil.RemoveHtmlTag(newscontent), 200, "...");
                                        }
                                        rc.LinkUrl = newspiclink;
                                        rc.PicUrl = newspicurl;
                                        rc.ThumbPicUrl = newspicurl;
                                        rc.TextContent = newscontent;
                                        listTemp.Add(rc);
                                        i++;
                                    }
                                    Wlniao.WxApi.ResponseMsg(Wlniao.WxApi.ResponsePicTextMsg(listTemp, clientOpenId, serverAccount), clientOpenId, serverAccount);
                                    #endregion
                                }
                                break;
                            //case "image":   //图片消息
                            //    Wlniao.Weixin.SaveRequest(account, clientUser, weixinFristAccount, MsgType, PostAndGet.GetResponseString(doc.GetElementsByTagName("PicUrl")[0].InnerText), MsgId);
                            //    Wlniao.WxApi.ResponseMsg("Image Ok", clientUser, weixinFristAccount);
                            //    break;
                            //case "voice":   //语音消息
                            //    Wlniao.Weixin.SaveRequest(account, clientUser, weixinFristAccount, MsgType, "MediaId:" + doc.GetElementsByTagName("MediaId")[0].InnerText);
                            //    break;
                            //case "location":   //地理位置
                            //    Wlniao.Weixin.SaveRequest(account, clientUser, weixinFristAccount, MsgType, string.Format("Location_X:{0},Location_Y:{1},Scale:{2}", doc.GetElementsByTagName("Location_X")[0].InnerText, doc.GetElementsByTagName("Location_Y")[0].InnerText, doc.GetElementsByTagName("Scale")[0].InnerText));
                            //    break;
                            case "app":   //转发到APP
                                string appResult = GetFromUrl(extUrl, Content, account.Id.ToString(), serverAccount, clientOpenId);
                                Wlniao.WxApi.ResponseMsg(appResult, clientOpenId, serverAccount);
                                break;
                            case "api":   //转发到API
                                string apiResult = GetFromUrl(extUrl, Content, account.Id.ToString(), serverAccount, clientOpenId, serverWx.Token, serverWx.FristAccount);
                                Wlniao.WxApi.ResponseMsg(apiResult, clientOpenId, serverAccount);
                                break;
                            default:
                                if (serverWx.DefaultMsg.StartsWith("Link:") && allowGoto)
                                {
                                    allowGoto = false;
                                    MsgType = "text";
                                    Content = serverWx.DefaultMsg.Replace("Link:", "");
                                    goto startDo;
                                }
                                else if (serverWx.DefaultMsg.StartsWith("App:") && allowGoto)
                                {
                                    allowGoto = false;
                                    MsgType = "app";
                                    extUrl = serverWx.DefaultMsg.Replace("App:", "");
                                    goto startDo;   //跳转至消息处理开始处
                                }
                                else if (serverWx.DefaultMsg.StartsWith("Api:") && allowGoto)
                                {
                                    allowGoto = false;
                                    MsgType = "api";
                                    extUrl = serverWx.DefaultMsg.Replace("Api:", "");
                                    goto startDo;   //跳转至消息处理开始处
                                }
                                else if (!string.IsNullOrEmpty(serverWx.DefaultMsg))
                                {
                                    Wlniao.WxApi.ResponseMsg(strUtil.HtmlDecode(serverWx.DefaultMsg), clientOpenId, serverAccount);
                                }
                                else
                                {
                                    allowGoto = false;
                                    MsgType = "app";
                                    extUrl = "http://public.api.weback.cn/api.aspx";   //公共互联网接入APP接口
                                    goto startDo;   //跳转至消息处理开始处
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Wlniao.WxApi.CheckSignature(Context, serverWx.Token))
                        {
                            Response.Write(Request.QueryString["echostr"]);
                        }
                        else
                        {
                            Response.Write("Token签名错误");
                        }
                    }
                }
                else
                {
                    Response.Write("当前用户暂未启用Weback服务!");
                }
            }
            else
            {
                Response.Write("您正在使用的API地址有误!");
            }
            Response.End();
        }

        private String GetFromUrl(string apiurl, string content, string accountid = "", string firstid = "", string clientUser = "", string token = "", string setfirstid = "")
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(token))
            {
                sb.AppendFormat("<xml>");
                sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", string.IsNullOrEmpty(setfirstid) ? firstid : setfirstid);
                sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", clientUser);
                sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTools.GetNow().Ticks);
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", content);
                sb.AppendFormat("</xml>");

                string paramstr = "";
                if (apiurl.IndexOf("signature") <= 0)
                {
                    string timestamp = (DateTime.Now.Ticks / 1000000).ToString();
                    string[] arr = { token, timestamp, timestamp };
                    Array.Sort(arr);     //字典排序
                    paramstr = "signature=" + System.Encryptor.GetSHA1(string.Join("", arr)).ToLower() + "&timestamp=" + timestamp + "&nonce=" + timestamp;
                }
                if (!string.IsNullOrEmpty(paramstr))
                {
                    if (apiurl.IndexOf('?') > 0)
                    {
                        apiurl += "&" + paramstr;
                    }
                    else
                    {
                        apiurl += "?" + paramstr;
                    }
                }
            }
            else if (apiurl.IndexOf('?') > 0)
            {
                apiurl += "&content=" + content + "&accountid=" + accountid + "&firstid=" + firstid + "&openid=" + clientUser;
            }
            else
            {
                apiurl += "?content=" + content + "&accountid=" + accountid + "&firstid=" + firstid + "&openid=" + clientUser;
            }

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(apiurl);
            request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = sb.Length;
            //request.Timeout = 20000;
            System.Net.HttpWebResponse response = null;
            System.Text.Encoding encode = System.Text.Encoding.UTF8;
            try
            {
                StreamWriter swRequestWriter = new StreamWriter(request.GetRequestStream());
                swRequestWriter.Write(sb.ToString());
                try
                {
                    if (swRequestWriter != null)
                        swRequestWriter.Close();
                }
                catch { }
                response = (System.Net.HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encode))
                {
                    string msg = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(setfirstid))
                    {
                        msg = msg.Replace(setfirstid, firstid);
                    }
                    return msg;
                }
            }
            catch(Exception ex)
            {
                return "Sorry，获取数据失败！";
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
    }
}