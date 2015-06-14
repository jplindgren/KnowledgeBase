using KnowledgeBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeBase.Models {
    public class KnowledgeListViewModel {
        public KnowledgeListViewModel() {
            this.Knowledges = new List<Knowledge>();
            this.Tags = new List<string>();
        }
        public IList<Knowledge> Knowledges { get; set; }
        public IList<string> Tags { get; set; }
    }
}