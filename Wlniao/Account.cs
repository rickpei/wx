using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao
{
    public class Account
    {
        public static Boolean Exists(String username)
        {
            if (Wlniao.Model.DB.Account.findByField("AccountUserName", username) == null)
            {
                return Oss.Exists("UsersData/" + username + "/ServiceWeixin.data");
            }
            else
            {
                return true;
            }
        }
        public static Result Add(String username, String password,String email="",String mobile="")
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("用户名未填写");
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Add("登录密码未填写");
            }
            else
            {
                if (password.Length != 32)
                {
                    password = Encryptor.Md5Encryptor32(password, 5);
                }
                try
                {
                    if (Wlniao.Account.Exists(username))
                    {
                        result.Add("用户已存在，禁止重复注册");
                    }
                    else
                    {
                        Wlniao.Model.DB.Account account = new Wlniao.Model.DB.Account();
                        account.AccountUserName = username;
                        account.AccountPassword = password;
                        account.AccountEmail = email;
                        account.AccountMobile = mobile;
                        account.AccountJoinTime = DateTime.Now;
                        account.AccountLastLogin = DateTime.Now;
                        account.IsStop = 0;
                        result.Join(account.insert());
                    }
                }
                catch (Exception ex)
                {
                    result.Add(ex.Message);
                }
            }
            return result;
        }
        public static Wlniao.Model.DB.Account Get(String username)
        {
            try
            {
                Wlniao.Model.DB.Account model = Wlniao.Model.DB.Account.findByField("AccountUserName", username);
                return model;
            }
            catch (Exception ex) { }
            return null;
        }
        public static Result CheckLogin(String username, String password)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("用户名未填写");
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Add("登录密码未填写");
            }
            else
            {
                if (password.Length != 32)
                {
                    password = Encryptor.Md5Encryptor32(password, 5);
                }
                Wlniao.Model.DB.Account account = Wlniao.Model.DB.Account.findByField("AccountUserName", username);
                if (account != null && account.AccountPassword == password)
                {
                    if (account.IsStop != 1)
                    {
                        account.AccountLastLogin = DateTime.Now;
                        db.update(account, "AccountLastLogin");
                    }
                    else
                    {
                        result.Add("帐号“" + account + "”已被禁止登录");
                    }
                }
                else
                {
                    result.Add("用户名或密码错误");
                }
            }
            return result;
        }
        public static Result ChangePwd(String username, String password, String oldpassword = "")
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("用户名未填写");
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Add("登录密码未填写");
            }
            else
            {
                if (password.Length != 32)
                {
                    password = Encryptor.Md5Encryptor32(password, 5);
                }
                if (!string.IsNullOrEmpty(oldpassword) && oldpassword.Length != 32)
                {
                    oldpassword = Encryptor.Md5Encryptor32(oldpassword, 5);
                }
                Wlniao.Model.DB.Account account = Wlniao.Model.DB.Account.findByField("AccountUserName", username);
                if (string.IsNullOrEmpty(oldpassword) || account.AccountPassword == oldpassword)
                {
                    account.AccountPassword = password;
                    db.update(account, "AccountPassword");
                }
                else
                {
                    result.Add("您输入的旧密码不正确");
                }
            }
            return result;
        }
        public static Result ChangeStop(String username)
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    result.Add("用户名未填写");
                }
                else
                {
                    Wlniao.Model.DB.Account account = Wlniao.Model.DB.Account.findByField("AccountUserName", username);
                    if (account != null)
                    {
                        account.IsStop = account.IsStop == 0 ? 1 : 0;
                        db.update(account, "IsStop");
                    }
                    else
                    {
                        result.Add("更改用户状态失败");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add("更改用户状态失败");
            }
            return result;
        }
        public static DataPage<Wlniao.Model.DB.Account> GetPage(string where="",int pageindex = 0, int pagesize = 10)
        {
            try
            {
                return db.findPage<Wlniao.Model.DB.Account>("", pageindex, pagesize);
            }
            catch
            {
                return null;
            }
        }
    }
}
