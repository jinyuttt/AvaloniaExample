using Microsoft.Extensions.Configuration;
using System.IO;

namespace AvaDB
{
    internal class CfgOpt
    {
        static IConfigurationBuilder builder;
        static CfgOpt() {
             builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("config.xml");
        }

    
        public static string[] Read()
        {
            
               
                IConfiguration _configuration = builder.Build();
            string token = _configuration["DB"];

            string[] str = token.Split(';');
            return str;
            
        }
    }
}
