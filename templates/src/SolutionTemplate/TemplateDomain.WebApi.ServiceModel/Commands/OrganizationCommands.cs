﻿using ServiceStack;
using $ext_projectname$.Common;

namespace $safeprojectname$
{
    [Route("/organizations", Verbs = "POST")]
    public class RegisterOrganization : IReturn<ResponseStatus>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}