using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Wx
{
    public partial class MenuAjax : PageLogin
    {
        private string uid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = GetAccountGuid();
            var context = Context;
            string action = GetRequest("do");
            switch (action)
            {
                case "saveMenuItem":
                    SaveMenuItem(context);
                    break;
                case "delMenuItem":
                    DeleteMenu(context);
                    break;
                case "syncMenu":
                    PublishMenu(context);
                    break;
                case "stopMenu":
                    StopMenu(context);
                    break;
                case "saveTreeData":
                    SaveTree(context);
                    break;
                case "get":
                    GetWxMenuJson(context);
                    break;
                default:
                    GetWxMenuJson(context);
                    break;

            }
            context.Response.End();
        }


        #region GetWxMenuJson
        /// <summary>
        ///获取微信菜单格式信息
        /// </summary>
        /// <param name="context"></param>
        private void GetWxMenuJson(HttpContext context)
        {
            string data =Wlniao.WXMenu.GetWxMenuData(GetAccountGuid());
            context.Response.Write(data);
        }
        #endregion

        #region SaveMenuItem
        /// <summary>
        /// SaveMenuItem
        /// </summary>
        /// <param name="context"></param>
        private void SaveMenuItem(HttpContext context)
        {

            string text = GetRequest("menu_text");
            string type = GetRequest("menu_type");
            string key = GetRequest("menu_key");
            int itemId = GetRequest("item_id").ToInt32();

            string data = "";
            if (string.IsNullOrEmpty(GetRequest("item_id")) || GetRequest("item_id") == "0")
            {
                int sequense = Wlniao.WXMenu.GetNewSequense(uid);
                data = "{\"menu_text\":\"" + text + "\",\"menu_type\":\"" + type + "\",\"menu_key\":\"" + key + "\",\"itemId\":" + sequense + ",\"insert\":true,\"success\":true}";
                SequenseBll.UpdateSeed(uid, sequense);

            }
            else
            {
                data = "{\"menu_text\":\"" + text + "\",\"menu_type\":\"" + type + "\",\"menu_key\":\"" + key + "\",\"itemId\":" + itemId + ",\"insert\":false,\"success\":true}";
            }
            context.Response.Write(data);
        }
        #endregion

        #region SaveData
        /// <summary>
        /// SaveData
        /// </summary>
        /// <param name="context"></param>
        private void SaveTree(HttpContext context)
        {
            int seed = GetRequest("id").ToInt32();
            string treedata = GetRequest("treedata");


            Wlniao.WXMenu.SaveWxMenuData(uid, WeixinMenuHelper.Parse(treedata).Replace(", \"url\":\"\"", "").Replace(", \"key\":\"\"", ""));
            Wlniao.WXMenu.SaveTreeData(uid, treedata);
            context.Response.Write("{\"success\":true}");
        }
        #endregion

        #region DeleteMenu
        /// <summary>
        /// DeleteMenu
        /// </summary>
        /// <param name="context"></param>
        private void DeleteMenu(HttpContext context)
        {
            int id = GetRequest("itemId").ToInt32();
            string treeData = GetRequest("treeData");

            Wlniao.WXMenu.SaveWxMenuData(uid, WeixinMenuHelper.Parse(treeData).Replace(",\"url\":\"\"", "").Replace(",\"key\":\"\"", ""));
            Wlniao.WXMenu.SaveTreeData(uid, treeData);
            context.Response.Write("{\"success\":true}");
        }
        #endregion

        #region PublishMenu
        private void PublishMenu(HttpContext context)
        {
            string json = Wlniao.WXMenu.GetWxMenuData(uid);
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    Wlniao.Model.ServiceWeixin weixin = Wlniao.ServiceWeixin.Get(uid);
                    if (weixin == null)
                    {
                        weixin = new Wlniao.Model.ServiceWeixin();
                    }
                    if (string.IsNullOrEmpty(weixin.WeixinMpAppkey) || string.IsNullOrEmpty(weixin.WeixinMpSecret))
                    {
                        context.Response.Write("{\"success\":false,\"errormsg\":\"你暂未设置授权信息，请先设置授权信息\"}");
                    }
                    else
                    {
                        Result rlt = Web.Class.MP.SyncMenu(weixin.WeixinMpAppkey, weixin.WeixinMpSecret, json);
                        if (rlt.IsValid)
                        {
                            context.Response.Write("{\"success\":true}");
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false,\"errormsg\":\"" + rlt.Errors[0] + "\"}");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                context.Response.Write("{\"success\":false,\"errormsg\":\"错误：" + ex.Message + "\"}");
            }
        }
        #endregion

        #region StopMenu
        private void StopMenu(HttpContext context)
        {
            try
            {
                Wlniao.Model.ServiceWeixin weixin = Wlniao.ServiceWeixin.Get(uid);
                if (weixin == null)
                {
                    weixin = new Wlniao.Model.ServiceWeixin();
                }
                if (string.IsNullOrEmpty(weixin.WeixinMpAppkey) || string.IsNullOrEmpty(weixin.WeixinMpSecret))
                {
                    context.Response.Write("{\"success\":false,\"errormsg\":\"你暂未设置授权信息，请先设置授权信息\"}");
                }
                else
                {
                    Result rlt = Web.Class.MP.DelMenu(weixin.WeixinMpAppkey, weixin.WeixinMpSecret);
                    if (rlt.IsValid)
                    {
                        context.Response.Write("{\"success\":true}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false,\"errormsg\":\"" + rlt.Errors[0] + "\"}");
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"success\":false,\"errormsg\":\"错误：" + ex.Message + "\"}");
            }
        }
        #endregion

        #region utility
        public string GetRequest(string param)
        {
            return HttpContext.Current.Request[param];
            // return HttpContext.Current.Request.QueryString[param];

        }

        public string PostRequest(string param)
        {
            return HttpContext.Current.Request.Form[param];
        }
        #endregion
    }

}