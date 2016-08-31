using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data {
    public interface IDatasource<T> where T : IEntity{
        IEnumerable<T> Load();
        Task Save(T article);
        Task Remove(Guid id);

    } //class
}
