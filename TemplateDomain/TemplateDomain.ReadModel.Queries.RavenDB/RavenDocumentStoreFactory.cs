﻿using Microsoft.Extensions.Configuration;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Security.Cryptography.X509Certificates;

namespace TemplateDomain.ReadModel.Queries.RavenDB
{
    public class RavenConfig
    {
        public string[] Urls { get; set; }
        public string DatabaseName { get; set; }
        public string CertificateFilePath { get; set; }
        public string CertificateFilePassword { get; set; }

        public static RavenConfig FromConfiguration(IConfiguration conf)
            => new RavenConfig { Urls = conf["RavenDB:Urls"].Split(';'), CertificateFilePassword = conf["RavenDB:CertificatePassword"], CertificateFilePath = conf["RavenDB:CertificatePath"], DatabaseName = conf["RavenDB:DatabaseName"] };
    }

    public class RavenDocumentStoreFactory
    {
        public IDocumentStore CreateDocumentStore(RavenConfig conf)
        {
            var store = new DocumentStore { Urls = conf.Urls };
            if (!string.IsNullOrWhiteSpace(conf.CertificateFilePath))
                store.Certificate = new X509Certificate2(conf.CertificateFilePath, conf.CertificateFilePassword);
            store.Database = conf.DatabaseName;
            store.Initialize();
            IndexCreation.CreateIndexes(typeof(Organizations_Search).Assembly, store);
            WarmUpStoreToAvoidLazyLoadingDuringFirstRequest(store);
            return store;
        }

        public IDocumentStore CreateAndInitializeDocumentStore(RavenConfig conf)
        {
            var store = new DocumentStore { Urls = conf.Urls };
            if (!string.IsNullOrWhiteSpace(conf.CertificateFilePath))
                store.Certificate = new X509Certificate2(conf.CertificateFilePath, conf.CertificateFilePassword);
            store.Database = conf.DatabaseName;
            store.Initialize();
            EnsureDatabaseExists(store, conf.DatabaseName, true);
            IndexCreation.CreateIndexes(typeof(Organizations_Search).Assembly, store);
            WarmUpStoreToAvoidLazyLoadingDuringFirstRequest(store);
            return store;
        }

            void EnsureDatabaseExists(IDocumentStore store, string database = null, bool createDatabaseIfNotExists = true)
            {
                database = database ?? store.Database;

                if (string.IsNullOrWhiteSpace(database))
                    throw new ArgumentException("Value cannot be null or whitespace.", nameof(database));

                try
                {
                    store.Maintenance.ForDatabase(database).Send(new GetStatisticsOperation());
                }
                catch (DatabaseDoesNotExistException)
                {
                    if (createDatabaseIfNotExists == false)
                        throw;

                    try
                    {
                        store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(database)));
                    }
                    catch (ConcurrencyException)
                    {
                        // The database was already created before calling CreateDatabaseOperation
                    }
                }
            }

        static void WarmUpStoreToAvoidLazyLoadingDuringFirstRequest(IDocumentStore store)
        {
            using (var ses = store.OpenSession())
            {
                ses.Load<Organization>("Organizations-1");
            }
        }
    }
}