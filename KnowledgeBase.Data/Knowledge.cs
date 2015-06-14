using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data{
    public class Knowledge{
        public Tag Tag { get; set; }
        public IList<Article> Articles { get; set; }

        public bool ContainsArticle(string name) {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (Articles == null)
                Articles = new List<Article>();
            return Articles.Any(x => x.Name == name);
        }

        public void RemoveArticle(string name) {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (Articles == null)
                Articles = new List<Article>();

            var article = Articles.Where(x => x.Name == name).FirstOrDefault();
            if (article == null)
                throw new Exception("Article not found for name: " + name);
            Articles.Remove(article);
        }

        public bool AddArticle(Article article) {
            if (article == null)
                throw new ArgumentNullException("article");
            if (Articles == null)
                Articles = new List<Article>();

            if (Articles.Any(x => x.Link == article.Link || x.Name == article.Name))
                throw new Exception("There is already an article with this name or link");

            Articles.Add(article);
            return true;
        }
    } //class
}
