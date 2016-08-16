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

        public KnowledgeCollection Load() {
            return this.datasource.Load();
        }

        public void Save(KnowledgeCollection knowledgeBase) {
            this.datasource.Save(knowledgeBase);
        }
    } //class
}
