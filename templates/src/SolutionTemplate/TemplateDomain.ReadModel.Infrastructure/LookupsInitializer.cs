using DStack.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public interface ILookupsInitializer
    {
        Task Initialize();
    }
    public class LookupsInitializer : ILookupsInitializer
    {
        INoSqlStore Store;

        public LookupsInitializer(INoSqlStore store)
        {
            Store = store;
        }

        public async Task Initialize()
        {
            await CreateTimezones();
            await CreateLanguages();
        }

        async Task CreateTimezones()
        {
            const string DocumentId = "lookups-timezones";
            List<LookupItem> data = (from t in TimeZoneInfo.GetSystemTimeZones() select new LookupItem { Id = t.Id, Value = t.DisplayName }).ToList();
            await CreateLookupDocument(DocumentId, data);
        }

        async Task CreateLanguages()
        {
            const string DocumentId = "lookups-languages";
            List<LookupItem> data = new List<LookupItem>() {
                            new LookupItem { Id = "en", Value = "English" },
                            new LookupItem { Id = "de", Value = "German" }
                        };

            await CreateLookupDocument(DocumentId, data);
        }

        async Task CreateLookupDocument(string DocumentId, List<LookupItem> data)
        {

            var doc = await Store.LoadAsync<Lookup>(DocumentId);
            if (null != doc)
                return;
            doc = new Lookup { Id = DocumentId, Data = data };
            await Store.StoreAsync(doc);
        }
    }
}
