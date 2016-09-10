using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Data {
    public class Article : IEntity{
        public Article() { }
        public Article(string name, string description, string link, string tag) {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.Link = link;
            this.Tag = new Tag(tag);
            this.CreatedAt = new DateTime(); 
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("tag")]
        public Tag Tag { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    } //class
}
