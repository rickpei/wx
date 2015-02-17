using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
namespace Wlniao
{
    public class WxApi
    {
        private static string _ApiUrl;
        public static string ApiUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_ApiUrl))
                {
                    if (HttpContext.Current.Request.Url.Port == 80)
                    {
                        _ApiUrl = "http://" + HttpContext.Current.Request.Url.Host;
                    }
                    else
                    {
                        _ApiUrl = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                    }
                }
                return _ApiUrl;
            }
        }
        /// <summary>
        /// 根据参数和密码生成签名字符串
        /// </summary>
        /// <param name="parameters">API参数</param>
        /// <param name="secret">密码</param>
        /// <returns>签名字符串</returns>
        public static bool CheckSignature(HttpContext context, String WeChatToken)
        {
            return true;
            if (string.IsNullOrEmpty(WeChatToken))
            {
                return true;
            }
            else
            {
                string[] arr = {
					WeChatToken,
					context.Request.QueryString ["timestamp"],
					context.Request.QueryString ["nonce"]
				};
                Array.Sort(arr);     //字典排序
                var s = System.Encryptor.GetSHA1(string.Join("", arr)).ToLower();
                return s == context.Request.QueryString["signature"];
            }
        }
        public static string GetTextByHtml(string htmlStr)
        {
            return strUtil.RemoveHtmlTag(strUtil.HtmlDecode(htmlStr).Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("</p><p>", "\r\n")).Trim();
        }

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="msg"></param>
        public static void ResponseMsg(string msg, string clientUser, string serverUser)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(msg);
                if (doc.InnerText.Length < 50)
                {
                    doc = null;
                }
            }
            catch
            {
                doc = null;
            }
            if (doc == null)
            {
                HttpContext.Current.Response.Write(ResponseTextMsg(msg,clientUser,serverUser));
            }
            else
            {
                HttpContext.Current.Response.Write(doc.InnerXml);
            }
        }

        /// <summary>
        /// 回复文本内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="clientUser">消息来源</param>
        /// <param name="content">消息内容</param>
        /// <returns>生成的输出文本</returns>
        public static string ResponseTextMsg(string content, string clientUser, string serverUser)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", strUtil.RemoveHtmlTag(content));
            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }
        /// <summary>
        /// 回复图文内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="content">消息内容</param>
        /// <returns>生成的输出文本</returns>
        public static string ResponsePicTextMsg(List<RuleContent> articles, string clientUser, string serverUser)
        {
            if (articles == null)
            {
                articles = new List<RuleContent>();
            }
            int count = 0;
            StringBuilder sbItems = new StringBuilder();
            foreach (RuleContent article in articles)
            {
                try
                {
                    //if (string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.PicUrl) || string.IsNullOrEmpty(article.TextContent))
                    //{
                    //    continue;
                    //}
                    if (string.IsNullOrEmpty(article.Title))
                    {
                        continue;
                    }
                    StringBuilder sbTemp = new StringBuilder();
                    sbTemp.AppendFormat("<item>");
                    sbTemp.AppendFormat("   <Title><![CDATA[{0}]]></Title>", article.Title);
                    sbTemp.AppendFormat("   <Description><![CDATA[{0}]]></Description>", GetTextByHtml(article.TextContent));
                    if (!string.IsNullOrEmpty(article.PicUrl))
                    {
                        string urlTemp = article.PicUrl;
                        if (count > 0)
                        {
                            urlTemp = string.IsNullOrEmpty(article.ThumbPicUrl) ? article.PicUrl : article.ThumbPicUrl;
                        }
                        sbTemp.AppendFormat("   <PicUrl><![CDATA[{0}]]></PicUrl>", (!string.IsNullOrEmpty(urlTemp) && urlTemp.Contains("http://")) ? urlTemp : ApiUrl + urlTemp);
                    }
                    if (string.IsNullOrEmpty(article.LinkUrl))
                    {
                        sbTemp.AppendFormat("   <Url><![CDATA[{0}]]></Url>", "");
                    }
                    else
                    {
                        if (!article.LinkUrl.Contains("openid="))
                        {
                            if (article.LinkUrl.Contains("?"))
                            {
                                article.LinkUrl += "&openid=" + clientUser;
                            }
                            else
                            {
                                article.LinkUrl += "?openid=" + clientUser;
                            }
                        }
                        sbTemp.AppendFormat("   <Url><![CDATA[{0}]]></Url>", article.LinkUrl.Contains("http://") ? article.LinkUrl : ApiUrl + article.LinkUrl);
                    }
                    sbTemp.AppendFormat("   <FuncFlag>0</FuncFlag>");
                    sbTemp.AppendFormat("</item>");
                    sbItems.Append(sbTemp.ToString());
                    count++;
                    //更新推送次数
                    if (count == 9)
                    {
                        break;
                    }
                }
                catch
                {
                }
            }


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}></ArticleCount>", count);
            sb.AppendFormat("<Articles>");
            sb.AppendFormat(sbItems.ToString());
            sb.AppendFormat("</Articles>");
            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }
        /// <summary>
        /// 回复音乐内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述信息</param>
        /// <param name="musicurl">音乐链接</param>
        /// <param name="hqmusicurl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <returns>生成的输出文本</returns>
        public static string ResponseMusicMsg(string title, string description, string musicurl, string hqmusicurl, string clientUser, string serverUser)
        {
            if (string.IsNullOrEmpty(hqmusicurl))
            {
                hqmusicurl = musicurl;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
            sb.AppendFormat("<MsgType><![CDATA[music]]></MsgType>");
            sb.AppendFormat("<Music>");
            sb.AppendFormat("   <Title><![CDATA[{0}]]></Title>", title);
            sb.AppendFormat("   <Description><![CDATA[{0}]]></Description>", description);
            sb.AppendFormat("   <MusicUrl><![CDATA[{0}]]></MusicUrl>", (!string.IsNullOrEmpty(musicurl) && musicurl.Contains("http://")) ? musicurl : ApiUrl + musicurl);
            sb.AppendFormat("   <HQMusicUrl><![CDATA[{0}]]></HQMusicUrl>", (!string.IsNullOrEmpty(hqmusicurl) && hqmusicurl.Contains("http://")) ? hqmusicurl : ApiUrl + hqmusicurl);
            sb.AppendFormat("   <FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</Music>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }
    }

    /// <summary>
    /// 规则内容
    /// </summary>
    public class RuleContent
    {
        private string _ContentType;
        /// <summary>
        /// 内容类型 text（文本）,pictext(图文) ,music（音乐）
        /// </summary>
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }
        private string _Title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _LinkUrl;
        /// <summary>
        /// 外链地址
        /// </summary>
        public string LinkUrl
        {
            get { return _LinkUrl; }
            set { _LinkUrl = value; }
        }
        private string _PicUrl;
        /// <summary>
        /// 图片外链地址
        /// </summary>
        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }
        private string _ThumbPicUrl;
        /// <summary>
        /// 小图外链地址
        /// </summary>
        public string ThumbPicUrl
        {
            get { return _ThumbPicUrl; }
            set { _ThumbPicUrl = value; }
        }
        private string _MusicUrl;
        /// <summary>
        /// 声音文件外链地址
        /// </summary>
        public string MusicUrl
        {
            get { return _MusicUrl; }
            set { _MusicUrl = value; }
        }
        private string _TextContent;
        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextContent
        {
            get { return _TextContent; }
            set { _TextContent = value; }
        }
        private int _PushCount;
        /// <summary>
        /// 推送次数
        /// </summary>
        public int PushCount
        {
            get { return _PushCount; }
            set { _PushCount = value; }
        }

        private DateTime _LastStick;
        /// <summary>
        /// 最后置顶的时间
        /// </summary>
        public DateTime LastStick
        {
            get { return _LastStick; }
            set { _LastStick = value; }
        }

    }
}
