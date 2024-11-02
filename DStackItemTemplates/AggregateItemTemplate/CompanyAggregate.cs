namespace $rootnamespace$.$fileinputname$;

public class $fileinputname$Aggregate : Aggregate<$fileinputname$AggregateState>
{
    internal void Create$fileinputname$(Create$fileinputname$ c)
    {
        if (State.Version > 0)
            if (c.IsIdempotent(State))
                return;
            else
                throw DomainError.Named("$fileinputname$AlreadyExists", string.Empty); 
    
        Apply(c.ToEvent());
    }
}
