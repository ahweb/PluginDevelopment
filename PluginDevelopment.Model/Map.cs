using System;

namespace PluginDevelopment.Model
{
   public class Map
    {
        /// <summary>
        /// 主键ID
        /// </summary>
       public string Id { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
       public string Description { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
       public string Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
       public string Latitude { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
       public DateTime OperationDateTime { get; set; }
    }
}
