using Raven.Client.Documents;

namespace TemplateDomain.ReadModel.Queries.RavenDB;

public abstract class QueryBase<T>
{
    protected readonly IDocumentStore DocumentStore;

    public QueryBase(IDocumentStore documentStore)
    {
        DocumentStore = documentStore;
    }

    public async Task<PaginatedResult<T>> Execute(PaginatedQueryRequest qry)
    {
        QueryResult<T> qResult = await ExecuteAsync(qry);
        var resp = ToPaginatedResult(qry, qResult);
        if (CurrentPageIsOverflown(resp))
            return await Execute(new PaginatedQueryRequest() { Qry = qry.Qry, CurrentPage = 0, PageSize = qry.PageSize });
        return resp;
    }

        protected abstract Task<QueryResult<T>> ExecuteAsync(PaginatedQueryRequest qry);

        protected PaginatedResult<T> ToPaginatedResult(PaginatedQueryRequest request, QueryResult<T> qr)
        {
            var totalItems = qr.Statistics.TotalResults;
            var totalPages = totalItems / request.PageSize;
            if ((totalItems % request.PageSize) > 0)
                totalPages += 1;

            return new PaginatedResult<T>()
            {
                Data = qr.Data,
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage
            };
        }

        static bool CurrentPageIsOverflown(PaginatedResult<T> result)
            => (result.Data.Count == 0) && (result.TotalPages > 0);

    protected string GetParamValue(PaginatedQueryRequest req, string key)
    {
        return req.Qry.ContainsKey(key) ?
            req.Qry[key] :
            string.Empty;
    }
}