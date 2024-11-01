﻿namespace TemplateDomain.Domain.Organization;

public class OrganizationAggregate :  Aggregate<OrganizationAggregateState>
{
    internal void RegisterOrganization(RegisterOrganization c)
    {
        if (State.Version > 0)
            if (c.IsIdempotent(State))
                return;
            else
                throw DomainError.Named("OrganizationAlreadyExists", string.Empty);

        Apply(c.ToEvent());
    }

    internal void CorrectOrganizationName(CorrectOrganizationName c)
    {
        if (c.IsIdempotent(State))
            return;
        Apply(c.ToEvent());
    }
}
