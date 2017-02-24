using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PluginDevelopment.Model;

namespace PluginDevelopment.DAL
{
    public class ServerFile
    {
        private readonly string _api = "http://localhost:6841/api/files";

        public ServerFile(string apiurl)
        {
            _api = apiurl;
        }
        public IEnumerable<FileData> UploadFiles(params string[] fullFileNames)
        {
            Uri server = new Uri(_api);
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            foreach (var fullfilename in fullFileNames)
            {
                string filename = Path.GetFileName(fullfilename);
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(fullfilename);
                //这里会向服务器上传一个png图片和一个txt文件
                StreamContent streamConent = new StreamContent(new FileStream(fullfilename, FileMode.Open, FileAccess.Read, FileShare.Read));

                multipartFormDataContent.Add(streamConent, filenameWithoutExtension, filename);
            }
            HttpResponseMessage responseMessage = httpClient.PostAsync(server, multipartFormDataContent).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            var hdFiles = JsonConvert.DeserializeObject<IList<FileData>>(content);
            return hdFiles.Count > 0 ? hdFiles : null;
        }

        public bool DownLoad(string serverFileName, string saveFileName)
        {
            Uri server = new Uri(string.Format("{0}?filename={1}", _api, serverFileName));
            HttpClient httpClient = new HttpClient();
            string path = Path.GetDirectoryName(saveFileName);
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(path);
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }                
            HttpResponseMessage responseMessage = httpClient.GetAsync(server).Result;
            if (!responseMessage.IsSuccessStatusCode) return false;
            using (FileStream fs = File.Create(saveFileName))
            {
                Stream streamFromService = responseMessage.Content.ReadAsStreamAsync().Result;
                streamFromService.CopyTo(fs);
                return true;
            }
        }
    }
}
