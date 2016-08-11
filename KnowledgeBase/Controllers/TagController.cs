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
        public TagController(KnowledgeRepository repository) {            
            //dataSource = new AzureStorageDatasource();
            //repository = new KnowledgeRepository(dataSource);
            this.repository = repository;
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
