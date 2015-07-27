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
        IDatasource dataSource;
        public TagController() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\knowledge.json");
            dataSource = new AzureStorageDatasource();
            repository = new KnowledgeRepository(dataSource);
        }
        //
        // GET: /Tag/
        public JsonResult Index() {
            IList<Knowledge> results = repository.Load();
            var tags = new List<string>();
            if (results != null) {
                tags = results.Select(x => x.Tag.Name).ToList();
            }
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

    } //class
}
