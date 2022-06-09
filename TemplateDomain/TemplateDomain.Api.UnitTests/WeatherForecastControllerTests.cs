using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateDomain.Api.ServiceInterface;
using TemplateDomain.Api.ServiceModel;
using Xunit;

namespace TemplateDomain.Api.UnitTests
{
    
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void ShouldGet()
        {
            var c = new WeatherForecastController();

            var res = c.Get();

            Assert.Equal(5, res.Count());
        }
    }
}
