using TemplateDomain.Domain.Organization;

namespace TemplateDomain.Domain.UnitTests.OrganizationTests;


/// <summary>
/// An example of overriden generic AggregateTester
/// </summary>
public class OrganizationAggregateTester : AggregateTester<OrganizationInteractor>
{
    public override void Initialize()
    {
        Tester = new OrganizationInteractor(Repository);
    }
}