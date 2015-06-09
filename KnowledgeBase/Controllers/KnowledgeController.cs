using KnowledgeBase.Data;
using KnowledgeBase.Data.Repository;
using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Controllers{
    public class KnowledgeController : Controller{
        KnowledgeRepository repository;
        JsonDataSource jsonDataSource;
        public KnowledgeController() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\knowledge.json");
            jsonDataSource = new JsonDataSource(path);
            repository = new KnowledgeRepository(jsonDataSource);
        }

        //
        // GET: /Knowledge/
        public ActionResult Index(){
            IList<Knowledge> results = repository.Load();
            var knowledgeListViewModel = new KnowledgeListViewModel() {
                Knowledges = results,
                Tags = results.Select(x => x.Tag.Name).ToList()
            };
            return View(knowledgeListViewModel);
        }

        //
        // POST: /Knowledge/
        [HttpPost]
        public void AddArticle(AddArticleEvtArgs args) {
            if (args == null || !args.IsValid())
                throw new Exception("Invalid arguments");
            IList<Knowledge> knowledgeBase = repository.Load();
            var existentKnowledge = knowledgeBase.Where(x => x.Tag.Name == args.Tag).FirstOrDefault();
            if (existentKnowledge == null) {
                existentKnowledge = new Knowledge() { Tag = new Tag() { Name = args.Tag } };
                knowledgeBase.Add(existentKnowledge);
            }
            existentKnowledge.AddArticle(new Article() { Description = args.Description, Link = args.Link, Name = args.Name } );
            repository.Save(knowledgeBase);
        }

        public ActionResult GenerateJson() {
            IList<Knowledge> knowledgeList = new List<Knowledge>();
            var dapperTag = new Tag() { Name = "Dapper" };
            knowledgeList.Add(new Knowledge() {
                Tag = dapperTag,
                Articles = new List<Article>() { 
                    new Article() {
                        Description = "Dapper github repo",
                        Link = "https://github.com/StackExchange/dapper-dot-net/blob/master/Readme.md"
                    },
                    new Article(){
                        Description = "Dapper contrib github repo",
                        Link = "https://github.com/StackExchange/dapper-dot-net/blob/master/Dapper.Contrib/Readme.md"        
                    }
                }
            });
            return Json(knowledgeList, JsonRequestBehavior.AllowGet);
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
