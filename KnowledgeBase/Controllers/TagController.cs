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
            IEnumerable<Tag> tags = repository.LoadTags(GetUserId());
            return Json(tags.Select(x => x.Name).ToList(), JsonRequestBehavior.AllowGet);
        }

    } //class
}
