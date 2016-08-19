using KnowledgeBase.Data;
using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            repository.Remove(articleId);

            return RedirectToAction("Index");
        }

        private ArticleListViewModel GetArticleListViewModel() {
            IEnumerable<Article> collection = repository.Load();
            var articleListViewModel = new ArticleListViewModel();

            articleListViewModel.GroupedArticles = collection.GroupBy(x => x.Tag).ToDictionary(x => x.Key, y => y.AsEnumerable());

            List<List<string>> tagsGroups = articleListViewModel.GroupedArticles.Keys.Select((x, i) => new { Index = i, Value = x })
                                                            .GroupBy(x => x.Index / 5)
                                                            .Select(x => x.Select(y => y.Value.Name).ToList())
                                                            .ToList();

            return articleListViewModel;
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
