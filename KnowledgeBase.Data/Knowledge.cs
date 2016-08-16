using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data{
    public class Knowledge{
        public Tag Tag { get; set; }
        public Guid Id { get; set; }
        public IList<Article> Articles { get; set; }

        public bool ContainsArticle(Guid articleId) {
            if (articleId == Guid.Empty)
                throw new ArgumentNullException("articleId");
            if (Articles == null)
                Articles = new List<Article>();
            return Articles.Any(x => x.Id == articleId);
        }

        public void RemoveArticle(Guid articleId) {
            if (articleId == Guid.Empty)
                throw new ArgumentNullException("articleId");

            if (Articles == null)
                Articles = new List<Article>();

            var article = Articles.Where(x => x.Id == articleId).FirstOrDefault();
            if (article == null)
                throw new Exception("Article not found");
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

        public bool Empty() {
            return !Articles.Any();
        }
    } //class
}
