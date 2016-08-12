using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Data {
    public class Article {
        public Article() { }
        public Article(string name, string description, string link) {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.Link = link;            
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    } //class
}
