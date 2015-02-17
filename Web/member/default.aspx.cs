using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Member
{
    public partial class _Default : PageLogin
    {
        protected string _account = "";
        protected string _dataurl = Oss.DataUrl;
        protected string _website = "";
        protected string msg = "";
        protected string _script = "";
        protected string CompanyName = "";
        protected string KeyWords = "";
        protected string Address = "";
        protected string TelPhone = "";
        protected string CardName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _account = GetAccountGuid();
            Wlniao.Model.MemberCompany company = Wlniao.Members.Get(_account);
            if (company == null)
            {
                company = new Wlniao.Model.MemberCompany();
            }
            CompanyName = Request["CompanyName"];
            KeyWords = Request["KeyWords"];
            Address = Request["Address"];
            TelPhone = Request["TelPhone"];
            CardName = Request["CardName"];
            if (string.IsNullOrEmpty(Request["action"]))
            {
                if (string.IsNullOrEmpty(CompanyName))
                {
                    CompanyName = company.CompanyName;
                }
                if (string.IsNullOrEmpty(KeyWords))
                {
                    KeyWords = company.KeyWords;
                }
                if (string.IsNullOrEmpty(Address))
                {
                    Address = company.Address;
                }
                if (string.IsNullOrEmpty(TelPhone))
                {
                    TelPhone = company.TelPhone;
                }
                if (string.IsNullOrEmpty(CardName))
                {
                    CardName = company.CardName;
                }
            }
            if (Request["action"] == "save")
            {
                string url = "";
                if (HttpContext.Current.Request.Url.Port == 80)
                {
                    url = "http://" + HttpContext.Current.Request.Url.Host;
                }
                else
                {
                    url = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                }
                url += "/card.aspx?a=" + _account;
                Result result = Wlniao.Members.Set(_account, CompanyName, KeyWords, Address, TelPhone, CardName);
                if (!result.IsValid)
                {
                    msg = result.Errors[0];
                }
                else
                {
                    _script = "<script>parent.showTips('您的设置保存成功!',4);</script>";
                }
            }
            if (Request.Url.Port == 80)
            {
                _website = Request.Url.Host;
            }
            else
            {
                _website = Request.Url.Host + ":" + Request.Url.Port;
            }
        }
    }
}