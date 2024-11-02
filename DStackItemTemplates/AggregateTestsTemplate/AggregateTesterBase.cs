using $domain$.Domain.$fileinputname$;

namespace $rootnamespace$.$fileinputname$Tests;

public class $fileinputname$AggregateTester: AggregateTester<$fileinputname$Interactor>
{
    public override void Initialize()
    {
        Tester = new $fileinputname$Interactor(Repository);
    }
}