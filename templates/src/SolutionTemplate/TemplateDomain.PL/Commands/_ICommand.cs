using System;

namespace $safeprojectname$.Commands
{
    public interface ICommand
    {
        string Id { get; }
    }

    public abstract record Command : ICommand
    {
        public string Id { get; set; }
        public string IssuedBy { get; set; }
        public DateTime TimeIssued { get; set; }
    }
}