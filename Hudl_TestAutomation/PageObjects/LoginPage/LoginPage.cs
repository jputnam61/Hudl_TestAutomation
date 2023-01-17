using Hudl_TestAutomation.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Hudl_TestAutomation.PageObjects.LoginPage
{
    public class LoginPage
    {
        protected static HudlLogger log = new HudlLogger();
        private IWebDriver driver;
        private ElementsActions eActions;

        //Initialize Page
        public LoginPage(IWebDriver driver, string url)
        {
            this.driver = driver;
            this.driver.Url = url;
            eActions = new ElementsActions(driver);
        }

        // <summary>
        // Verifies login successful
        // Author: Jacob Putnam
        // Date: 01/13/2023
        // </summary>
        // <returns></returns>
        public void VerifyLoggedIn()
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.MyAccountMenu, 30);
            }
            catch
            {
                log.Error($"Unable to find my account menu");
                Assert.Fail($"Unable to find my account menu");
            }
        }

        // <summary>
        // Validates username displayed
        // Author: Jacob Putnam
        // Date: 01/13/2023
        // </summary>
        // <returns></returns>
        public bool IsUserNameDisplayed()
        {
            var username = false;
            try
            {
                username = eActions.IsElementDisplayed(LoginPageConsts.UserNameTextBox);
                log.Info($"found username");
            }
            catch (Exception e)
            {
                log.Error($"Unable to find username. {e.Message}");
                Assert.Fail($"Unable to find username. {e.Message}");
            }
            return username;
        }

        // <summary>
        // Attempts to enter username
        // Author: Jacob Putnam
        // Date: 01/13/2023
        // </summary>
        // <returns></returns>
        public void EnterUsername(string username)
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.UserNameTextBox, 5);
                eActions.Type(LoginPageConsts.UserNameTextBox, username);
                log.Info($"entered username: {username}.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to enter username. {e.Message}");
                Assert.Fail($"Unable to enter username. {e.Message}");
            }
        }

        // <summary>
        // Attempts to enter org username
        // Author: Jacob Putnam
        // Date: 01/13/2023
        // </summary>
        // <returns></returns>
        public void EnterOrgUsername(string username)
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.LoginWithOrgTextBox,5);
                eActions.Type(LoginPageConsts.LoginWithOrgTextBox, username);
                log.Info($"entered org username: {username}.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to enter org username. {e.Message}");
                Assert.Fail($"Unable to enter org username. {e.Message}");
            }
        }

        // <summary>
        // Attempts to enter password
        // Author: Jacob Putnam
        // Date: 01/13/2023
        // </summary>
        // <returns></returns>
        public void EnterPassword(string password)
        {
            try
            {
                IsUserNameDisplayed();
                eActions.Type(LoginPageConsts.PasswordTextbox, password);
                log.Info($"entered password: {password}.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to enter password. {e.Message}");
                Assert.Fail($"Unable to enter password. {e.Message}");
            }
        }

        /// <summary>
        /// Attempts to Click submit
        /// </summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        public void ClickSubmit()
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.SubmitButton, 5);
                eActions.Click(LoginPageConsts.SubmitButton);

                log.Info("Clicked Submit.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to click submit. {e.Message}");
                Assert.Fail($"Unable to click submit. {e.Message}");
            }
        }

        /// <summary>
        /// Attempts to Click Login with org
        /// </summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        public void ClickLoginWithOrg()
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.LoginWithOrgButton, 5);
                eActions.Click(LoginPageConsts.LoginWithOrgButton);

                log.Info("Clicked the Login with org button.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to click Login  with org. {e.Message}");
                Assert.Fail($"Unable to click Login with org. {e.Message}");
            }
        }

        /// <summary>
        /// Attempts to Click org submit
        /// </summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        public void ClickOrgSubmitButton()
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.LoginWithOrgSubmit, 5);
                eActions.Click(LoginPageConsts.LoginWithOrgSubmit);

                log.Info("Clicked the org submit.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to click org submit. {e.Message}");
                Assert.Fail($"Unable to click org submit. {e.Message}");
            }
        }
        // <summary>
        // Attempts to verify login error message
        // Author: Jacob Putnam
        // Date: 01/13/2023
        // </summary>
        // <returns></returns>
        public string GetErrorText()
        {
            string errorMessage = string.Empty;
            try
            {

                eActions.WaitForElementToBeDisplayed(LoginPageConsts.ErorrText, 5);
                errorMessage = eActions.GetText(LoginPageConsts.ErorrText);
                log.Info($"verified error text: {errorMessage}.");
            }
            catch (Exception e)
            {
                log.Error($"unable to verify error text. {e.Message}");
                Assert.Fail($"unable to verify error text. {e.Message}");
            }
            return errorMessage;
        }

        // <summary>
        // Attempts to verify org error message
        // Author: Jacob Putnam
        // Date: 01/13/2023
        // </summary>
        // <returns></returns>
        public string GetOrgErrorText()
        {
            string errorMessage = string.Empty;
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.LoginWithOrgErrorText, 5);
                errorMessage = eActions.GetText(LoginPageConsts.LoginWithOrgErrorText);
                log.Info($"verified org error text: {errorMessage}.");
            }
            catch (Exception e)
            {
                log.Error($"unable to verify org error text. {e.Message}");
                Assert.Fail($"unable to verify org error text. {e.Message}");
            }
            return errorMessage;
        }

        /// <summary>
        /// Attempts to Click Need Help
        /// </summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        public void ClickNeedHelpButton()
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.NeedHelpButton, 5);
                eActions.Click(LoginPageConsts.NeedHelpButton);

                log.Info("Clicked the Need help button.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to click need help button. {e.Message}");
                Assert.Fail($"Unable to click need help button. {e.Message}");
            }
        }

        /// <summary>
        /// Verifies the Need Help header
        /// </summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        public void ValidateNeedHelpHeader()
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.NeedHelpHeader, 5);

                log.Info("Verifies the need help header.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to Verify the need help header. {e.Message}");
                Assert.Fail($"Unable to Verify the need help header. {e.Message}");
            }
        }

        /// <summary>
        /// Verifies the Need Hep Submit Button
        /// </summary>
        /// Author: Jacob Putnam
        /// Date: 01/13/2023
        public void ValidateNeedHelpSubmitButton()
        {
            try
            {
                eActions.WaitForElementToBeDisplayed(LoginPageConsts.NeedHelpSubmit, 5);

                log.Info("Verifies the need help Submit button.");
            }
            catch (Exception e)
            {
                log.Error($"Unable to Verify the need help Submit button. {e.Message}");
                Assert.Fail($"Unable to Verify the need help Submit button. {e.Message}");
            }
        }
    }
}
