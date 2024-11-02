namespace $rootnamespace$.$fileinputname$;

public interface I$fileinputname$Interactor : IInteractor { }

public class $fileinputname$Interactor : Interactor<$fileinputname$Aggregate>, I$fileinputname$Interactor
{

    public $fileinputname$Interactor(IAggregateRepository aggRepository)
    {
        AggregateRepository = aggRepository;
    }

    public override async Task ExecuteAsync(object command)
        => await When((dynamic)command);

    async Task When(Create$fileinputname$ c)
        => await IdempotentlyCreateAgg(c.Id, agg => agg.Create$fileinputname$(c));
}
