using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Data {
    public class Tag {
        public Tag() {

        }
        public Tag(string tag) {
            this.Name = tag;
        }

        public string Name { get; set; }
    }
}
