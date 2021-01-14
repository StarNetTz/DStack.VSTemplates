using Raven.Client.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public abstract class SearchQuery<T>
    {
        protected readonly IDocumentStore DocumentStore;

        public SearchQuery(IDocumentStore documentStore)
        {
            DocumentStore = documentStore;
        }

        public async Task<PaginatedResult<T>> Execute(ISearchQueryRequest qry)
        {
            QueryResult<T> qResult = await SearchAsync(qry);
            var resp = CreateResponse(qry, qResult);
            if (CurrentPageIsOverflown(resp))
                return await Execute(new SearchQueryRequest() { Qry = qry.Qry, CurrentPage = 0, PageSize = qry.PageSize });
            return resp;
        }

        protected abstract Task<QueryResult<T>> SearchAsync(ISearchQueryRequest qry);

        protected PaginatedResult<T> CreateResponse(ISearchQueryRequest request, QueryResult<T> qr)
        {
            PaginatedResult<T> retVal = new PaginatedResult<T>() { Data = new List<T>() };
            retVal.Data = qr.Data;
            retVal.TotalItems = qr.Statistics.TotalResults;
            retVal.TotalPages = retVal.TotalItems / request.PageSize;
            if ((retVal.TotalItems % request.PageSize) > 0)
                retVal.TotalPages += 1;
            retVal.PageSize = request.PageSize;
            retVal.CurrentPage = request.CurrentPage;
            return retVal;
        }

        static bool CurrentPageIsOverflown(PaginatedResult<T> result)
            => (result.Data.Count == 0) && (result.TotalPages > 0);
    }
}