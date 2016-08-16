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
        public IEnumerable<Knowledge> Knowledges { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}