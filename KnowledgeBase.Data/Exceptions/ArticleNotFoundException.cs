using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Exceptions {
    public class ArticleNotFoundException : Exception {
        public ArticleNotFoundException(string message) : base(message) { 
        }
    }
}
