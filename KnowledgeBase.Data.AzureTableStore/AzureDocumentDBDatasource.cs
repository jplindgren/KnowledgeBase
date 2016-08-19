using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.AzureTableStore{
    public class AzureDocumentDBDatasource : IDatasource {
        public const string ENDPOINT_ENVIROMENTVARIABLE = "MYCONTENT_DOCUMENTDB_ENDPOINT";
        public const string PRIMARYKEY_ENVIROMENTVARIABLE = "MYCONTENT_DOCUMENTDB_KEY";
        private DocumentClient client;
        private string collectionName;
        private string dbname;

        public AzureDocumentDBDatasource(string endpoint, string key, string dbName, string collectionName) {
            if (string.IsNullOrEmpty(endpoint))
                throw new AzureDocumentDbConfigurationException(string.Format("Azure DocumentDb Configuration failed. 'Endpoint' property cannot be null. Check if the enviroment variable {0} is correctly set in your enviroment", ENDPOINT_ENVIROMENTVARIABLE));
            if (string.IsNullOrEmpty(key))
                throw new AzureDocumentDbConfigurationException(string.Format("Azure DocumentDb Configuration failed. 'PrimaryKey' property cannot be null. Check if the enviroment variable {0} is correctly set in your enviroment", PRIMARYKEY_ENVIROMENTVARIABLE));

            this.client = new DocumentClient(new Uri(endpoint), key);

            this.dbname = dbName;
            this.collectionName = collectionName;

            AsyncHelpers.RunSync(() => CreateDatabaseIfNotExists(this.dbname));
            AsyncHelpers.RunSync(() => CreateDocumentCollectionIfNotExists(this.dbname, this.collectionName));
            //var t1 = CreateDatabaseIfNotExists(this.dbname);
            //t1.Wait();
            //var t2 = CreateDocumentCollectionIfNotExists(this.dbname, this.collectionName);
            //t2.Wait();
        }

        public async Task Prepare() {
            await CreateDatabaseIfNotExists(this.dbname);
            await CreateDocumentCollectionIfNotExists(this.dbname, collectionName);
        }

        public IEnumerable<Article> Load() {
            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 500 };
            // Here we find the Andersen family via its LastName
            IQueryable<Article> articleQuery = this.client.CreateDocumentQuery<Article>(
                    UriFactory.CreateDocumentCollectionUri(this.dbname, this.collectionName), queryOptions);
                        //.Where(f => f.Name == "zzzz");
            return articleQuery;
        }

        public async Task Save(Article article) {
            var what = await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.dbname, this.collectionName), article);
        }

        public async Task Remove(Guid articleId) {
            var what = await this.client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(this.dbname, this.collectionName, articleId.ToString()));            
        }


        private async Task CreateDatabaseIfNotExists(string databaseName) {
            // Check to verify a database with the id=FamilyDB does not exist
            try {
                await this.client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));

            } catch (DocumentClientException de) {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound) {
                    await this.client.CreateDatabaseAsync(new Database { Id = databaseName });
                } else {
                    throw;
                }
            }
        }

        private async Task CreateDocumentCollectionIfNotExists(string databaseName, string collectionName) {
            try {
                await this.client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));                
            } catch (DocumentClientException de) {
                // If the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound) {
                    DocumentCollection collectionInfo = new DocumentCollection();
                    collectionInfo.Id = collectionName;

                    // Configure collections for maximum query flexibility including string range queries.
                    collectionInfo.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });

                    // Here we create a collection with 400 RU/s.
                    await this.client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseName),
                        collectionInfo,
                        new RequestOptions { OfferThroughput = 400 });
                } else {
                    throw;
                }
            }
        }        
    } //class
}
