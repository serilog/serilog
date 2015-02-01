// Copyright 2014 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.AzureDocumentDb
{
    public class AzureDocumentDBSink : ILogEventSink
    {
        readonly IFormatProvider _formatProvider;
        DocumentClient _client;
        Database _database;
        DocumentCollection _collection;

        public AzureDocumentDBSink(Uri endpointUri, string authorizationKey, string databaseName, string collectionName, IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
            _client = new DocumentClient(endpointUri, authorizationKey);
            Task.WaitAll(new []{CreateDatabaseIfNotExistsAsync(databaseName)});
            Task.WaitAll(new []{CreateCollectionIfNotExistsAsync(collectionName)});
        }

        public void Emit(LogEvent logEvent)
        {
            Task.WaitAll(EmitAsync(logEvent));
        }

        private async Task CreateDatabaseIfNotExistsAsync(string databaseName)
        {
            _database = (await _client.ReadDatabaseFeedAsync())
                .SingleOrDefault(d => d.Id == databaseName);

            if (_database == null)
                _database = await _client.CreateDatabaseAsync(new Database
                {
                    Id = databaseName
                });
        }

        private async Task CreateCollectionIfNotExistsAsync(string collectionName)
        {
            _collection = (await _client.ReadDocumentCollectionFeedAsync(_database.CollectionsLink))
                .SingleOrDefault(c => c.Id == collectionName);

            if (_collection == null)
                _collection = await _client.CreateDocumentCollectionAsync(_database.SelfLink, new DocumentCollection
                {
                    Id = collectionName
                });
        }

        private Task EmitAsync(LogEvent logEvent)
        {
            return _client.CreateDocumentAsync(_collection.SelfLink, new Data.LogEvent(logEvent, logEvent.RenderMessage(_formatProvider)));
        }
    }
}
