using TemplateDomain.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DStack.Aggregates;

namespace TemplateDomain.App
{
    public class AggregateInteractorsExtractor
    {
        public static List<Type> GetInteractors()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(OrganizationInteractor));
            return assembly.GetTypes().Where(p => typeof(IInteractor).IsAssignableFrom(p) && p.IsClass).ToList();
        }
    }
}