
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.TestDriver;

namespace TemplateDomain.ReadModel.Queries.RavenDB.IntegrationTests;

public class DocumentStoreFixture : RavenTestDriver
{
    public IDocumentStore DocumentStore;
    static bool IsConfigured = false;

    public DocumentStoreFixture()
    {
        if (!IsConfigured)
        {
            ConfigureServer(new TestServerOptions { Licensing = new Raven.Embedded.ServerOptions.LicensingOptions { ThrowOnInvalidOrMissingLicense = false } });
            IsConfigured = true;
        }
        DocumentStore = GetDocumentStore();
        CreateTestDocuments();
        WaitForIndexing(DocumentStore);
    }

    void CreateTestDocuments()
    {
        using (var s = DocumentStore.OpenSession())
        {
            s.Store(new Organization { Id = $"{Consts.IdPrefixes.Organization}1", Name = "Slime Ltd" });
            s.Store(new Organization { Id = $"{Consts.IdPrefixes.Organization}2", Name = "Blood Inc." });
            s.SaveChanges();
        }
    }

    protected override void SetupDatabase(IDocumentStore documentStore)
    {
        base.SetupDatabase(documentStore);
        IndexCreation.CreateIndexes(typeof(Organizations_Search).Assembly, documentStore);
    }
}