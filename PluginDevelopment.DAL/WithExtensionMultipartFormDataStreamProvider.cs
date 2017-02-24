using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PluginDevelopment.DAL
{
    public class WithExtensionMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public WithExtensionMultipartFormDataStreamProvider(string rootPath)
            : base(rootPath)
        {
        }

        /// <summary>
        /// 获取本地文件名，该文件名将与用于创建存储当前 MIME 正文部分内容的绝对文件名的根路径组合在一起。
        /// </summary>
        /// <param name="headers">内容标题</param>
        /// <returns></returns>
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var extension = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? Path.GetExtension(GetValidFileName(headers.ContentDisposition.FileName)) : "";
            return Guid.NewGuid() + extension;
        }

        private string GetValidFileName(string filePath)
        {
            char[] invalids = Path.GetInvalidFileNameChars();
            return string.Join("_", filePath.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
        }
    }
}
