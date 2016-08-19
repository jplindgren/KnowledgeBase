using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Data {
    public class Tag {       
        public Tag(string tag) {
            this.Name = tag;
        }

        public string Name { get; set; }

        public override bool Equals(object obj) {
            Tag tagToCompare = obj as Tag;
            if (tagToCompare == null)
                return false;
            return tagToCompare.Name == this.Name;
        }

        public override int GetHashCode() {
            return Name.GetHashCode();            
        }
    } //class
}
