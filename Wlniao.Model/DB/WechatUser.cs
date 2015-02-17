using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.Model.DB
{
    public class WechatUser : ObjectBase<WechatUser>
    {
        /// <summary>
        /// FakeID主动推送消息时使用
        /// </summary>
        public string FakeId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// Area
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime JoinTime { get; set; }
    }
}
