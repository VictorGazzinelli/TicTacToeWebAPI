using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWebAPI.Tests.Config
{
    [TestClass]
    public abstract class TestBase
    {
        const string LOCALHOST_API_URL = @"https://localhost:44386/";

        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            // Runs before all tests
            TestServerSingleton.Instance.InitializeTestServer();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Runs before each test
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Runs after each test
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            // Runs after all tests
        }

        public static async Task<O> CallEndpoint<O>(string endpointPath, HttpMethod httpMethod, Dictionary<string, object> input)
        {
            HttpResponseMessage HttpResponseMessage;
            string ResponseBody = string.Empty;

            using (HttpClient httpClient = TestServerSingleton.Instance.GetHttpClient())
            {
                switch (httpMethod)
                {
                    case HttpMethod m when m == HttpMethod.Post:
                        HttpResponseMessage = await httpClient.PostAsync(LOCALHOST_API_URL + @endpointPath, ParseInputAsRequestBody(input));
                        break;
                    default:
                        throw new Exception("HTTP Method not found");
                }

                //HttpResponseMessage.EnsureSuccessStatusCode();

                ResponseBody = await HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<O>(ResponseBody);
        }

        private static StringContent ParseInputAsRequestBody(Dictionary<string, object> input)
        {
            if (input == null)
                return new StringContent(String.Empty);

            return new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
        }
    }
}
