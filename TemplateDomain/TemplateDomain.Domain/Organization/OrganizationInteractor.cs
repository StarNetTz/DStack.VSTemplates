﻿namespace TemplateDomain.Domain.Organization;

public interface IOrganizationInteractor : IInteractor { }

public class OrganizationInteractor : Interactor, IOrganizationInteractor
{
    readonly IAggregateRepository AggRepository;

    public OrganizationInteractor(IAggregateRepository aggRepository)
    {
        AggRepository = aggRepository;
    }

    async Task IdempotentlyCreateAgg(string id, Action<OrganizationAggregate> usingThisMethod)
    {
        var agg = await AggRepository.GetAsync<OrganizationAggregate>(id);
        if (agg == null)
            agg = new OrganizationAggregate(new OrganizationAggregateState());
        var ov = agg.Version;
        usingThisMethod(agg);
        PublishedEvents = agg.PublishedEvents;
        if (ov != agg.Version)
            await AggRepository.StoreAsync(agg);
    }

    async Task IdempotentlyUpdateAgg(string id, Action<OrganizationAggregate> usingThisMethod)
    {
        var agg = await AggRepository.GetAsync<OrganizationAggregate>(id);
        if (agg == null)
            throw DomainError.Named("OrganizationDoesNotExist", string.Empty);
        var ov = agg.Version;
        usingThisMethod(agg);
        PublishedEvents = agg.PublishedEvents;
        if (ov != agg.Version)
            await AggRepository.StoreAsync(agg);
    }

    public override async Task ExecuteAsync(object command)
        => await When((dynamic)command);

    async Task When(RegisterOrganization c)
        => await IdempotentlyCreateAgg(c.Id, agg => agg.RegisterOrganization(c));

    async Task When(CorrectOrganizationName c)
        => await IdempotentlyUpdateAgg(c.Id, agg => agg.CorrectOrganizationName(c));
}