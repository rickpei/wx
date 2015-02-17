using System;
using System.Collections.Generic;
using System.Text;
namespace Wlniao
{
    public class MPUserInfo
    {
        public static void Check(String account, String fakeid, String nickName, String remarkName = "", String openid = "", String groupid = "")
        {
            var acc = db.find<Wlniao.Model.DB.Account>("AccountUserName='" + account + "'").first();
            Check(acc.Id, fakeid, nickName, remarkName, openid, groupid);
        }
        public static void Check(Int32 account, String fakeid, String nickName, String remarkName = "", String openid = "", String groupid = "")
        {
            try
            {
                var wechat = Model.DB.WechatUser.findByField("FakeId", fakeid);
                if (wechat == null)
                {
                    wechat = new Model.DB.WechatUser();
                    wechat.FakeId = fakeid;
                    wechat.NickName = nickName;
                    wechat.JoinTime = DateTime.Now;
                    wechat.Area = "";
                    wechat.MobileNumber = "";
                    wechat.QQ = "";
                    wechat.Email = "";
                    wechat.insert();
                }
                if (wechat.NickName != remarkName)
                {
                    wechat.update("NickName");
                }
                var mpuser = Get(account, fakeid);
                if (mpuser == null)
                {
                    mpuser = new Model.DB.MPUserInfo();
                    mpuser.AccountId = account;
                    mpuser.FakeId = wechat.FakeId;
                    mpuser.JoinTime = DateTime.Now;
                    mpuser.insert();
                }
                if (!string.IsNullOrEmpty(openid))
                {
                    mpuser.OpenId = openid;
                }
                if (!string.IsNullOrEmpty(nickName))
                {
                    mpuser.NickName = nickName;
                }
                if (!string.IsNullOrEmpty(remarkName))
                {
                    mpuser.Remarkname = remarkName;
                }
                if (!string.IsNullOrEmpty(groupid))
                {
                    mpuser.GroupId = groupid;
                }
                mpuser.update(new string[] { "OpenId", "NickName", "Remarkname", "GroupId" });
            }
            catch { }
        }
        public static Wlniao.Model.DB.MPUserInfo Get(String account, String fakeid)
        {
            try
            {
                var acc = db.find<Wlniao.Model.DB.Account>("AccountUserName='" + account + "'").first();
                return Get(acc.Id, fakeid);
            }
            catch (Exception ex){ }
            return null;
        }
        public static Wlniao.Model.DB.MPUserInfo Get(Int32 account, String fakeid)
        {
            try
            {
                return db.find<Wlniao.Model.DB.MPUserInfo>("AccountId=" + account + " and FakeId=" + fakeid).first();
            }
            catch (Exception ex) { }
            return null;
        }
        public static DataPage<Wlniao.Model.DB.MPUserInfo> GetPage(String account, int pageindex = 0, int pagesize = 10)
        {
            var acc = db.find<Wlniao.Model.DB.Account>("AccountUserName='" + account + "'").first();
            return db.findPage<Wlniao.Model.DB.MPUserInfo>("1=1 and AccountId=" + acc.Id.ToString() + " order by JoinTime", pageindex, pagesize);
        }
    }
}
