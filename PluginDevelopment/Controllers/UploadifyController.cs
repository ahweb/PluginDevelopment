using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PluginDevelopment.BLL;
using PluginDevelopment.DAL.DapperDal;
using PluginDevelopment.Helper.RabbitMQ;
using PluginDevelopment.Model;

namespace PluginDevelopment.Controllers
{
    public class UploadifyController : Controller
    {
        private readonly IUserService _userService = Container.Resolve<IUserService>();
        // GET: Uploadify
        public ActionResult Index()
        {
            //List<user> list = _userService.GetModels(p => true).ToList();
            var liset = RabbitMqServer.RabbitMq();
            //DapperMethod.MappingDouble();
            return View();
        }

        public ActionResult AjaxFileUpload()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData == null)
            {
                throw new ArgumentNullException("fileData");
            }
            try
            {
                // 文件上传后的保存路径
                var filePath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                // 原始文件名称
                var fileName = Path.GetFileName(fileData.FileName);
                // 文件扩展名
                var fileExtension = Path.GetExtension(fileName);
                // 保存文件名称
                var saveName = Guid.NewGuid() + fileExtension; 
                fileData.SaveAs(filePath + saveName);

                return Json(new { Success = true, FileName = fileName, SaveName = saveName });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
