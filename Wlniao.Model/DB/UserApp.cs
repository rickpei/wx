using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.Model.DB
{
    /// <summary>
    /// 用户的应用关系
    /// </summary>
    public class UserApp : ObjectBase<UserApp>
    {
        /// <summary>
        /// 随机的Token
        /// </summary>
        [Column(Length = 80), NotNull("随机的Token"),Unique("Token不能重复")]
        public string Token { get; set; }
        /// <summary>
        /// 随机的Token验证字符
        /// </summary>
        [Column(Length = 20)]
        public string RandStr { get; set; }
        /// <summary>
        /// Token失效时间(生成后10分钟)
        /// </summary>
        public DateTime TokenOutTime { get; set; }
        /// <summary>
        /// 所属的用户
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 所添加的APP
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int AppSort { get; set; }
        /// <summary>
        /// 有效期（大于2019-12-31即为无限制）
        /// </summary>
        public DateTime PeriodOfValidity { get; set; }
    }
}
