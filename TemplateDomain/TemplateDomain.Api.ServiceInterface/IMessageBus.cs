namespace TemplateDomain.Api.ServiceInterface
{
    public interface IMessageBus
    {
        Task Send(object message);
    }
}
