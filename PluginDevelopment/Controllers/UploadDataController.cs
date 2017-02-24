using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using PluginDevelopment.DAL;
using PluginDevelopment.Helper;
using PluginDevelopment.Model;

namespace PluginDevelopment.Controllers
{
    public class UploadDataController : ApiController
    {
        private static readonly string UploadFolder = ConfigurationHelper.GetAppSetting("FilePath");

        //var path = HostingEnvironment.MapPath(UploadFolder);当前项目的地址

        /// <summary>
        /// 根据文件路径获取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(fileName);
            }
            
            if (string.IsNullOrEmpty(UploadFolder))
            {
                throw new ArgumentNullException(UploadFolder);
            }
            HttpResponseMessage result;
            DirectoryInfo directoryInfo = new DirectoryInfo(UploadFolder);
            FileInfo foundFileInfo = directoryInfo.GetFiles().FirstOrDefault(x => x.Name == fileName);
            if (foundFileInfo != null)
            {
                FileStream fs = new FileStream(foundFileInfo.FullName, FileMode.Open);
                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(fs)
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = foundFileInfo.Name
                };
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return result;
        }

        /// <summary>
        /// 获取用户提交的文件上传请求
        /// </summary>
        /// <returns></returns>
        public Task<IQueryable<FileData>> Post()
        {
            try
            {
                if (string.IsNullOrEmpty(UploadFolder))
                {
                    throw new ArgumentNullException(UploadFolder);
                }
                if (!Directory.Exists(UploadFolder))
                {
                    Directory.CreateDirectory(UploadFolder);
                }
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,"此请求格式不正确"));
                }                    
                var streamProvider = new WithExtensionMultipartFormDataStreamProvider(UploadFolder);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }
                    var fileInfo = streamProvider.FileData.Select(i =>
                    {
                        var info = new FileInfo(i.LocalFileName);
                        return new FileData(info.Name, string.Format("{0}?filename={1}", Request.RequestUri.AbsoluteUri, info.Name), (info.Length / 1024).ToString());
                    });
                    return fileInfo.AsQueryable();
                });
                return task;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }     
    }      
}
