using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repository {
    public class JsonDataSource<T> : IDatasource<T> where T : IEntity{
        public string datasourcePath { get; set; }

        public JsonDataSource(string datasourcePath) {
            this.datasourcePath = datasourcePath;
        }
        public IEnumerable<T> Load() {
            string data = LoadData();
            var result = JsonParse(data);
            return result;
        }

        public Task Save(T article) {
            string data = LoadData();
            var articles = JsonParse(data);
            articles = articles.Concat(new T[] { article });

            var articlesJson = JsonParse(articles);
            File.WriteAllText(datasourcePath, articlesJson, Encoding.UTF8);
            return Task.FromResult(0);
        }

        public Task Remove(Guid articleId) {
            string data = LoadData();
            var articles = JsonParse(data);

            var articlesJson = JsonParse(articles.Where(x => x.Id != articleId).ToList());
            File.WriteAllText(datasourcePath, articlesJson, Encoding.UTF8);
            return Task.FromResult(0);
        }

        public string LoadData(){            
            using (FileStream stream = File.Open(datasourcePath, FileMode.OpenOrCreate, FileAccess.Read)) {
                using (var reader = new StreamReader(stream, Encoding.UTF8)) {
                    return reader.ReadToEnd();
                }
            }
        }

        private IEnumerable<T> JsonParse(string data) {
            var result = JsonConvert.DeserializeObject<List<T>>(data);
            return result;
        }

        private string JsonParse(IEnumerable<T> articles) {
            var result = JsonConvert.SerializeObject(articles, Formatting.Indented);
            return result;
        }
        
    } //class
}
