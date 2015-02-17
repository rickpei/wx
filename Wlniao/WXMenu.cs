using System;
using System.Collections.Generic;
using System.Web;

namespace Wlniao
{
    public class SequenseBll
    {
        public static int GetNewSeed(String account)
        {
            var seed = Oss.ReadStr("UsersData/" + account + "/Count/WXMenuSequense.data");
            return seed.ToInt32() + 1;
        }

        public static void UpdateSeed(String account,int seed)
        {
            Oss.WriteStr("UsersData/" + account + "/Count/WXMenuSequense.data", seed.ToString());
        }
    }
    public class WXMenu
    {
        public static int GetNewSequense(String account)
        {
            return SequenseBll.GetNewSeed(account);
        }

        public static string GetWxMenuData(String account)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/WxMenu.data";
                string json = Oss.ReadStr(keyPath);
                return json;
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        public static string GetTreeData(string account)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/WxMenuTreeData.data";
                string json = Oss.ReadStr(keyPath);
                //return json.Replace("\r", "").Replace("\n", "").Replace("\b", "");
                return json;
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        public static int SaveTreeData(string account, string data)
        {
            string keyPath = "UsersData/" + account + "/WxMenuTreeData.data";
            Oss.WriteStr(keyPath, data);
            return 0;
        }

        public static int SaveWxMenuData(string account, string data)
        {
            string keyPath = "UsersData/" + account + "/WxMenu.data";
            Oss.WriteStr(keyPath, data);
            return 0;
        }
    }

    #region WeixinMenuHelper
    public class WeixinMenuHelper
    {
        #region 由原数据格式解析成微信菜单json格式
        /// <summary>
        /// 由原数据格式解析成微信菜单json格式
        /// </summary>
        /// <param name="data"></param>
        public static string Parse(string data)
        {
            Wlniao.Model.WXMenu menu = new Wlniao.Model.WXMenu();
            //1 反序列化为jsonarray 数组，不过数组集合中始终只有1条对象
            List<Wlniao.Model.WXMenuButton> buttons = Json.ToList<Wlniao.Model.WXMenuButton>(data);
            if (buttons != null && buttons.Count > 0)
            {
                #region Parse 解析成微信 json格式

                for (int i = 0; i < buttons.Count; i++)         //遍历jsonarray               
                {
                    if (buttons[i].children.Count > 0)
                    {
                        //定义包含子菜单的菜单
                        Wlniao.Model.WXMenuComplexButton cxb = new Wlniao.Model.WXMenuComplexButton();
                        cxb.name = buttons[i].text;

                        #region
                        //循环添加用户子菜单项
                        for (int j = 0; j < buttons[i].children.Count; j++)
                        {
                            Wlniao.Model.WXMenuCommonButton cb = new Wlniao.Model.WXMenuCommonButton();
                            cb.name = buttons[i].children[j].text;
                            cb.type = buttons[i].children[j].type;
                            if (cb.type == "click")
                            {
                                cb.key = buttons[i].children[j].key;
                                cb.url = "";
                            }
                            else
                            {
                                cb.key = "";
                                cb.url = buttons[i].children[j].key;
                            }

                            //添加
                            cxb.sub_button.Add(cb);
                        }
                        #endregion

                        //添加进菜单
                        menu.button.Add(cxb);
                    }
                    else
                    {
                        //定义普通菜单并添加菜单
                        Wlniao.Model.WXMenuCommonButton cb = new Wlniao.Model.WXMenuCommonButton();
                        cb.name = buttons[i].text;
                        cb.type = buttons[i].type;
                        if (cb.type == "click")
                        {
                            cb.key = buttons[i].key;
                            cb.url = "";
                        }
                        else
                        {
                            cb.key = "";
                            cb.url = buttons[i].key;
                        }
                        menu.button.Add(cb);
                    }
                }
                #endregion
            }
            var jsondata = Json.ToStringEx(menu);
            return jsondata;
        }
        #endregion
    }
    #endregion

    #region StringExtend
    public static class StringExtend
    {
        public static int ToInt32(this string obj)
        {
            int v = 0;
            if (string.IsNullOrEmpty(obj))
                return v;

            Int32.TryParse(obj, out v);
            return v;
        }
    }
    #endregion
}