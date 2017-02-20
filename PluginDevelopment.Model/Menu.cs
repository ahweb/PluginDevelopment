using System.Collections.ObjectModel;

namespace PluginDevelopment.Model
{
    /// <summary>
    /// 菜单实体类
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// url路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 子菜单集合
        /// </summary>
        public Collection<Menu> Children { get; set; }
    }
}