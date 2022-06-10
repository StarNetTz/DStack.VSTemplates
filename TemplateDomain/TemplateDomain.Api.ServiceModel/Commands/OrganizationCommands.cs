using TemplateDomain.Common;

namespace TemplateDomain.Api.ServiceModel
{
    public class RegisterOrganization 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}