using TemplateDomain.ReadModel;

namespace TemplateDomain.Testing.ReadModel
{
    public static class OrganizationTestData
    {
        public static Organization CreateDefault(string id) => 
            new Organization()
            {
                Id = id,
                Name = "Xamics Ltd",
                Address = AddressTestData.CreateDefault()
            };
    }
}
