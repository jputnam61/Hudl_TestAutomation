using Newtonsoft.Json.Linq;
using NLog;
using NUnit.Framework;
using System;
using System.Configuration;
using Newtonsoft.Json;
using Hudl_TestAutomation.Extensions;
using Hudl_TestAutomation.Helpers.DataHelpers;

namespace Hudl_TestAutomation.Configuration
{
    /// <summary>
        /// Class used to access environment configuration files that are json
        /// </summary>
    public static class EnvironmentConfiguration
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        /// <summary>
                /// Gets the environment json config file value with the specified key
                /// </summary>
                /// <param name="key"></param>
                /// <returns></returns>
        public static string GetValue(string key)
        {
            var json = GetConfigurations();
            return json.GetString(key, $"{ ConfigurationManager.AppSettings["Environment"]}.json");
        }

        private static JObject GetConfigurations()
        {
            var environment = string.Empty;

            try
            {
                environment = ConfigurationManager.AppSettings["Environment"];
                var jsonFile = FileHelper.RetrieveFileContents($"Configurations\\{environment}.json");

                return JsonConvert.DeserializeObject<JObject>(jsonFile,
                new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
            }
            catch (Exception e)
            {
                _log.Error($"Error reading configurations from file. Environment: {environment}. Error: {e.Message}");
                Assert.Fail($"Error reading configurations from file. Environment: {environment}. Error: {e.Message}");
            }

            return null;
        }
    }
}


