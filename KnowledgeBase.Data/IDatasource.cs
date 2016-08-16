using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Data {
    public interface IDatasource {
        IEnumerable<Article> Load();
        void Save(Article article);
        void Remove(Article article);

    } //class
}
