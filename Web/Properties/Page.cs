﻿using System;
using System.Collections.Generic;
using System.Web;

namespace Wlniao
{
    public class PageLogin:System.Web.UI.Page
    {
        protected AshxHelper helper = new AshxHelper(HttpContext.Current);
        protected override void OnLoad(EventArgs e)
        {
            if (string.IsNullOrEmpty(GetAccountGuid()))
            {
                if (string.IsNullOrEmpty(helper.GetParam("do")))
                {
                    Response.Clear();
                    Response.Write("<script>top.location.href='/login.aspx';</script>");
                    Response.End();
                }
                else
                {
                    Result result = new Result();
                    result.Add("Sorry,您尚未登录或登录已经超时！");
                    helper.Result = result;
                    helper.ResponseResult();
                }
            }
            else
            {
                base.OnLoad(e);
            }
        }
        protected string GetAccountGuid()
        {
            string account = "";
            try
            {
                account = Request.Cookies["login_account"].Value;
            }
            catch { }
            return account;
        }

    }
    public class MiniPage : System.Web.UI.Page
    {
        private static string _Root = "";
        protected string RootPath
        {
            get
            {
                if (string.IsNullOrEmpty(_Root))
                {
                    _Root = System.Web.Configuration.WebConfigurationManager.AppSettings["DataPath"];
                    if (string.IsNullOrEmpty(_Root))
                    {
                        _Root += AppDomain.CurrentDomain.BaseDirectory + "Data\\";
                    }
                    if (!_Root.EndsWith("\\"))
                    {
                        _Root += "\\";
                    }
                }
                return _Root;
            }
        }
        private static string _DataUrl = Oss.DataUrl;
        protected string DataUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_DataUrl))
                {
                    _DataUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["DataUrl"];
                    if (string.IsNullOrEmpty(_DataUrl))
                    {
                        if (Request.Url.Port == 80)
                        {
                            _DataUrl = "http://" + Request.Url.Host;
                        }
                        else
                        {
                            _DataUrl = "http://" + Request.Url.Host + ":" + Request.Url.Port;
                        }
                    }
                }
                if (!_DataUrl.StartsWith("http://"))
                {
                    _DataUrl = "http://" + _DataUrl;
                }
                return _DataUrl;
            }
        }
        protected string MiniSiteAccount
        {
            get
            {
                return Request["a"];
            }
        }

        protected string CopyRight
        {
            get { return "本功能由Weback提供技术支持"; }
        }
    }
}