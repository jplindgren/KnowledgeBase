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
            KnowledgeCollection collection = repository.Load();            
            return Json(collection.GetTags().Select(tag => tag.Name), JsonRequestBehavior.AllowGet);
        }

    } //class
}
