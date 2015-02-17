using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.Model.DB
{
    public class Notice : ObjectBase<Notice>
    {
        /// <summary>
        /// 公告标题
        /// </summary>
        [Column(Length=120),NotNull("公告标题未填写")]
        public string NoticeTitle { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        [LongText]
        public string NoticeContent { get; set; }
    }
}
