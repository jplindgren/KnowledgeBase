﻿using KnowledgeBase.Data;
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
            IList<Knowledge> knowledgeBase = repository.Load();
            if (knowledgeBase == null)
                knowledgeBase = new List<Knowledge>();

            var existentKnowledge =  knowledgeBase.Where(x => x.Tag.Name == args.Tag).FirstOrDefault();
            if (existentKnowledge == null) {
                existentKnowledge = new Knowledge() { Tag = new Tag() { Name = args.Tag } };
                knowledgeBase.Add(existentKnowledge);
            }
            existentKnowledge.AddArticle(new Article() { Id = Guid.NewGuid(), Description = args.Description, Link = args.Link, Name = args.Name } );
            repository.Save(knowledgeBase);
        }

        //
        // POST: /Knowledge/Remove/{name}
        [HttpGet]
        public ActionResult Remove(string id) {
            var articleId = Guid.Parse(id);
            
            IList<Knowledge> knowledgeBase = repository.Load();
            if (knowledgeBase == null)
                knowledgeBase = new List<Knowledge>();

            Knowledge knowledge = knowledgeBase.Where(x => x.ContainsArticle(articleId)).FirstOrDefault();
            if (knowledge == null) {
                throw new Exception("Knowledge not found");
            }

            knowledge.RemoveArticle(articleId);
            if (knowledge.Articles.Count() == 0) {
                knowledgeBase.Remove(knowledge);
            }

            repository.Save(knowledgeBase);
            return RedirectToAction("Index");
        }

        private KnowledgeListViewModel GetKnowledgeListViewModel() {
            IList<Knowledge> results = repository.Load();
            var knowledgeListViewModel = new KnowledgeListViewModel();
            if (results != null) {
                knowledgeListViewModel.Knowledges = results;
                knowledgeListViewModel.Tags = results.Select(x => x.Tag.Name).ToList();
            }
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
