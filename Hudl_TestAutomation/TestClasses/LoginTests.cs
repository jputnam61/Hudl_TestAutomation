using Hudl_TestAutomation.Configuration;
using Hudl_TestAutomation.Helpers;
using Hudl_TestAutomation.PageObjects.LoginPage;
using NUnit.Framework;

namespace Hudl_TestAutomation.TestClasses
{
    [TestFixture]
    public class LoginTests : TestHelper
    {
        /// <summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        /// This test validates a successful login (happy path)
        /// </summary>
        [Test, Parallelizable]
        [Property("TestId", "1234")]
        public void LoginWithValidCredentials()
        {
            ExecuteTestActionsWithWebdriver((driver) =>
            {

                //Navigate to Login Page
                var loginPage = new LoginPage(driver, EnvironmentConfiguration.GetValue("HudlUrl").ToString());
                var loginTestData = DataProviderAccess.GetTestData("TestData");

                //Validate Page Elements Displayed
                loginPage.EnterUsername(EnvironmentConfiguration.GetValue("UserName").ToString());
                loginPage.EnterPassword(EnvironmentConfiguration.GetValue("Password").ToString());
                loginPage.ClickSubmit();

                //Validate Page has logged in successfully
                loginPage.VerifyLoggedIn();
            });
        }

        /// <summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        /// This test validates a login using bad pass
        /// </summary>
        [Test, Parallelizable]
        [Property("TestId", "1235")]
        public void LoginWithInvalidCredentials_BadPass()
        {
            ExecuteTestActionsWithWebdriver((driver) =>
            {

                //Navigate to Login Page
                var loginPage = new LoginPage(driver, EnvironmentConfiguration.GetValue("HudlUrl").ToString());
                var loginTestData = DataProviderAccess.GetTestData("TestData");

                //Generate random string
                string randStr = DataRandomizer.CreateString(Helpers.Type.AlphaLower, 5);

                //Validate Page Elements Displayed
                loginPage.EnterUsername(EnvironmentConfiguration.GetValue("UserName").ToString());
                loginPage.EnterPassword(loginTestData.GetValue("BadPass").ToString() + randStr);
                loginPage.ClickSubmit();

                //Validate Login error
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(),loginPage.GetErrorText());

            });
        }

