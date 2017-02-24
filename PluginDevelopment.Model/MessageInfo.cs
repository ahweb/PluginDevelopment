using System.Collections.Generic;

namespace PluginDevelopment.Model
{
    /// <summary>
    /// 信息
    /// </summary>
   public class MessageInfo
   {
        /// <summary>
        /// 主键ID
        /// </summary>
       public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
       public string Name { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
       public string Opera { get; set; }

        /// <summary>
        /// 文本编辑ID
        /// </summary>
        public string TextId { get; set; }

   }
}
