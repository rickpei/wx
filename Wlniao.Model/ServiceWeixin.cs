using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.Model
{
    public class ServiceWeixin
    {
        /// <summary>
        /// 微信名称
        /// </summary>
        public string WeixinName { get; set; }
        /// <summary>
        /// 0：订阅号
        /// 1：服务号
        /// </summary>
        public int AccountType { get; set; }
        /// <summary>
        /// 原始帐号
        /// </summary>
        public string FristAccount { get; set; }
        /// <summary>
        /// 易信号
        /// </summary>
        public string Yixinhao { get; set; }
        /// <summary>
        /// 微信Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 欢迎词
        /// </summary>
        public string WelcomeMsg { get; set; }
        /// <summary>
        /// 默认消息
        /// </summary>
        public string DefaultMsg { get; set; }

        /// <summary>
        /// 公众平台Appkey
        /// </summary>
        public string WeixinMpAppkey { get; set; }
        /// <summary>
        /// 公众平台Secret
        /// </summary>
        public string WeixinMpSecret { get; set; }

        /// <summary>
        /// 公众平台帐号
        /// </summary>
        public string WeixinMpAccount { get; set; }
        /// <summary>
        /// 公众平台密码
        /// </summary>
        public string WeixinMpPassword { get; set; }
    }
}
