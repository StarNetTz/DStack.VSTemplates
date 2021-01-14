using $ext_projectname$.Common;

namespace $safeprojectname$.Commands
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