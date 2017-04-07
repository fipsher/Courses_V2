using System.Configuration;
using Core.Interfaces;

namespace Courses_v2
{
    public class WebApplicationConfig : IWebApplicationConfig
    {
        public string WebApiUrl => ConfigurationManager.AppSettings["WebApiUrl"];
    }
}