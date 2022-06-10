using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDomain.Api.ServiceInterface.MapperProfiles
{
    public class CommandProfile : Profile
    {

        //Conventionbased possibility
        public CommandProfile()
        {
            CreateMap<ServiceModel.RegisterOrganization, PL.Commands.RegisterOrganization>();
        }
    }
}
