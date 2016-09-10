using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data {
    public class KnowledgeRepository {
        public IDatasource<Article> datasource { get; set; }
        public KnowledgeRepository(IDatasource<Article> dataSource) {
            this.datasource = dataSource;
        }

        public IEnumerable<Article> Load(Guid userId) {
            return this.datasource.Load().Where(x => x.UserId == userId).ToList();
        }

        public IEnumerable<Tag> LoadTags(Guid userId) {
            //DocumentDb does not support distinct yet =[
            var tags = this.datasource.Load().Where(x => x.UserId == userId).Select(x => x.Tag);
            return tags.Distinct().ToList();
        }

        public void Save(Article article) {
            this.datasource.Save(article);
        }

        public void Remove(Guid articleId) {
            this.datasource.Remove(articleId);
        }
    } //class
}
