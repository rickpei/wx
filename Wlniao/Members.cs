using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao
{
    public class Members
    {
        public static Result Set(String account, String CompanyName, String KeyWords, String Address, String TelPhone, String CardName)
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(CompanyName))
                {
                    result.Add("Sorry，商户名称未填写");
                }
                else if (string.IsNullOrEmpty(KeyWords))
                {
                    result.Add("Sorry,关键字未填写");
                }
                else
                {
                    string keyPath = "UsersData/" + account + "/Members/Company.data";
                    Wlniao.Model.MemberCompany model = Get(account);
                    if (model == null)
                    {
                        model = new Model.MemberCompany();
                    }
                    model.CompanyName = CompanyName;
                    model.Address = Address;
                    model.TelPhone = TelPhone;
                    model.CardName = CardName;
                    string old = model.KeyWords;
                    model.KeyWords = KeyWords;
                    if (!string.IsNullOrEmpty(KeyWords))
                    {
                        result.Join(KeyWord.Set(account, true, KeyWords, "news", "会员卡引导信息（自动生成）", CompanyName + "会员卡#@@#会员卡引导信息#@@#" + "1" + "#@@#" + "2" + "#@@#" + " " + "#@@#", "equal", old));
                    }
                    if (result.IsValid)
                    {
                        string json = Json.ToStringEx(model);
                        Oss.WriteStr(keyPath, json);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
        public static Wlniao.Model.MemberCompany Get(String account)
        {
            try
            {
                string keyPath = "UsersData/" + account + "/Members/Company.data";
                string json = Oss.ReadStr(keyPath);
                Wlniao.Model.MemberCompany model = Json.ToObject<Wlniao.Model.MemberCompany>(json);

                return model;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
