﻿using KnowledgeBase.Data;
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
    public class OptionsController : BaseController {
        KnowledgeRepository repository;

        public OptionsController(KnowledgeRepository repository) {
            this.repository = repository;
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
            //if (restoreBackup != null && restoreBackup.ContentLength > 0) {
            //    using (StreamReader reader = new StreamReader(restoreBackup.InputStream)) {
            //        var json = reader.ReadToEnd();
            //        var articles = JsonConvert.DeserializeObject<List<Article>>(json);
            //        repository.Save(articles);
            //    }
            //}
            return RedirectToAction("Index" , "Knowledge");
        }

        //
        // POST: /Options/Save
        [HttpPost]
        public FileResult Create() {
            var data = repository.Load(GetUserId());
            var json = JsonConvert.SerializeObject(data);
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(json);
            string fileName = "bkp.json";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    } // class
}
