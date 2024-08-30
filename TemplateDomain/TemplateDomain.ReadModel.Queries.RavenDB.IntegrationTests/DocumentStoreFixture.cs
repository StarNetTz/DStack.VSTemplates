
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.TestDriver;

namespace TemplateDomain.ReadModel.Queries.RavenDB.IntegrationTests;

public class DocumentStoreFixture : RavenTestDriver
{
    public IDocumentStore DocumentStore;

    public DocumentStoreFixture()
    {
        DocumentStore = GetDocumentStore();
        CreateTestDocuments();
        WaitForIndexing(DocumentStore);
    }

    void CreateTestDocuments()
    {
        using (var s = DocumentStore.OpenSession())
        {
            s.Store(new Organization { Id = "Organizations-1", Name = "Slime Ltd" });
            s.Store(new Organization { Id = "Organizations-2", Name = "Blood Inc." });
            s.SaveChanges();
        }
    }

    protected override void SetupDatabase(IDocumentStore documentStore)
    {
        base.SetupDatabase(documentStore);
        IndexCreation.CreateIndexes(typeof(Organizations_Search).Assembly, documentStore);
    }
}