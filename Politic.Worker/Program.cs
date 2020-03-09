using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Common.DataAccess;
using Common.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Politic.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

        Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .Build();
                    
                    services.AddHostedService<Worker>();

                    var awsOptions = configuration.GetAWSOptions();
                    services.AddDefaultAWSOptions(awsOptions);

                    var client = awsOptions.CreateServiceClient<IAmazonDynamoDB>();

                    services.AddSingleton<IDBContext<Loan>>(provider => new DBContext<Loan>(client));
                    services.AddSingleton<IDBContext<Interest>>(provider => new DBContext<Interest>(client));
                });
    }
}
