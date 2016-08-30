using KnowledgeBase.Data;
using KnowledgeBase.Models;
using Microsoft.Owin.Security;
using MyKnowledge.Authentication;
using MyKnowledge.Authentication.Model;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace KnowledgeBase.Controllers{ 

    public class KnowledgeController : BaseController {
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
        public JsonResult AddArticle(AddArticleEvtArgs args) {
            if (args == null)
                throw new Exception("Invalid arguments");

            if (ModelState.IsValid) {
                repository.Save(new Article() { Id = Guid.NewGuid(),
                                                Description = args.Description,
                                                Link = args.Link,
                                                Name = args.Name,
                                                Tag = new Tag(args.Tag),
                                                UserId = GetUserId() });
            }           

            var modelStateErrors = ModelState.Where(x => x.Value.Errors.Count > 0);
            var allErrors = modelStateErrors.SelectMany(v => v.Value.Errors);
            var allFieldErrors = modelStateErrors.Select(x => x.Key.ToLower());

            return Json(new {
                HasErrors = allErrors.Any(),
                Errors = allErrors.Select(x => x.ErrorMessage).ToList(),
                ErrorFields = allFieldErrors
            });
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
            IEnumerable<Article> collection = repository.Load(GetUserId());
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
        [Required]
        public string Tag { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(2048)]
        [Url()]
        public string Link { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        public bool IsValid() { 
            return !string.IsNullOrEmpty(Tag) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Link);
        }
    } //class AddArticleEvtArgs 
}