        /// <summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        /// This test validates a login using no user input
        /// </summary>
        [Test, Parallelizable]
        [Property("TestId", "1236")]
        public void LoginWithInValidCredentials_NoUserInput()
        {
            ExecuteTestActionsWithWebdriver((driver) =>
            {

                //Navigate to Login Page
                var loginPage = new LoginPage(driver, EnvironmentConfiguration.GetValue("HudlUrl").ToString());
                var loginTestData = DataProviderAccess.GetTestData("TestData");

                //Generate random string
                string randStr = DataRandomizer.CreateString(Helpers.Type.AlphaLower, 5);

                //Attempt to login
                loginPage.EnterUsername(loginTestData.GetValue("NoUser").ToString());
                loginPage.EnterPassword(loginTestData.GetValue("NoPass").ToString());
                loginPage.ClickSubmit();

                //Validate login error
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(),loginPage.GetErrorText());

            });
        }

        /// <summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        /// This test validates an error when too many bad login attempts have been made
        /// </summary>
        [Test, Parallelizable]
        [Property("TestId", "1234")]
        public void LoginWithInValidCredentials_TooManyAttempts()
        {
            ExecuteTestActionsWithWebdriver((driver) =>
            {

                //Navigate to Login Page
                var loginPage = new LoginPage(driver, EnvironmentConfiguration.GetValue("HudlUrl").ToString());
                var loginTestData = DataProviderAccess.GetTestData("TestData");

                //Attempt to login 1
                loginPage.EnterUsername(EnvironmentConfiguration.GetValue("UserName").ToString());
                loginPage.EnterPassword(loginTestData.GetValue("BadPass").ToString());
                loginPage.ClickSubmit();

                //Validate login unsuccessful 1 message 1
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(), loginPage.GetErrorText());

                //Attempt to login 2
                loginPage.EnterPassword(loginTestData.GetValue("BadPass").ToString());
                loginPage.ClickSubmit();

                //Validate login unsuccessful 2 message 1
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(), loginPage.GetErrorText());

                //Attempt to login 3
                //Validate Page Elements Displayed
                loginPage.EnterPassword(loginTestData.GetValue("BadPass").ToString());
                loginPage.ClickSubmit();

                //Validate login unsuccessful 3 message 1
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(), loginPage.GetErrorText());

                //Attempt to login 4
                loginPage.EnterPassword(loginTestData.GetValue("BadPass").ToString());
                loginPage.ClickSubmit();

                //Validate login unsuccessful 4 message 1
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(), loginPage.GetErrorText());

                //Attempt to login 5
                loginPage.EnterPassword(loginTestData.GetValue("BadPass").ToString());
                loginPage.ClickSubmit();

                //Validate login unsuccessful 5 message 1
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(), loginPage.GetErrorText());

                //Attempt to login 6
                loginPage.EnterPassword(loginTestData.GetValue("BadPass").ToString());
                loginPage.ClickSubmit();

                //Validate login unsuccessful 6 message 1
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(), loginPage.GetErrorText());

                //Attempt to login 7
                loginPage.EnterPassword(loginTestData.GetValue("BadPass").ToString());
                loginPage.ClickSubmit();

                //Validate login unsuccessful 7 message 2 (too many attempts, having trouble?)
                Assert.AreEqual(loginTestData.GetValue("ErrorMessageTooMany").ToString(),loginPage.GetErrorText());

            });
        }

        /// <summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        /// This test validates a login when no password has been entered
        /// </summary>
        [Test, Parallelizable]
        [Property("TestId", "1234")]
        public void LoginWithInValidCredentials_NoPassInput()
        {
            ExecuteTestActionsWithWebdriver((driver) =>
            {

                //Navigate to Login Page
                var loginPage = new LoginPage(driver, EnvironmentConfiguration.GetValue("HudlUrl").ToString());
                var loginTestData = DataProviderAccess.GetTestData("TestData");

                //Generate random string
                string randStr = DataRandomizer.CreateString(Helpers.Type.AlphaLower, 5);

                //Attempt to login
                loginPage.EnterUsername(EnvironmentConfiguration.GetValue("UserName").ToString());
                loginPage.EnterPassword(loginTestData.GetValue("NoPass").ToString());
                loginPage.ClickSubmit();

                //Validate login error
                Assert.AreEqual(loginTestData.GetValue("ErrorMessage").ToString(), loginPage.GetErrorText());

            });
        }

        /// <summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        /// This test validates an invalid login with org using an external email
        /// </summary>
        [Test, Parallelizable]
        [Property("TestId", "1234")]
        public void LoginWithInValidCredentials_OrgLoginOutsiteEmail()
        {
            ExecuteTestActionsWithWebdriver((driver) =>
            {

                //Navigate to Login Page
                var loginPage = new LoginPage(driver, EnvironmentConfiguration.GetValue("HudlUrl").ToString());
                var loginTestData = DataProviderAccess.GetTestData("TestData");

                //Generate random string
                string randStr = DataRandomizer.CreateString(Helpers.Type.AlphaLower, 5);

                //Attempt to login via organiziation using outside email
                loginPage.ClickLoginWithOrg();
                loginPage.EnterOrgUsername(EnvironmentConfiguration.GetValue("UserName").ToString());
                loginPage.ClickOrgSubmitButton();

                //Validate org login error
                Assert.AreEqual(loginTestData.GetValue("OrgErrorMessage").ToString(),loginPage.GetOrgErrorText());

            });
        }

        /// <summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        /// This test validates that the page loads the 'need help' page
        /// </summary>
        [Test, Parallelizable]
        [Property("TestId", "1234")]
        public void LoginWithInValidCredentials_NeedHelp()
        {
            ExecuteTestActionsWithWebdriver((driver) =>
            {

                //Navigate to Login Page
                var loginPage = new LoginPage(driver, EnvironmentConfiguration.GetValue("HudlUrl").ToString());

                //Attempt to validate need help page
                loginPage.ClickNeedHelpButton();
                loginPage.ValidateNeedHelpHeader();
                loginPage.ValidateNeedHelpSubmitButton();

            });
        }
    }
}

