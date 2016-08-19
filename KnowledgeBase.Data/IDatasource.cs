using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data {
    public interface IDatasource {
        IEnumerable<Article> Load();
        Task Save(Article article);
        Task Remove(Guid articleId);

    } //class
}
