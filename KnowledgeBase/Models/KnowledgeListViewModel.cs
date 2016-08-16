using KnowledgeBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeBase.Models {
    public class ArticleListViewModel {
        public ArticleListViewModel() {
            this.Articles = new List<Article>();
            this.Tags = new List<Tag>();
        }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}