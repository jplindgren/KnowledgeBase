using System;
using System.Runtime.Serialization;

namespace KnowledgeBase.Data.DocumentDB {
    [Serializable]
    internal class AzureDocumentDbConfigurationException : Exception {
        public AzureDocumentDbConfigurationException() {
        }

        public AzureDocumentDbConfigurationException(string message) : base(message) {
        }

        public AzureDocumentDbConfigurationException(string message, Exception innerException) : base(message, innerException) {
        }

        protected AzureDocumentDbConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}