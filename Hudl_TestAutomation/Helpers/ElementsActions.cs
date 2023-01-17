using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Hudl_TestAutomation.Helpers
{
    /// <summary>
    /// Class used for performing web page actions
    /// </summary>
    public class ElementsActions
    {
        private static HudlLogger _log = new HudlLogger("ElementActions");
        private IWebDriver _driver;

        public ElementsActions(IWebDriver driver)
        {
            _driver = driver;
        }

        public static IWebElement ElementIfVisible(IWebElement element)
        {
            if (!element.Displayed)
            {
                return null;
            }
            return element;
        }

        public static Func<IWebDriver, IWebElement>ElementIsVisible(By locator)
        {
            return delegate (IWebDriver driver)
            {
                try
                {
                    return ElementIfVisible(driver.FindElement(locator));
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public void WaitForElementToBeDisplayed(By pageElement, int timeoutInSeconds)
        {
            try
            {
                Thread.Sleep(1500);
                new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(ElementIsVisible(pageElement));

            }
            catch (Exception ex)
            {
                _log.Error(string.Concat("WebDriverWait failed to wait for element to be displayed with locator: '", pageElement, "' after ", timeoutInSeconds, "seconds", ex.Message));
                throw;
            }
        }

        public string GetText(By pageElement)
        {
            try
            {
                return _driver.FindElement(pageElement).Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
                /// <b>Description: </b> Retrieves the title from the current page.
                /// </summary>
                /// <returns>Page title as string.</returns>
        public string GetPageTitle()
        {
            var title = string.Empty;
            try
            {
                title = _driver.Title;
            }
            catch (Exception)
            {
                throw;
            }
            return title;
        }


        /// <summary>
                /// <b>Description: </b> Clear the text from a page element.
                /// </summary>
                /// <param name="pageElement"></param>
        public void Clear(By pageElement)
        {
            try
            {
                _driver.FindElement(pageElement).Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
                /// <b>Description: </b> Checks if an element is displayed or not. 
                /// </summary>
                /// <param name="pageElement"></param>
                /// <returns>TRUE if element is displayed on the page. FALSE if element is not displayed on the page. 
                /// NoSuchElementException if the element is not present in the DOM.</returns>
        public bool IsElementDisplayed(By pageElement)
        {
            var isDisplayed = false;
            try
            {
                isDisplayed = _driver.FindElement(pageElement).Displayed;
            }
            catch (Exception)
            {
                throw;
            }
            return isDisplayed;
        }

        /// <summary>
                /// <b>Description: </b> Click a page element.
                /// </summary>
                /// <param name="element"></param>
                /// <exception cref="WebDriverTimeoutException">Gets thrown if the click action takes longer than 30 seconds to finish.</exception>
        public void Click(By pageElement)
        {
            try
            {
                _driver.FindElement(pageElement).Click();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("timed out"))
                {
                    throw new WebDriverTimeoutException();

                }

                else
                {
                    throw;
                }

            }
        }

        /// <summary>
                /// <b>Description: </b> Enters text into a text element.
                /// </summary>
                /// <param name="element"></param>
                /// <param name="text"></param>
        public void Type(By pageElement, string text)
        {
            try
            {
                _driver.FindElement(pageElement).SendKeys(text);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
