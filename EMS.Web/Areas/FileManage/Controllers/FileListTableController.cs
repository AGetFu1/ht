using EMS.Application.ShiftManage;
using EMS.Code;
using EMS.Code.Excel;
using System;
using System.Collections.Generic;
using EMS.Domain.Entity.ShiftManage;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using EMS.Code.Ftp;
using System.IO;
using EMS.Application.ExceptionManage;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace EMS.Web.Areas.FileManage.Controllers
{    
    public class FileListTableController : ControllerBase
    {
        FileListTableApp fileListTableApp = new FileListTableApp();
        FileItemApp fileItemApp = new FileItemApp();
        [HttpGet]
        public virtual ActionResult LoadFile()
        {
            return View();
        }
        public ActionResult GetGridJson(string itemId, Pagination pagination, string queryJson)
        {
            //var data = fileListTableApp.getListJson(itemId,pagination, queryJson);
            var data = new
            {
                rows = fileListTableApp.getListJson(pagination, itemId, queryJson),//数据
                total = pagination.total,//总页数
                page = pagination.page,//当前页数
                records = pagination.records//数据总条数
            };
            return Content(data.ToJson());
        } 
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = fileListTableApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult SubmitForm(FileListTableEntity fileListTableEntity, string keyValue)
        {
                  
            fileListTableApp.SubmitForm(fileListTableEntity, keyValue);
            return Success("操作成功。");
        }
        public ActionResult Form()
        {
            return View();
        }
        public ActionResult UploadFileJson(string keyValue)
        {
            FtpHelper ftpHelper = new FtpHelper();
            byte[] uploadFileBytes = null;
            string uploadFileName = null;
            string fileAddress = null;
            string itemName = null;
            if (keyValue != null) {
                //var msg = "";
                HttpFileCollectionBase files = Request.Files;

                var file = files["file-0"];
                uploadFileName = file.FileName;//获取文件名-带后缀
                if (file != null)
                {
                    string fileType = Path.GetExtension(file.FileName);//获取文件类型
                    DateTime dt = DateTime.Now;
                    var date = Common.CreateNo();                    
                    string name = Path.GetFileNameWithoutExtension(file.FileName);//获取文件名-不带后缀

                    uploadFileBytes = new byte[file.ContentLength];
                    try
                    {
                        file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    var itemEntity = fileItemApp.GetForm(keyValue);
                    itemName = itemEntity.F_FullName;//获取左侧菜单名作为文件夹名
                    var savePath = "upload/file/" + itemName + "/" + uploadFileName;//文件保存路径

                    var localPath = "D:\\FTP\\EMS\\Upload\\" + savePath.Replace("/", "\\");
                    try
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                        }
                        System.IO.File.WriteAllBytes(localPath, uploadFileBytes);
                        ftpHelper.UpLoadFile(localPath, savePath);
                        //Result.Url = savePath;
                        fileAddress = "http://192.168.99.159:3698/" + savePath;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    FileListTableEntity fileListTableEntity = new FileListTableEntity();
                    fileListTableEntity.F_UseName = EMS.Code.OperatorProvider.Provider.GetCurrent().UserName;
                    fileListTableEntity.F_FileName = name;
                    fileListTableEntity.F_Date = DateTime.Now;
                    fileListTableEntity.F_GongXv = itemName;
                    fileListTableEntity.F_ItemId = keyValue;
                    fileListTableEntity.F_Type = fileType;
                    fileListTableEntity.F_Address = fileAddress;
                    fileListTableApp.SubmitForm(fileListTableEntity);
                }
                return Success("插入成功");
            }
            return Error("未选中左边菜单栏");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult DeleteForm(string keyValue)
        {
            fileListTableApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult download(string keyValue)
        {
            var data =  fileListTableApp.download(keyValue);
            return  Content(data.ToJson());
        }
        [HttpGet]
        
        /// <summary>
        ///  excel 转换为html
        /// </summary>
        /// <param name="path">要转换的文档的路径</param>
        /// <param name="savePath">转换成的html的保存路径</param>
        /// <param name="wordFileName">转换后html文件的名字</param>
        public ActionResult onlinetohtml(string keyValue)
        {
            
            GC.Collect();  
            ResultJson result = new ResultJson();
            FileListTableEntity entity = fileListTableApp.GetForm(keyValue);
            //string path = entity.F_Address;
            //string filename = "/file/" + entity.F_FileName;//111
            string path = entity.F_Address;//Server.MapPath(filename);
            string savePath = Server.MapPath("/file/");
            string wordFileName = entity.F_FileName;//转换成html文件的名字
            string str = string.Empty;
            string resultUrl = "";
            var date = Common.RndNum(4);
            //String path = System.getProperty("user.dir").replace("\\", "/") + "/photos/";
            //var localPath = "D:\\FTP\\EMS\\Upload\\" + savePath.Replace("/", "\\");
            //String path = new File(this.getClass().getClassLoader().getResource("/").getPath()).getParentFile().getParentFile().getAbsolutePath() + "/zjfxjk/expertsLib/photos/"
            if (entity.F_Type == ".xls" || entity.F_Type == ".xlsx")
            {
                Microsoft.Office.Interop.Excel.Application repExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = null;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
                workbook = repExcel.Application.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                object htmlFile = savePath.Replace("/", "\\") + wordFileName + date + ".html";
                resultUrl = htmlFile.ToString();
                object ofmt = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml;
                workbook.SaveAs(htmlFile, ofmt, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                object osave = false;
                workbook.Close(osave, Type.Missing, Type.Missing);
                repExcel.Quit();
            }
            else if (entity.F_Type == ".doc" || entity.F_Type == ".docx")
            {
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                Type wordType = word.GetType();
                Microsoft.Office.Interop.Word.Documents docs = word.Documents;
                Type docsType = docs.GetType();
                Microsoft.Office.Interop.Word.Document doc = (Microsoft.Office.Interop.Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { (object)path, true, true });
                Type docType = doc.GetType();
                resultUrl = savePath + wordFileName + date + ".html";
                object saveFileName = (object)resultUrl;
                docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatFilteredHTML });
                docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
                wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);                                
            }
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = resultUrl.Replace(tmpRootDir, ""); //转换成相对路径
            imagesurl2 = imagesurl2.Replace(@"\", @"/");
            result.str = imagesurl2;
            return Content(result.ToJson());
        }
        public class ResultJson
        {
            public bool res { get; set; }
            public string info { get; set; }
            public string str { get; set; }
        }
    }
}
