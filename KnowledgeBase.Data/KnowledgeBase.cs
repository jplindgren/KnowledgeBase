using KnowledgeBase.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data {
    public class KnowledgeCollection : IEnumerable<Knowledge>{
        private IList<Knowledge> knowledges;
        public KnowledgeCollection(IList<Knowledge> knowledges) {
            this.knowledges = knowledges;
        }

        public KnowledgeCollection() {
            this.knowledges = new List<Knowledge>();
        }      

        public IEnumerator<Knowledge> GetEnumerator() {
            return this.knowledges.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.knowledges.GetEnumerator();
        }
        
        public void AddArticle(string tag, Article article) {
            var knowledge = knowledges.Where(x => x.Tag.Name == tag).FirstOrDefault();
            if (knowledge == null) {
                knowledge = CreateKnowledgeForTag(tag);
            }
            knowledge.AddArticle(article);
        }

        public void RemoveArticle(Guid articleId) {
            var knowledge = this.knowledges.Where(x => x.ContainsArticle(articleId)).FirstOrDefault();
            if (knowledge == null) {
                throw new ArticleNotFoundException("Knowledge not found");
            }            
            knowledge.RemoveArticle(articleId);

            if (knowledge.Empty()) {
                ClearKnowledge(knowledge);
            }            
        }

        public int ArticleCount(string tag) {
            var knowledge = knowledges.Where(x => x.Tag.Name == tag).FirstOrDefault();
            return knowledge != null ? knowledge.Articles.Count() : 0;
        }

        public bool HasKnowledge(string tag) {
            var knowledge = knowledges.Where(x => x.Tag.Name == tag).FirstOrDefault();
            return knowledge != null;
        }

        private void ClearKnowledge(Knowledge knowledge) {
            this.knowledges.Remove(knowledge);
        }

        private Knowledge CreateKnowledgeForTag(string tag) {
            var newKnowledge = new Knowledge() { Tag = new Tag() { Name = tag } };
            knowledges.Add(newKnowledge);
            return newKnowledge;
        }
    } //class
}
