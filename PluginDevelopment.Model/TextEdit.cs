using System;

namespace PluginDevelopment.Model
{
    /// <summary>
    /// 文本编辑
    /// </summary>
   public class TextEdit
    {
        /// <summary>
        /// 主键ID
        /// </summary>
       public string TextId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
       public string Category { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
       public DateTime EntryDate { get; set; }

        public byte[] EntryContent { get; set; }

    }
}
