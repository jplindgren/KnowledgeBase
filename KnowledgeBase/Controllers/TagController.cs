using KnowledgeBase.Data;
using KnowledgeBase.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Controllers{
    public class TagController : BaseController{
        KnowledgeRepository repository;
        public TagController(KnowledgeRepository repository) {                        
            this.repository = repository;
        }

        //
        // GET: /Tag/
        public JsonResult Index() {
            //TODO: Fix this in another branch
            IEnumerable<Article> collection = repository.Load(GetUserId());
            return Json(collection.Select(x => x.Tag.Name).Distinct(), JsonRequestBehavior.AllowGet);
        }

    } //class
}
