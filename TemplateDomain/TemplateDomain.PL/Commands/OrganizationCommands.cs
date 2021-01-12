namespace TemplateDomain.PL.Commands
{
    public record RegisterOrganization : Command
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public record CorrectOrganizationName : Command
    {
        public string Name { get; set; }
    }
}