namespace PluginDevelopment.Model
{
    /// <summary>
    /// 文件上传
    /// </summary>
   public class FileData
    {
        public FileData(string name, string url, string size)
        {
            Name = name;
            Url = url;
            Size = size;
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string Size { get; set; }
    }
}
