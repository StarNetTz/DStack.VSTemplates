using $domain$.Common;
using $domain$.PL.Commands;
using $domain$.PL.Events;
using DStack.Aggregates;

namespace $rootnamespace$.$fileinputname$
{
    public static class $fileinputname$CmdExtensions 
    {
       public static $fileinputname$Created ToEvent(this Create$fileinputname$ cmd)
        {
            return new  $fileinputname$Created()
            {
                Id = cmd.Id,
                AuditInfo = cmd.AuditInfo,
                Name = cmd.Name
            };
        }

        public static bool IsIdempotent(this Create$fileinputname$ cmd, $fileinputname$AggregateState state)
        {
            return cmd.Name == state.Name;
        }
    }
}