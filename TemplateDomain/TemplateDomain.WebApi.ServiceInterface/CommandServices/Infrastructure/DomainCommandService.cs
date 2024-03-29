﻿using ServiceStack;
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

        void AddAuditInfoToCommand(PL.Commands.Command cmd)
        {
            var ses = Request.GetSession();
            cmd.IssuedBy = ses.Email;
            cmd.TimeIssued = TimeProvider.GetUtcTime();
        }
    }
}
