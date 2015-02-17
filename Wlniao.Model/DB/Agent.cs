using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.Model.DB
{
    public class Agent : ObjectBase<Agent>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(Length=20),NotNull("用户名未输入"),Unique("用户名已被其它用户使用")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column(Length = 50)]
        public string Password { get; set; }
        /// <summary>
        /// 称呼
        /// </summary>
        [Column(Length =30)]
        public string NickName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [Column(Length = 30)]
        public string Principal { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        [Column(Length = 30)]
        public string IDCard { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        [Column(Length = 80)]
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Column(Length = 30)]
        public string Tel { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        [Column(Length = 20)]
        public string QQ { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column(Length = 20)]
        public string JoinTime { get; set; }
        /// <summary>
        /// 代理商等级 小于零时禁止登录
        /// </summary>
        public int AgentLevel { get; set; }
    }
}
