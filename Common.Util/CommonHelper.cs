using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Util
{
    public static class CommonHelper
    {
        private static IConfigurationRoot Configuracao
        {
            get
            {
                return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            }
        }

        public static string UrlScore
        {
            get
            {
                return Configuracao.GetSection("UrlScore").Value;
            }
        }

        public static string UrlCommitment
        {
            get
            {
                return Configuracao.GetSection("UrlCommitment").Value;
            }
        }
    }
}
