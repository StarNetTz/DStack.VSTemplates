using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TemplateDomain.Common;

namespace TemplateDomain.Api.ServiceInterface
{
    [ApiController]
    public class CommandControllerBase : ControllerBase
    {
        readonly IMessageBus Bus;
        readonly ITimeProvider TimeProvider;
        readonly IMapper Mapper;

        public CommandControllerBase(IMessageBus bus, ITimeProvider timeProvider, IMapper mapper)
        {
            Bus = bus;
            TimeProvider = timeProvider;
            Mapper = mapper;
        }

        protected async Task TryProcessRequest<T>(object command)
        {
            T cmd = Mapper.Map<T>(command);
            AddAuditInfoToCommand(cmd as PL.Commands.Command);
            await Bus.Send(cmd);
        }

        void AddAuditInfoToCommand(PL.Commands.Command cmd)
        {
            cmd.IssuedBy = User.Identity.Name;
            cmd.TimeIssued = TimeProvider.GetUtcTime();
        }
    }
}