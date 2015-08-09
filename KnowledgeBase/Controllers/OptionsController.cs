using KnowledgeBase.Data;
using KnowledgeBase.Data.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Controllers{
    public class OptionsController : Controller{
        KnowledgeRepository repository;
        IDatasource datasource;

        public OptionsController() {
            datasource = new AzureStorageDatasource();
            repository = new KnowledgeRepository(datasource);
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
                using (StreamReader reader = new StreamReader(restoreBackup.InputStream)) {
                    var json = reader.ReadToEnd();
                    var knowledge = JsonConvert.DeserializeObject<List<Knowledge>>(json);
                    repository.Save(knowledge);
                }
            }
            return RedirectToAction("Index" , "Knowledge");
        }

        //
        // POST: /Options/Save
        [HttpPost]
        public FileResult Create() {
            var data = repository.Load();
            var json = JsonConvert.SerializeObject(data);
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(json);
            string fileName = "bkp.json";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    } // class
}
