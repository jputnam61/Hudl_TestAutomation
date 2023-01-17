using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Hudl_TestAutomation.Helpers
{
    public class TestHelper : DriverHelper
    {
        private readonly HudlLogger _log;

        public TestHelper()
        {
            _log = new HudlLogger("TestHelper");
        }

        public void ExecuteTestActionsWithWebdriver(Action<IWebDriver> action)
        {
            IWebDriver webDriver = null;

            try
            {
                _log.Info($"Starting execution for test: {TestContext.CurrentContext.Test.MethodName}");
                webDriver = InitWebDriver();
                webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(180); // setting the default max timeout for a page load to 3 minutes
                webDriver.Manage().Window.Maximize();
                action(webDriver);
                _log.Info($"Completed execution for test: {TestContext.CurrentContext.Test.MethodName}");
            }

            catch (Exception e)
            {
                var name = TestContext.CurrentContext.Test.Name;

                // checking if the test has any failures associated with it or not. this is due to the fact that there are still some areas
                // where an exception gets thrown (not an Assert.Fail()) outside of scope of the automation project and the exception gets lost.
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    _log.Error($"ERROR in Test Execution: Test Name: {name} \n Error Msg: {e}");
                }
                else
                {
                    _log.Error($"ERROR in Test Execution: Test Name: {name} \n Error Msg: {e}");
                    Assert.Fail($"ERROR in Test Execution: Test Name: {name} \n Error Msg: {e}");
                }
            }
            finally
            {
                //Sleep is only for demonstration purposes
                //Thread.Sleep(5000);
                webDriver.Close();
                webDriver.Quit();
            }

        }
    }
}
