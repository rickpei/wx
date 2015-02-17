using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.Model
{
    public class KeyWord
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 内容配置
        /// </summary>
        public string Config { get; set; }
        /// <summary>
        /// 数据文件的Guid
        /// </summary>
        public string GuidContent { get; set; }
        /// <summary>
        /// 是否包含模式
        /// </summary>
        public bool IsHas { get; set; }
        /// <summary>
        /// 是否等价模式
        /// </summary>
        public bool IsEqual { get; set; }
        /// <summary>
        /// 推送次数
        /// </summary>
        public int Push { get; set; }
        /// <summary>
        /// 是否系统定义的
        /// </summary>
        public bool IsSys { get; set; }
    }
}
