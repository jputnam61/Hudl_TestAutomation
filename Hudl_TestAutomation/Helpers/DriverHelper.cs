using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Configuration;
using System.Threading;

namespace Hudl_TestAutomation.Helpers
{
    /// <summary>
    /// A Class used to assist in getting a webDriver 
    /// </summary>
    public class DriverHelper
    {
        int retryCounter = 0;
        private readonly HudlLogger _log = new HudlLogger("DriverHelper");


        /// <summary>
        /// Gets the Selenium WebDriver Instanced based on the value in the app.config
        /// </summary>
        /// <returns></returns>
        public IWebDriver InitWebDriver()
        {
            try
            {
                var browserType = ConfigurationManager.AppSettings["Browser"].ToUpper();
                switch (browserType)
                {
                    case "C":
                        var cOptions = new ChromeOptions();
                        cOptions.AddArgument("--ignore-certificate-errors");
                        cOptions.AddArgument("--start-maximized");
                        cOptions.AddUserProfilePreference("download.prompt_for_download", false);
                        cOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                        cOptions.AddUserProfilePreference("plugins.plugins_disabled", "Chrome PDF Viewer");
                        _log.Debug("Attempting to create ChromeDriver.exe.");
                        return new ChromeDriver(ChromeDriverService.CreateDefaultService(), cOptions, TimeSpan.FromMinutes(3));
                    case "CH":
                        _log.Debug("Attempting to create headless ChromeDriver.exe.");
                        var chOptions = new ChromeOptions();
                        chOptions.AddArgument("--ignore-certificate-errors");
                        chOptions.AddArgument("--window-size=1920,1080"); // explicitly setting the window size as it seems that a headless window cant be maximized
                        chOptions.AddArgument("--headless");
                        chOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                        return new ChromeDriver(ChromeDriverService.CreateDefaultService(), chOptions, TimeSpan.FromMinutes(3));
                    case "F":
                        _log.Debug("Attempting to create GeckoDriver.exe.");
                        var fOptions = new FirefoxOptions();
                        fOptions.AcceptInsecureCertificates = true;
                        fOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                        var fProfile = new FirefoxProfile();
                        fOptions.Profile = fProfile;
                        return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), fOptions, TimeSpan.FromMinutes(3));
                    case "FH":
                        _log.Debug("Attempting to create headless GeckoDriver.exe.");
                        var fhOptions = new FirefoxOptions();
                        fhOptions.AddArgument("--headless");
                        fhOptions.AcceptInsecureCertificates = true;
                        fhOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                        var fhProfile = new FirefoxProfile();
                        fhOptions.Profile = fhProfile;
                        return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), fhOptions, TimeSpan.FromMinutes(3));

                    case "E":
                        _log.Debug("Attempting to create MicrosoftWebDriver.exe.");
                        var eOptions = new EdgeOptions();
                        eOptions.AcceptInsecureCertificates = true;
                        return new EdgeDriver(eOptions);

                    case "EH":
                        _log.Debug("Attempting to create headless MicrosoftWebDriver.exe.");
                        var ehOptions = new EdgeOptions();
                        ehOptions.AddArgument("--headless");
                        ehOptions.AcceptInsecureCertificates = true;
                        return new EdgeDriver(ehOptions);
                    default:
                        throw new Exception($"App.config browser value '{browserType}' does not match any available driver options.");
                }

            }
            catch (Exception ex)
            {
                // adding a retry to try and initialize webdriver if an exception occurs
                if (retryCounter < 5)
                {
                    // waiting 1 second before retrying
                    Thread.Sleep(1000);
                    retryCounter++;
                    _log.Error($"Failed to initialize WebDriver during setup. Retry: {retryCounter} {ex.Message}");
                    InitWebDriver();
                }
                else
                {
                    _log.Error($"Failed to initialize WebDriver during setup after {retryCounter} retries. {ex.Message}");
                    throw new Exception($"Failed to initialize WebDriver during setup after {retryCounter} retries. {ex.Message}");
                }

            }
            return null;
        }
    }
}
