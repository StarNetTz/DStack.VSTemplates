using TemplateDomain.Domain.Organization;

namespace TemplateDomain.Domain.UnitTests.OrganizationTests;

public class OrganizationAggregateTester : AggregateTesterBase<ICommand, IEvent>
{
    protected override async Task<ExecuteCommandResult<IEvent>> ExecuteCommand(IEvent[] given, ICommand cmd)
    {
        var repository = new BDDAggregateRepository();
        repository.Preload(cmd.Id, given);
        var svc = new OrganizationInteractor(repository);
        await svc.ExecuteAsync(cmd).ConfigureAwait(false);
        var publishedEvents = svc.GetPublishedEvents();
        var arr = (repository.Appended != null) ? repository.Appended.Cast<IEvent>().ToArray() : null;
        return new ExecuteCommandResult<IEvent> { ProducedEvents = arr ?? new IEvent[0], PublishedEvents = publishedEvents.Cast<IEvent>().ToArray() };
    }
}