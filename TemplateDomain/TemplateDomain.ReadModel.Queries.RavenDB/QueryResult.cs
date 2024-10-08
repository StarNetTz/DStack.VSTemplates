﻿using Raven.Client.Documents.Session;

namespace TemplateDomain.ReadModel.Queries.RavenDB;

public class QueryResult<T>
{
    public List<T> Data { get; set; }

    public QueryStatistics Statistics { get; set; }

    public QueryResult()
    {
        Data = new List<T>();
    }
}