using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Data {
    public interface IDatasource {
        KnowledgeCollection Load();
        void Save(KnowledgeCollection knowledgeBase);
    } //class
}
