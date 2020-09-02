using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        [HttpPost]
        public void UploadFiles(HttpPostedFileBase[] files)
        {

            //Ensure model state is valid  
               //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/images") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            
        }
    }
}