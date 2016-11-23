using System;
using OpenQA.Selenium.Support.PageObjects;
using TestKit.Web.Implementation.Utility;
using OpenQA.Selenium;
using System.Reflection;

namespace TestKit.Web.Implementation.Pages
{
    public abstract class BasePage
    {
        //private IWebDriver _driver;
        protected static readonly string BaseUrl = Environment.GetEnvironmentVariable("BaseUrl");
        protected static readonly string BaselinePath = Environment.GetEnvironmentVariable("BaselinePath");    
        //public BasePage()
        //{
        //    _driver = DriverSetUp.Driver;
        //    PageFactory.InitElements(_driver, this);
        //}

        //public IWebElement GetElement(string name)
        //{
        //    var field = GetType().GetField(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
        //    return field == null ? null : field.GetValue(this) as IWebElement;
        //}       
    }
}
