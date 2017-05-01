using System;
using System.Configuration;
using Core.Interfaces;

namespace Core
{
    public class WebApplicationConfig : IWebApplicationConfig
    {
        public string WebApiUrl => ConfigurationManager.AppSettings["WebApiUrl"];

        public int GroupAbstractLimit  => 
                            int.TryParse(ConfigurationManager.AppSettings["GroupAbstractLimit"], out int result) 
                            ? result 
                            : Constants.DefaultGroupLimit;
    }
}