using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.Model.DB
{
    public class Manage : ObjectBase<Manage>
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
        /// 添加时间
        /// </summary>
        [Column(Length = 20)]
        public string JoinTime { get; set; }
    }
}
