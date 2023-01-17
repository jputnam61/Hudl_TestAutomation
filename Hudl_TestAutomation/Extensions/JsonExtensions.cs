using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Hudl_TestAutomation.Extensions
{
    /// <summary>
        /// Class that contains extension mentions for Json JObject and JArray
        /// </summary>
    public static class JsonExtensions
    {

        /// <summary>
                /// Gets a string from the key provided
                /// </summary>
                /// <param name="json"></param>
                /// <param name="key">Key in the JObject</param>
                /// <param name="filename">name of the environment config file</param>
                /// <returns></returns>
        public static string GetString(this JObject json, string key, string filename = null)
        {
            var value = "";

            try
            {
                value = json[key].ToString();
            }
            catch (Exception e)
            {
                Assert.Fail($"No value found for key: {key}. Exception: {e.Message}. FileName: {filename}");
            }

            return value;
        }

        /// <summary>
                /// Gets a string from the key provided
                /// </summary>
                /// <param name="json"></param>
                /// <param name="key">Key in the JObject</param>
                /// <returns></returns>
        public static string GetDateAsString(this JObject json, string key)
        {
            var value = "";

            try
            {
                value = json[key].ToString();
            }
            catch (Exception e)
            {
                Assert.Fail($"No value found for key: {key}. Exception: {e.Message}");
            }

            return value;
        }

        /// <summary>
                /// Gets a integer from the key provided
                /// </summary>
                /// <param name="json"></param>
                /// <param name="key">Key in the JObject</param>
                /// <returns></returns>
        public static int GetInt(this JObject json, string key)
        {
            var value = 0;

            try
            {
                value = int.Parse(json[key].ToString());
            }
            catch (Exception e)
            {
                Assert.Fail($"Unable to parse value to int for key: {key}. Value: {json[key]}. Exception: {e.Message}");
            }

            return value;
        }

        /// <summary>
                /// Gets a long from the key provided
                /// </summary>
                /// <param name="json"></param>
                /// <param name="key">Key in the JObject</param>
                /// <returns></returns>
        public static long GetLong(this JObject json, string key)
        {
            var value = 0L;

            try
            {
                value = long.Parse(json[key].ToString());
            }
            catch (Exception e)
            {
                Assert.Fail($"Unable to parse value to long for key: {key}. Value: {json[key]}. Exception: {e.Message}");
            }

            return value;
        }

        /// <summary>
                /// Gets a double from the key provided
                /// </summary>
                /// <param name="json"></param>
                /// <param name="key">Key in the JObject</param>
                /// <returns></returns>
        public static double GetDouble(this JObject json, string key)
        {
            var value = 0d;

            try
            {
                value = double.Parse(json[key].ToString());
            }
            catch (Exception e)
            {
                Assert.Fail($"Unable to parse value to double for key: {key}. Value: {json[key]}. Exception: {e.Message}");
            }

            return value;
        }

        /// <summary>
                /// Gets a decimal from the key provided
                /// </summary>
                /// <param name="json"></param>
                /// <param name="key">Key in the JObject</param>
                /// <returns></returns>
        public static decimal GetDecimal(this JObject json, string key)
        {
            var value = 0m;

            try
            {
                value = decimal.Parse(json[key].ToString());
            }
            catch (Exception e)
            {
                Assert.Fail($"Unable to parse value to decimal for key: {key}. Value: {json[key]}. Exception: {e.Message}");
            }

            return value;
        }

        /// <summary>
                /// Gets a bool from the key provided
                /// </summary>
                /// <param name="json"></param>
                /// <param name="key">Key in the JObject</param>
                /// <returns></returns>
        public static bool GetBool(this JObject json, string key)
        {
            bool value = false;

            try
            {
                value = bool.Parse(json[key].ToString());
            }
            catch (Exception e)
            {
                Assert.Fail($"Unable to parse value to bool for key: {key}. Value: {json[key]}. Exception: {e.Message}");
            }

            return value;
        }

        /// <summary>
                /// Gets a DateTime from the key provided
                /// </summary>
                /// <param name="json"></param>
                /// <param name="key">Key in the JObject</param>
                /// <returns></returns>
        public static DateTime GetDateTime(this JObject json, string key)
        {
            var value = new DateTime();

            try
            {
                value = DateTime.Parse(json[key].ToString());
            }
            catch (Exception e)
            {
                Assert.Fail($"Unable to parse value to DateTime for key: {key}. Value: {json[key]}. Exception: {e.Message}");
            }

            return value;
        }
    }
}



