using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Data {
    public interface IDatasource {
        IList<Knowledge> Load();
        void Save(IList<Knowledge> knowledgeBase);
    } //class
}
