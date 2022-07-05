using System;

namespace TemplateDomain.PL.Events
{
    public interface IEvent
    {
        string Id { get; }
    }

    public abstract record Event : IEvent
    {
        public string Id { get; set; }
        public string IssuedBy { get; set; }
        public DateTime TimeIssued { get; set; }
    }
}