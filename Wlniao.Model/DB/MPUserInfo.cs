using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.Model.DB
{
    public class MPUserInfo : ObjectBase<MPUserInfo>
    {
        /// <summary>
        /// 帐号Id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// FakeID主动推送消息时使用
        /// </summary>
        public string FakeId { get; set; }
        /// <summary>
        /// OpenId针对每个帐号的唯一标识
        /// </summary>
        [Column(Length = 30)]
        public string OpenId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarkname { get; set; }
        /// <summary>
        /// 微信平台上的分组
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// Weback平台上的分组
        /// </summary>
        public string WeBackGroupId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime JoinTime { get; set; }
    }
}
