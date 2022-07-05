using ServiceStack;
using System;
using System.Threading.Tasks;
using TemplateDomain.Common;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public class DomainCommandService : Service
    {
        public IMessageBus Bus { get; set; }
        public ITimeProvider TimeProvider { get; set; }

        protected async Task<ResponseStatus> TryProcessRequest<T>(object command)
        {
            T cmd = command.ConvertTo<T>();
            AddAuditInfoToCommand(cmd as PL.Commands.Command);
            await Bus.Send(cmd);
            return new ResponseStatus();
        }

        protected async Task<ResponseStatus> TryProcessRequest<T>(object command, Action<T> action)
        {
            T cmd = command.ConvertTo<T>();
            AddAuditInfoToCommand(cmd as PL.Commands.Command);
            action(cmd);
            await Bus.Send(cmd).ConfigureAwait(false);
            return new ResponseStatus();
        }

        protected async Task<ResponseStatus> TryProcessRequest<T>(object command, Func<T, Task> func)
        {
            T cmd = command.ConvertTo<T>();
            AddAuditInfoToCommand(cmd as PL.Commands.Command);
            await func(cmd);
            await Bus.Send(cmd).ConfigureAwait(false);
            return new ResponseStatus();
        }

        void AddAuditInfoToCommand(PL.Commands.Command cmd)
        {
            var ses = Request.GetSession();
            cmd.IssuedBy = ses.Email;
            cmd.TimeIssued = TimeProvider.GetUtcTime();
        }
    }
}
