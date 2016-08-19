using KnowledgeBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeBase.Models {
    public class ArticleListViewModel {
        public ArticleListViewModel() {
            this.GroupedArticles = new Dictionary<Tag, IEnumerable<Article>>();
        }
        
        public IDictionary<Tag, IEnumerable<Article>> GroupedArticles { get; set; }
    }
}