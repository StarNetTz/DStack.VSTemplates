﻿using TemplateDomain.ReadModel;
using ServiceStack;

namespace TemplateDomain.WebApi.ServiceModel;

[Route("/lookups")]
public class GetLookup : IReturn<Lookup>
{
    public string Id { get; set; }
}
