using OpenQA.Selenium;

namespace Hudl_TestAutomation.PageObjects.LoginPage
{
    public class LoginPageConsts
    {
        public static By UserNameTextBox = By.Id("email");
        public static By PasswordTextbox = By.Id("password");
        public static By SubmitButton = By.XPath("//button[@id='logIn']");
        public static By ErorrText = By.XPath("//div[@id='app']/section/div[2]/div/form/div/div[3]/div/p");
        public static By MyAccountMenu = By.Id("ssr-webnav");
        public static By LoginWithOrgButton = By.XPath("//button[@type='button']");
        public static By LoginWithOrgTextBox = By.Id("uniId_1");
        public static By LoginWithOrgSubmit = By.XPath("//button[@type='submit']");
        public static By LoginWithOrgErrorText = By.XPath("//div[@id='app']/section/div[2]/div/form/div/div[3]/div/p");
        public static By NeedHelpButton = By.LinkText("Need help?");
        public static By NeedHelpHeader = By.XPath("//div[@id='app']/section/div[2]/div/form/div/h2");
        public static By NeedHelpSubmit = By.XPath("//button[@type='submit']");
        public static By BackButton = By.XPath("xpath=//div[@id='app']/section/a/span");

    }
}
