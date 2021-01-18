using $domain$.Common;
using $domain$.PL.Events;
using DStack.Aggregates;

namespace $rootnamespace$.$fileinputname$
{
    public class $fileinputname$AggregateState : AggregateState
    {
        internal string Name { get; set; }

        protected override void DelegateWhenToConcreteClass(object ev)
            => When((dynamic)ev);

        void When($fileinputname$Created e)
        {
            Id = e.Id;
            Name = e.Name;
        }
    }
}