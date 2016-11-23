using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestKit.Web.Implementation.Utility
{
    public static class DriverExtensions
    {
        public static void EnterText(IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        public static void Visit(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists(By.TagName("body")));
        }

        public static bool isAlertPresent(this IWebDriver driver)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isOnSpecialPage(this IWebDriver driver, string PartialUrl)
        {
            driver.Wait(2500).ForPage().ReadyStateComplete();
            try
            {
                driver.Wait(2500).ForPage().UrlToContain(PartialUrl);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDisplayed(this IWebElement element)
        {
            bool result;
            try
            {
                result = element.Displayed;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public static bool WaitForDisplay(IWebElement element, int timeoutInSeconds = 10)
        {
            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Func<IWebElement, bool> waiter = new Func<IWebElement, bool>((IWebElement ele) =>
            {
                bool result;
                try
                {
                    result = element.Displayed;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            });
            return wait.Until(waiter);
        }
        public static bool WaitForDisappear(IWebElement element, int timeoutInSeconds = 10)
        {
            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Func<IWebElement, bool> waiter = new Func<IWebElement, bool>((IWebElement ele) =>
            {
                bool result;
                try
                {
                    result = !element.Displayed;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            });
            return wait.Until(waiter);
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds=10)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement fmb = wait.Until(ExpectedConditions.ElementExists(by));
                return fmb;
            }
            return driver.FindElement(by);
        }

    }
}
