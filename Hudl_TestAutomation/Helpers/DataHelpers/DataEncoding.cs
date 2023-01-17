namespace Hudl_TestAutomation.Helpers.DataHelpers
{
    public class TestDataAccess
    {
        /// <summary>
            /// Additional encoding types available in the Json data provider. Only needed for data with characters that are not alpha-numeric or common punctuation. 
            /// </summary>
        public enum DataEncoding
        {
            /// <summary>
                    /// Use this when getting test data that is encoded in UTF-8
                    /// </summary>
            UTF8,
            /// <summary>
                    /// Use this when getting test data that is encoded in the Windows 1252 codepage
                    /// </summary>
            Windows1252
        }
    }
}
