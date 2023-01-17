using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Text;
using static Hudl_TestAutomation.Helpers.DataHelpers.TestDataAccess;

namespace Hudl_TestAutomation.Helpers
{
    /// <summary>
    /// Class used to access data provider file for test
    /// </summary>
    public class DataProviderAccess
    {
        private static HudlLogger _log = new HudlLogger("DataProviderAccess");


        /// <summary>
                /// Retrieves the test data string from the test data provider json file using the column parameter.
                /// </summary>
                /// <param name="column"></param>
                /// <param name="dataEncoding"></param>
                /// <returns></returns>
        public static JObject GetTestData(string column, DataEncoding dataEncoding = new DataEncoding())
        {
            JObject data = null;
            var fileName = string.Empty;
            var testName = TestContext.CurrentContext.Test.Name;

            // set encoding
            Encoding encoding;
            switch (dataEncoding)
            {
                case DataEncoding.UTF8:
                    encoding = Encoding.UTF8;
                    break;
                case DataEncoding.Windows1252:
                    encoding = Encoding.GetEncoding(1252);
                    break;
                default:
                    encoding = Encoding.ASCII;
                    break;
            }

            try
            {
                fileName = $"{TestContext.CurrentContext.Test.ClassName.Split('.').Last()}_Data.json";
                var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}//TestData//{fileName}";

                var fileData = File.ReadAllText(filePath, encoding);
                var json = JsonConvert.DeserializeObject<JObject>(fileData,
                new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                if (json[testName] == null)
                {
                    _log.Error($"Could not find test {testName} in file {fileName}");
                    Assert.Fail($"Could not find test {testName} in file {fileName}");
                }

                if (json[testName][column] == null)
                {
                    _log.Error($"Could not find column {column} in test {testName}");
                    Assert.Fail($"Could not find column {column} in test {testName}");
                }

                data = (JObject)json[testName][column];
            }
            catch (FileNotFoundException fe)
            {
                _log.Error($"Could not find file {fileName}. Double check that Copy To Ouput Directory is set to 'Always'. {fe.Message}");
                Assert.Fail($"Could not find file {fileName}. Double check that Copy To Ouput Directory is set to 'Always'. {fe.Message}");
            }
            catch (Exception e)
            {
                _log.Error(
                $"Could not retrieve test data for test '{testName} in data provider '{TestContext.CurrentContext.Test.ClassName.Split('.').Last()}_Data'. {e.Message}");
                Assert.Fail(
                $"Could not retrieve test data for test '{testName} in data provider '{TestContext.CurrentContext.Test.ClassName.Split('.').Last()}_Data'. {e.Message}");
            }

            return data;
        }
    }
}
