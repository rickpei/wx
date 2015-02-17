using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.Model.DB
{
    public class Account : ObjectBase<Account>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(Length=20),NotNull("用户名未输入"),Unique("用户名已存在,请更换用户名后再试")]
        public string AccountUserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string AccountPassword { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AccountJoinTime { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime AccountLastLogin { get; set; }
        /// <summary>
        /// 用户是否禁止登录
        /// </summary>
        public int IsStop { get; set; }

        /// <summary>
        /// 所属代理商
        /// </summary>
        public int AgentId { get; set; }
        /// <summary>
        /// 所属代理商名称
        /// </summary>
        [Column(Length = 50)]
        public string AgentName { get; set; }
        /// <summary>
        /// 代理商写入的备注
        /// </summary>
        [LongText]
        public string AgentMemo { get; set; }

        /// <summary>
        /// 微信名称
        /// </summary>
        [Column(Length = 50)]
        public string WeixinName { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        [Column(Length = 50)]
        public string AccountEmail { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Column(Length = 50)]
        public string AccountMobile { get; set; }
    }
}
