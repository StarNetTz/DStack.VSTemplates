using $domain$.Domain.$fileinputname$;
using $domain$.PL.Commands;
using $domain$.PL.Events;
using DStack.Aggregates.Testing;
using System.Linq;
using System.Threading.Tasks;

namespace $rootnamespace$.$fileinputname$Tests
{
    public class $fileinputname$AggregateTester: AggregateTesterBase<ICommand, IEvent>
    {
        protected override async Task<ExecuteCommandResult<IEvent>> ExecuteCommand(IEvent[] given, ICommand cmd)
        {
            var repository = new BDDAggregateRepository();
            repository.Preload(cmd.Id, given);
            var svc = new $fileinputname$Interactor(repository);
            await svc.ExecuteAsync(cmd).ConfigureAwait(false);
            var arr = repository.Appended != null ? repository.Appended.Cast<IEvent>().ToArray() : null;
			var publishedEvents = svc.GetPublishedEvents();
			return new ExecuteCommandResult<IEvent> { ProducedEvents = arr ?? new IEvent[0], PublishedEvents = publishedEvents.Cast<IEvent>().ToArray() };
        }
    }
}