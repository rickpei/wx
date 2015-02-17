using System;
using System.Collections.Generic;
using System.Text;
namespace Wlniao
{
    public class KeyWord
    {
        public static string[] Separation
        {
            get
            {
                //return new string[] { " ", "#" };
                return new string[] { " " };
            }
        }
        public static Result Set(String account,Boolean issys, String keyword, String msgtype, String description = "", String config = "", String msgmode = "", String old = "")
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/KeyWord/" + keyword + ".data";
                if (old != keyword)
                {

                    if (Oss.Exists(keyPath))
                    {
                        result.Add("您设置的关键字已存在");
                    }
                    else if (!string.IsNullOrEmpty(old) && Oss.Exists("UsersData/" + account + "/KeyWord/" + old + ".data"))
                    {
                        Oss.MoveTo("UsersData/" + account + "/KeyWord/" + old + ".data", keyPath);
                    }
                }
                if (result.IsValid)
                {
                    Wlniao.Model.KeyWord model = Get(account, keyword);
                    if (model == null)
                    {
                        model = new Model.KeyWord();
                    }
                    model.KeyWords = keyword;
                    model.Description = description;
                    model.MsgType = msgtype;
                    model.IsSys = issys;
                    model.IsHas = msgmode == "has";
                    model.IsEqual = msgmode == "equal";
                    if (string.IsNullOrEmpty(model.GuidContent))
                    {
                        model.GuidContent = Guid.NewGuid().ToString();
                    }
                    try
                    {
                        Oss.WriteStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data", config);
                    }
                    catch { }
                    string json = Json.ToStringEx(model);
                    Oss.WriteStr(keyPath, json);
                    try
                    {
                        CacheKeyWord(account);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                result.Add("错误：" + ex.Message);
            }
            return result;
        }
        public static Wlniao.Model.KeyWord Get(String account, String keyword)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/KeyWord/" + keyword + ".data";
                string json = Oss.ReadStr(keyPath);
                Wlniao.Model.KeyWord model = Json.ToObject<Wlniao.Model.KeyWord>(json);
                try
                {
                    model.Config = Oss.ReadStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data");
                }
                catch { }
                return model;
            }
            catch (Exception ex)
            {
                if (ex == null) { }
            }
            return null;
        }
        public static Wlniao.Model.KeyWord GetByText(String account, String text)
        {
            Wlniao.Model.KeyWord model = null;
            try
            {
                string Code = text.Split(Separation, StringSplitOptions.RemoveEmptyEntries)[0];
                string keyPath = "UsersData/" + account + "/KeyWord/" + Code + ".data";
                if (Oss.Exists(keyPath))
                {
                    model = Json.ToObject<Wlniao.Model.KeyWord>(Oss.ReadStr(keyPath));
                }
                else
                {
                    Result result = strUtil.CheckSensitiveWords(text, Oss.ReadStr("UsersData/" + account + "/KeyWord/KeyWord.cache"));
                    if (result.Errors.Count > 0)
                    {
                        foreach (string key in result.Errors)
                        {
                            keyPath = "UsersData/" + account + "/KeyWord/" + key + ".data";
                            if (Oss.Exists(keyPath))
                            {
                                model = Json.ToObject<Wlniao.Model.KeyWord>(Oss.ReadStr(keyPath));
                                if (model != null)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                try
                {
                    model.Config = Oss.ReadStr("UsersData/" + account + "/GuidContent/" + model.GuidContent + ".data");
                }
                catch { }
                try
                {
                    model.Push++;
                    Oss.WriteStr(keyPath, Json.ToStringEx(model));

                    int countD = 0;
                    string fileD = "UsersData/" + account + "/Count/Response/Total.data";
                    try
                    {
                        countD = Convert.ToInt32(Oss.ReadStr(fileD));
                    }
                    catch { }
                    countD++;
                    Oss.WriteStr(fileD, countD.ToString());

                    int countT = 0;
                    string fileT = "UsersData/" + account + "/Count/Response/" + DateTime.Now.ToString("yyMMdd") + ".data";
                    try
                    {
                        countT = Convert.ToInt32(Oss.ReadStr(fileT));
                    }
                    catch { }
                    countT++;
                    Oss.WriteStr(fileT, countT.ToString());
                }
                catch { }
            }
            catch{}
            return model;
        }
        public static Wlniao.Model.KeyWord GetByText(String account, String text,String openid)
        {
            try
            {
                string file = "UsersData/" + account + "/OpenId/" + openid + ".data";
                if (!Oss.Exists(file))
                {
                    Oss.WriteStr(file, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  说:\n") + text);


                    int countD = 0;
                    string fileD = "UsersData/" + account + "/Count/Fans/" + DateTime.Now.ToString("yyMMdd") + ".data";
                    try
                    {
                        countD = Convert.ToInt32(Oss.ReadStr(fileD));
                    }
                    catch { }
                    countD++;
                    Oss.WriteStr(fileD, countD.ToString());


                    int countT = 0;
                    string fileT = "UsersData/" + account + "/Count/Fans/Total.data";
                    try
                    {
                        countT = Convert.ToInt32(Oss.ReadStr(fileT));
                    }
                    catch { }
                    countT++;
                    Oss.WriteStr(fileT, countT.ToString());
                }
                else
                {
                    //System.file.Append(file, DateTime.Now.ToString("\n\nyyyy-MM-dd HH:mm:ss  说:\n") + text);

                    Oss.WriteStr(file, DateTime.Now.ToString("\n\nyyyy-MM-dd HH:mm:ss  说:\n") + text);
                }
            }
            catch { }
            return GetByText(account,text);
        }

        public static Result Del(String account, String keyword)
        {
            Result result = new Result();
            try
            {
                string keyPath = "UsersData/" + account + "/KeyWord/" + keyword + ".data";
                Oss.Delete(keyPath);
                CacheKeyWord(account);
            }
            catch (Exception ex)
            {
                result.Add("错误：" + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 获取关键字回复列表
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="show">0：显示自定义的关键字；1：显示系统关键字；2：显示全部关键字</param>
        /// <returns></returns>
        public static DataPage<Wlniao.Model.KeyWord> GetPage(String account, int pageindex = 0, int pagesize = 10,int show=0)
        {
            try
            {
                var pager = new DataPage<Wlniao.Model.KeyWord>();
                string[] files = Oss.GetFiles("UsersData/" + account + "/KeyWord/");
                var listtemp = new List<string>(files);
                listtemp.Reverse();
                var pagerstr = DataPage<String>.GetPage(listtemp, pagesize, pageindex);
                List<Wlniao.Model.KeyWord> kws = new List<Model.KeyWord>();
                foreach (string file in pagerstr.Results)
                {
                    try
                    {
                        string json = Oss.ReadStr(file);
                        var temp = Json.ToObject<Wlniao.Model.KeyWord>(json);
                        if (temp.IsSys && show==0)
                        {
                            continue;
                        }
                        if (!temp.IsSys && show == 1)
                        {
                            continue;
                        }
                        try
                        {
                            temp.Config = Oss.ReadStr("UsersData/" + account + "/GuidContent/" + temp.GuidContent + ".data");
                        }
                        catch { }
                        kws.Add(temp);
                    }
                    catch (Exception Exception)
                    {
                        if (Exception != null) { }
                    }
                }
                pager.RecordCount = pagerstr.RecordCount;
                pager.Size = pagerstr.Size;
                pager.PageCount = pagerstr.PageCount;
                pager.Current = pageindex;
                pager.Results = kws;
                return pager;
            }
            catch
            {
                return null;
            }
        }

        public static Result CacheKeyWord(String account)
        {
            Result result = new Result();
            try
            {
                string[] files = Oss.GetFiles("UsersData/" + account + "/KeyWord/");
                string cache = "";
                foreach (string file in files)
                {
                    try
                    {
                        var model = Json.ToObject<Wlniao.Model.KeyWord>(Oss.ReadStr(file));
                        if (model.IsHas)
                        {
                            if (!string.IsNullOrEmpty(cache))
                            {
                                cache += "|";
                            }
                            cache += model.KeyWords;
                        }
                    }
                    catch { }
                }
                Oss.WriteStr("UsersData/" + account + "/KeyWord/KeyWord.cache", cache);
            }
            catch { }
            return result;
        }

    }
}
