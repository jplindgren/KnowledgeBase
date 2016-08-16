using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data {
    public class KnowledgeRepository {
        public IDatasource datasource { get; set; }
        public KnowledgeRepository(IDatasource dataSource) {
            this.datasource = dataSource;
        }

        public IEnumerable<Article> Load() {
            return this.datasource.Load();
        }

        public void Save(Article article) {
            this.datasource.Save(article);
        }

        public void Remove(Article article) {
            this.datasource.Remove(article);
        }
    } //class
}
