﻿using ServiceStack;

namespace TemplateDomain.WebApi.ServiceModel;

[Route("/organizations", Verbs = "POST")]
public class RegisterOrganization : IReturn<ResponseStatus>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
}