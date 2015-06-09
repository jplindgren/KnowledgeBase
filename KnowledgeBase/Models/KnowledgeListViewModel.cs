using KnowledgeBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeBase.Models {
    public class KnowledgeListViewModel {
        public IList<Knowledge> Knowledges { get; set; }
        public IList<string> Tags { get; set; }
    }
}