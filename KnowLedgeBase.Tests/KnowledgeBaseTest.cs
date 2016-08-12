using KnowledgeBase.Data;
using KnowledgeBase.Data.Exceptions;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KnowLedgeBase.Tests {    
    public class KnowledgeBaseTest {        
        [Fact()]
        public void Should_Throw_Exception_If_No_Article_Is_Provided_When_Creating_Knowledge() {
            KnowledgeCollection knowledges = new KnowledgeCollection();
            Article nullArticle = null;
            Assert.Throws(typeof(ArgumentNullException), () => knowledges.AddArticle("aTag", nullArticle)); 
        }

        [Theory(), AutoData()]
        public void Should_Create_NewKnowledge_If_Article_Tag_Does_Not_Exists(KnowledgeCollection knowledges, Article newArticle) {
            string aTag = "Inexistent Tag";
            
            knowledges.AddArticle(aTag, newArticle);
            Assert.True(knowledges.HasKnowledge(aTag));
        }

        [Theory(), AutoData()]
        public void Should_Add_Article_To_Newly_Created_Knowledges(KnowledgeCollection knowledges, Article newArticle) {
            string aTag = "Inexistent Tag";
            var originalArticlesCount = knowledges.ArticleCount(aTag);

            knowledges.AddArticle(aTag, newArticle);
            var newArticlesCount = knowledges.ArticleCount(aTag);
            Assert.Equal(0, originalArticlesCount);
            Assert.Equal(1, newArticlesCount);
        }

        [Theory(), AutoData()]
        public void Should_Add_Article_To_Correctly_Existent_Tag(IList<Knowledge> knowledges, Article newArticle) {
            var knowledgeCollection = new KnowledgeCollection(knowledges);
            var firstKnowledge = knowledges.FirstOrDefault();

            var originalArticlesCount = knowledgeCollection.ArticleCount(firstKnowledge.Tag.Name);
            knowledgeCollection.AddArticle(firstKnowledge.Tag.Name, newArticle);
            var newArticlesCount = knowledgeCollection.ArticleCount(firstKnowledge.Tag.Name);
            
            Assert.Equal(originalArticlesCount + 1, newArticlesCount);
        }

        [Theory(), AutoData()]
        public void Should_Throw_Exception_When_Does_Not_Find_Article_To_Remove(Guid articleId, KnowledgeCollection knowledges) {            
            Assert.Throws(typeof(ArticleNotFoundException), () => knowledges.RemoveArticle(articleId)); 
        }

        [Theory(), AutoData()]
        public void Should_Remove_Article(IList<Knowledge> knowledges) {
            var knowledgeCollection = new KnowledgeCollection(knowledges);
            var firstKnowledge = knowledges.FirstOrDefault();
            var originalArticlesCount = knowledgeCollection.ArticleCount(firstKnowledge.Tag.Name);

            knowledgeCollection.RemoveArticle(firstKnowledge.Articles.First().Id);
            var newArticlesCount = knowledgeCollection.ArticleCount(firstKnowledge.Tag.Name);

            Assert.Equal(originalArticlesCount - 1, newArticlesCount);
        }

        [Theory(), AutoData()]
        public void Should_Remove_Knowledge_If_All_Knowledges_Were_Deleted(KnowledgeCollection knowledgeCollection, Article article) {
            knowledgeCollection.AddArticle("aTag", article);
            Assert.True(knowledgeCollection.HasKnowledge("aTag"));

            knowledgeCollection.RemoveArticle(article.Id);
            
            Assert.False(knowledgeCollection.HasKnowledge("aTag"));
        }
    } //class
}
