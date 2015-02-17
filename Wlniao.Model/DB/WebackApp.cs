using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.Model.DB
{
    /// <summary>
    /// Weback应用信息
    /// </summary>
    public class WebackApp : ObjectBase<WebackApp>
    {
        /// <summary>
        /// 是否公用APP(为-1时不显示)
        /// </summary>
        public int IsPrivate { get; set; }
        /// <summary>
        /// 私有用户
        /// </summary>
        public string PrivateAccount { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        [Column(Length = 80), NotNull("应用名称未输入")]
        public string AppName { get; set; }
        /// <summary>
        /// 应用简介
        /// </summary>
        [Column(Length = 200)]
        public string AppDescription { get; set; }
        /// <summary>
        /// 应用地址
        /// </summary>
        [Column(Length = 120)]
        public string AppUrl { get; set; }
        /// <summary>
        /// 应用图标地址
        /// </summary>
        [Column(Length = 120)]
        public string AppIcons { get; set; }
        /// <summary>
        /// Appkey
        /// </summary>
        [Column(Length = 50)]
        public string Appkey { get; set; }
        /// <summary>
        /// Secret
        /// </summary>
        [Column(Length = 50)]
        public string Secret { get; set; }
        /// <summary>
        /// App销售价格
        /// </summary>
        public float AppPrice { get; set; }
        /// <summary>
        /// 应用分类
        /// </summary>
        [Column(Length = 50)]
        public string AppType { get; set; }
        /// <summary>
        /// 应用标签
        /// </summary>
        [Column(Length = 50)]
        public string AppTags { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int AppSort { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column(Length = 20)]
        public string JoinTime { get; set; }
    }
}
