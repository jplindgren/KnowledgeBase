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
            this.repository = repository;
        }

        //
        // GET: /Tag/
        public JsonResult Index() {
            IEnumerable<Article> collection = repository.Load();            
            return Json(collection.Select(x => x.Tag.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }

    } //class
}
