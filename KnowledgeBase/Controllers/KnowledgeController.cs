using KnowledgeBase.Data;
using KnowledgeBase.Data.Repository;
using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Controllers{
    public class KnowledgeController : Controller{
        KnowledgeRepository repository;
        public KnowledgeController(KnowledgeRepository repository) {
            this.repository = repository;
        }

        //
        // GET: /Knowledge/
        public ActionResult Index(){
            return View(GetKnowledgeListViewModel());
        }

        [HttpGet]
        public ActionResult GetKnowledgeList() {
            var result = GetKnowledgeListViewModel();
            return PartialView("_KnowledgeList", result);
        }

        //
        // POST: /Knowledge/
        [HttpPost]
        public void AddArticle(AddArticleEvtArgs args) {
            if (args == null || !args.IsValid())
                throw new Exception("Invalid arguments");
            KnowledgeCollection collection = repository.Load();
            collection.AddArticle(args.Tag, new Article() { Id = Guid.NewGuid(), Description = args.Description, Link = args.Link, Name = args.Name });
            
            repository.Save(collection);
        }

        //
        // POST: /Knowledge/Remove/{name}
        [HttpGet]
        public ActionResult Remove(string id) {
            var articleId = Guid.Parse(id);

            KnowledgeCollection collection = repository.Load();
            collection.RemoveArticle(articleId);
            repository.Save(collection);

            return RedirectToAction("Index");
        }

        private KnowledgeListViewModel GetKnowledgeListViewModel() {
            KnowledgeCollection collection = repository.Load();
            var knowledgeListViewModel = new KnowledgeListViewModel();
            
            knowledgeListViewModel.Knowledges = collection.GetKnowledges();
            knowledgeListViewModel.Tags = collection.GetTags().Select(tag => tag.Name);
            return knowledgeListViewModel;
        }

    } //class

    public class AddArticleEvtArgs {
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public bool IsValid() { 
            return !string.IsNullOrEmpty(Tag) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Link);
        }
    } //class AddArticleEvtArgs 
}
