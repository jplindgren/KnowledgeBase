using KnowledgeBase.Data;
using KnowledgeBase.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Controllers{
    public class TagController : Controller{
        KnowledgeRepository repository;
        JsonDataSource jsonDataSource;
        public TagController() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\knowledge.json");
            jsonDataSource = new JsonDataSource(path);
            repository = new KnowledgeRepository(jsonDataSource);
        }
        //
        // GET: /Tag/
        public JsonResult Index() {
            IList<Knowledge> results = repository.Load();
            var tags = results.Select(x => x.Tag.Name).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

    } //class
}
