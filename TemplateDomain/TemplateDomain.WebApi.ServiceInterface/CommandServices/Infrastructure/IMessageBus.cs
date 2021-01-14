using System.Threading.Tasks;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public interface IMessageBus
    {
        Task Send(object message);
    }
}
