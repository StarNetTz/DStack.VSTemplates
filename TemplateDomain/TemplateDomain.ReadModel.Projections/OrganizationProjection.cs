using TemplateDomain.PL.Events;
using DStack.Projections;
using System.Threading.Tasks;

namespace TemplateDomain.ReadModel.Projections
{
    [SubscribesToStream(StreamName)]
    public class OrganizationProjection : Projection, IHandledBy<OrganizationProjectionHandler>
    {
        public const string StreamName = "$ce-Organizations";
    }

    public class OrganizationProjectionHandler : IHandler
    {
        readonly INoSqlStore Store;

        public OrganizationProjectionHandler(INoSqlStore store)
        {
            Store = store;
        }

        public async Task Handle(dynamic @event, long checkpoint)
            => await When(@event, checkpoint);

        async Task When(OrganizationRegistered e, long checkpoint)
        {
            var doc = await LoadOrCreateDoc(e.Id);
            doc.Id = e.Id;
            doc.Name = e.Name;
            doc.Address = new Address { 
                City = e.Address.City,
                Country = e.Address.Country,
                PostalCode = e.Address.PostalCode,
                State = e.Address.State,
                Street = e.Address.Street
            };
            await Store.StoreAsync(doc);
        }

        async Task<Organization> LoadOrCreateDoc(string id)
        {
            var doc = await Store.LoadAsync<Organization>(id);
            return (doc == null)? new Organization() : doc;
        }
    }
}
