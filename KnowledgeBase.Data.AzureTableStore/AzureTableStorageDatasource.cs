using System;
using Microsoft.Azure; // Namespace for CloudConfigurationManager 
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Data.AzureTableStore{
    public class AzureTableStorageDatasource : IDatasource {
        CloudTable table;
        public AzureTableStorageDatasource() {
            // Parse the connection string and return a reference to the storage account
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@"AccountEndpoint=https://knowledge.documents.azure.com:443/;AccountKey=1IcwipTSFUCS7mA5pFduTrwpnsiWGKl6FJGjXfpCVfdPC4pq22WSk3zpZscxFq7tTnVrqR3id4SPE9LcZr2vQg==;");

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            table = tableClient.GetTableReference("knowledge");

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();
        }
        public IEnumerable<Article> Load() {
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<ArticleEntity> query = new TableQuery<ArticleEntity>();
            //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));
            var collection = table.ExecuteQuery(query);
            return collection.Select(x => new Article() {
                Id = Guid.Parse(x.RowKey),
                Description = x.Description,
                Link = x.Link,
                Name = x.Name,
                Tag = new Tag(x.PartitionKey)
            });
            
        }

        public void Save(Article article) {

            TableOperation insertOperation = TableOperation.InsertOrMerge(new ArticleEntity(article));

            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        public void Remove(Article article) {

            //TableOperation insertOperation = TableOperation.(new ArticleEntity(article));

            //// Execute the insert operation.
            //table.Execute(insertOperation);
        }

        public class ArticleEntity : TableEntity {
            public ArticleEntity(Article article) {
                this.PartitionKey = article.Tag.Name;
                this.RowKey = article.Id.ToString();
                this.Description = article.Description;
                this.Link = article.Link;
                this.Name = article.Name;             
            }

            public ArticleEntity() { }
            public string Description { get; set; }
            public string Link { get; set; }
            public string Name { get; set; }
        }
    } //class
}
