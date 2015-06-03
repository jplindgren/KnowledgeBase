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
        public IList<Knowledge> Load() {
            string data = LoadData(datasourcePath);
            var result = JsonParse(data);
            return result;
        }

        public void Save(IList<Knowledge> knowledgeBase) {
            var knowledgeBaseJson = JsonParse(knowledgeBase);
            File.WriteAllText(datasourcePath, knowledgeBaseJson, Encoding.UTF8);
        }

        private string LoadData(string filePath){
            if (!File.Exists(datasourcePath))
                throw new FileNotFoundException();

            return File.ReadAllText(datasourcePath, Encoding.UTF8);
        }

        private IList<Knowledge> JsonParse(string data) {
            var result = JsonConvert.DeserializeObject<List<Knowledge>>(data);
            return result;
        }

        private string JsonParse(IList<Knowledge> knowledgeBase) {
            var result = JsonConvert.SerializeObject(knowledgeBase, Formatting.Indented);
            return result;
        }
        
    } //class
}
