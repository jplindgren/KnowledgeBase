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
            return View(GetArticleListViewModel());
        }

        [HttpGet]
        public ActionResult GetKnowledgeList() {
            var result = GetArticleListViewModel();
            return PartialView("_KnowledgeList", result);
        }

        //
        // POST: /Knowledge/
        [HttpPost]
        public void AddArticle(AddArticleEvtArgs args) {
            if (args == null || !args.IsValid())
                throw new Exception("Invalid arguments");            
            
            repository.Save(new Article() { Id = Guid.NewGuid(), Description = args.Description, Link = args.Link, Name = args.Name, Tag = new Tag(args.Tag) });
        }

        //
        // POST: /Knowledge/Remove/{name}
        [HttpGet]
        public ActionResult Remove(string id) {
            var articleId = Guid.Parse(id);

            IEnumerable<Article> collection = repository.Load();
            var articleToRemove = collection.Where(x => x.Id == articleId).First();
            repository.Save(articleToRemove);

            return RedirectToAction("Index");
        }

        private ArticleListViewModel GetArticleListViewModel() {
            IEnumerable<Article> collection = repository.Load();
            var knowledgeListViewModel = new ArticleListViewModel();
            
            knowledgeListViewModel.Articles = collection;
            knowledgeListViewModel.Tags = collection.Select(x => x.Tag);
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
