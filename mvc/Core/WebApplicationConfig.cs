using System.Configuration;
using Core.Interfaces;

namespace Core
{
    public class WebApplicationConfig : IWebApplicationConfig
    {
        public string WebApiUrl => ConfigurationManager.AppSettings["WebApiUrl"];
    }
}