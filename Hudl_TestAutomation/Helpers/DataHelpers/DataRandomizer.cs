using System;
using System.Linq;

namespace Hudl_TestAutomation.Helpers
{
    /// <summary>
    /// Represents the type of data you wish to create
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// Used to create data with all upper case alpha characters
        /// </summary>
        AlphaUpper,
        /// <summary>
        /// Used to create data with all lower case alpha characters
        /// </summary>
        AlphaLower,
        /// <summary>
        /// Used to create data with a mix of upper and lower characters
        /// </summary>
        AlphaUpperLower,
        /// <summary>
        /// Used to create data with a mix of Upper case alpha characters and numeric characters
        /// </summary>
        AlphaUpperNumeric,
        /// <summary>
        /// Used to create data with a mix of lower case alpha characters and numeric characters
        /// </summary>
        AlphaLowerNumeric,
        /// <summary>
        /// Used to create data with numeric characters
        /// </summary>
        Numeric,
        /// <summary>
        /// Used to create data with a mix of alpha upper and lower case characters as well as numeric characters
        /// </summary>
        AlphaUpperLowerNumeric

    }

    /// <summary>
    /// Used to randomize data for testing
    /// </summary>
    public static class DataRandomizer
    {
        private const string Numeric = "1234567890";
        private const string Alpha = "abcdefghijklmnopqrstuvwxyz";
        private static readonly Random Random = new Random();

        /// <summary>
        /// <para><b>Description: </b> Creates a random string of a specified type and length.</para>
        /// <para>The type parameter is an enum called "Type". This lets you choose what type of string you want to use 
        /// (i.e. Type.AlphaUpper, Type.AlphaLower, Type.Numeric, etc.).</para>
        /// 
        /// <para>The length parameter is the length of the random string.</para>
        /// </summary>
        /// <param name="type"></param> 
        /// <param name="length"></param>
        /// <returns>Randomly generated string.</returns>
        public static string CreateString(Type type, int length)
        {
            string value;

            switch (type)
            {
                case Type.AlphaUpper:
                    value = new string(Enumerable.Repeat(Alpha.ToUpper(), length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
                    break;
                case Type.AlphaLower:
                    value = new string(Enumerable.Repeat(Alpha.ToLower(), length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
                    break;
                case Type.AlphaUpperLower:
                    value = new string(Enumerable.Repeat(Alpha.ToLower() + Alpha.ToUpper(), length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
                    break;
                case Type.AlphaUpperNumeric:
                    value = new string(Enumerable.Repeat(Alpha.ToUpper() + Numeric, length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
                    break;
                case Type.AlphaLowerNumeric:
                    value = new string(Enumerable.Repeat(Alpha.ToLower() + Numeric, length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
                    break;
                case Type.Numeric:
                    value = new string(Enumerable.Repeat(Numeric, length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
                    break;
                case Type.AlphaUpperLowerNumeric:
                    value = new string(Enumerable.Repeat(Alpha.ToUpper() + Alpha.ToLower() + Numeric, length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
                    break;
                default:
                    value = string.Empty;
                    break;
            }

            return value;
        }

        /// <summary>
        /// <para><b>Description: </b> Creates a random integer.</para>
        /// <para>The length parameter is the length of the integer. It currently has a max length of 9 characters.</para>
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Randomly generated integer up to 9 characters.</returns>
        public static int CreateInt(int length)
        {
            // making sure that the random number doesn't exceed the integer limit
            if (length > 9)
            {
                return 0;
            }

            var num = string.Empty;

            for (var x = 1; x <= length; x++)
            {
                num += Random.Next(0, 9).ToString();
            }

            // checking to see if the random number starts with a 0. if it does then the 0 gets removed
            // and added to the end of the number.
            if (num.StartsWith("0"))
            {
                num = num.Remove(0, 1);
                num += "0";
            }

            return int.Parse(num);
        }
    }
}

