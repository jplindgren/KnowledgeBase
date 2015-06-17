using KnowledgeBase.Data;
using KnowledgeBase.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Controllers{
    public class OptionsController : Controller{
        KnowledgeRepository repository;
        JsonDataSource jsonDataSource;

        public OptionsController() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\knowledge.json");
            jsonDataSource = new JsonDataSource(path);
            repository = new KnowledgeRepository(jsonDataSource);
        }

        //
        // GET: /Options/
        public ActionResult Index(){
            return View();
        }

        //
        // POST: /Options/Load
        [HttpPost]
        public ActionResult Load(HttpPostedFileBase restoreBackup) {
            if (restoreBackup != null && restoreBackup.ContentLength > 0) {
                var fileName = Path.GetFileName(restoreBackup.FileName);
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\knowledge.json"); ;
                restoreBackup.SaveAs(path);
            }
            return RedirectToAction("Index","Knowledge");
        }

        //
        // POST: /Options/Save
        [HttpPost]
        public FileResult Create() {
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(jsonDataSource.LoadData());
            string fileName = "bkp.json";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    } // class
}
