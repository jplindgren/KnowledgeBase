using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repository {
    public class JsonDataSource : IDatasource{
        public string datasourcePath { get; set; }

        public JsonDataSource(string datasourcePath) {
            this.datasourcePath = datasourcePath;
        }
        public IEnumerable<Article> Load() {
            string data = LoadData();
            var result = JsonParse(data);
            return result;
        }

        public void Save(Article article) {
            string data = LoadData();
            var articles = JsonParse(data);
            articles = articles.Concat(new Article[] { article });

            var articlesJson = JsonParse(articles);
            File.WriteAllText(datasourcePath, articlesJson, Encoding.UTF8);
        }

        public void Remove(Article article) {
            string data = LoadData();
            var articles = JsonParse(data);

            var articlesJson = JsonParse(articles.Where(x => x.Id != article.Id).ToList());
            File.WriteAllText(datasourcePath, articlesJson, Encoding.UTF8);
        }

        public string LoadData(){            
            using (FileStream stream = File.Open(datasourcePath, FileMode.OpenOrCreate, FileAccess.Read)) {
                using (var reader = new StreamReader(stream, Encoding.UTF8)) {
                    return reader.ReadToEnd();
                }
            }
        }

        private IEnumerable<Article> JsonParse(string data) {
            var result = JsonConvert.DeserializeObject<List<Article>>(data);
            return result;
        }

        private string JsonParse(IEnumerable<Article> articles) {
            var result = JsonConvert.SerializeObject(articles, Formatting.Indented);
            return result;
        }
        
    } //class
}
