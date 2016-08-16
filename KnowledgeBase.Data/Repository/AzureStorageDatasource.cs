using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using System.IO;
using Newtonsoft.Json;

namespace KnowledgeBase.Data.Repository {
    //public class AzureStorageDatasource : IDatasource{
    //    CloudStorageAccount storageAccount;
    //    CloudBlobClient blobClient;
    //    CloudBlobContainer container;

    //    public AzureStorageDatasource() {
    //        storageAccount = StorageUtils.StorageAccount;

    //        // Create a blob client and retrieve reference to images container
    //        blobClient = storageAccount.CreateCloudBlobClient();
    //        container = blobClient.GetContainerReference("knowledges");

    //        // Create the "images" container if it doesn't already exist.
    //        if (container.CreateIfNotExists()) {
    //            // Enable public access on the newly created "images" container
    //            container.SetPermissions(
    //                new BlobContainerPermissions {
    //                    PublicAccess = BlobContainerPublicAccessType.Blob
    //                });
    //        }
    //    }
    //    public IList<Knowledge> Load() {
    //        var blockBlob = this.container.GetBlockBlobReference("current-knowledge.json");

    //        if (!blockBlob.Exists())
    //            return new List<Knowledge>();

    //        using (var memory = new MemoryStream()) {
    //            using (var reader = new StreamReader(memory)) {
    //                blockBlob.DownloadToStream(memory);
    //                memory.Seek(0, SeekOrigin.Begin);

    //                var data = reader.ReadToEnd();
    //                return JsonParse(data);
    //            }
    //        }
    //    }

    //    public void Save(IList<Knowledge> knowledgeBase) {
    //        var knowledgeBaseJson = JsonParse(knowledgeBase);

    //        var blockBlob = container.GetBlockBlobReference("current-knowledge.json");

    //        using (var memory = new MemoryStream()) {
    //            using (var writer = new StreamWriter(memory)) {
    //                writer.Write(knowledgeBaseJson);
    //                writer.Flush();
    //                memory.Seek(0, SeekOrigin.Begin);

    //                blockBlob.UploadFromStream(memory);
    //            }
    //        }

    //        blockBlob.Properties.ContentType = "application/json";
    //        blockBlob.SetProperties();            
    //    }



    //    private IList<Knowledge> JsonParse(string data) {
    //        var result = JsonConvert.DeserializeObject<List<Knowledge>>(data);
    //        return result;
    //    }

    //    private string JsonParse(IList<Knowledge> knowledgeBase) {
    //        var result = JsonConvert.SerializeObject(knowledgeBase, Formatting.Indented);
    //        return result;
    //    }
    } //class

    public class StorageUtils {
        public static CloudStorageAccount StorageAccount {
            get {
                string account = CloudConfigurationManager.GetSetting("StorageAccountName");
                string key = CloudConfigurationManager.GetSetting("StorageAccountAccessKey");
                string connectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
                return CloudStorageAccount.Parse(connectionString);
            }
        }
    } //class

    public static class BlobExtensions {
        public static bool Exists(this CloudBlockBlob blob) {
            try {
                blob.FetchAttributes();
                return true;
            } catch (StorageException e) {
                if (e.Message.Contains("404") || e.Message.Contains("Not Found")) {
                    return false;
                } else {
                    throw;
                }
            }
        }
    //} //class
}
