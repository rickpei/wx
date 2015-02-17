using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.Model.DB
{
    public class KeyCache : ObjectBase<KeyCache>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(Length = 32), NotNull, Unique]
        public string Md5Key { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [LongText]
        public string StrValue { get; set; }
    }
}
