using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TicTacToeWebAPI.Tests.Config
{
    public class TestServerSingleton
    {
        private static TestServer TestServer;
        private static TestServerSingleton _instance = new TestServerSingleton();
        public static TestServerSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TestServerSingleton();
                return _instance;
            }
        }

        private TestServerSingleton() { }

        public void InitializeTestServer()
        {
            IConfigurationRoot devConfiguration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            IWebHostBuilder builder = new WebHostBuilder()
                .UseConfiguration(devConfiguration)
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
        }

        public HttpClient GetHttpClient()
        {
            return TestServer.CreateClient();
        }
    }
}
