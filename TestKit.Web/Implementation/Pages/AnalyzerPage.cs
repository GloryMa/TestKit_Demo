using FluentAssertions;
using Gauge.CSharp.Lib;
using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Selenium.WebDriver.WaitExtensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TestKit.Web.Implementation.Utility;

namespace TestKit.Web.Implementation.Pages
{
    public class AnalyzerPage
    {
        //public static readonly string HomeUrl = BaseUrl;
        //public static readonly string ExpectPath = BaselinePath;
        private IWebDriver _driver;
        //private string _HomeUrl;
        //private string _ExpectPath;

        //public AnalyzerPage()
        //{
        //    _driver = DriverSetUp.Driver;
        //    _HomeUrl = InitData.suiteStore.Get<string>("BaseUrl");
        //    _ExpectPath= InitData.suiteStore.Get<string>("BaselinePath");
        //}
        //[FindsBy(How = How.Id, Using = "btnExportData")]
        //private IWebElement ExportButton { get; set; }

        //[FindsBy(How = How.ClassName, Using = "spinner")]
        //private IWebElement Spinner { get; set; }

        //[FindsBy(How = How.ClassName, Using = "messageContainer")]
        //private IWebElement MessageContainer { get; set; }

        //[FindsBy(How = How.XPath, Using = "/html/body/div[3]/div[2]/form/div[3]/input")]
        //private IWebElement DialogButton { get; set; }

        //[FindsBy(How = How.Id, Using = "filters_olapClient")]
        //private IWebElement FilterBar { get; set; }

        

        public void ExportCsv()
        {
            _driver = DriverSetUp.Driver;
            _driver.FindElement(By.Id("btnExportData")).Click();
            //ExportButton.Wait(2500).ForElement().ToBeVisible();
            //ExportButton.Click();                  
        }

        public void SaveOrCompareCsvs()
        {

        }

        public bool CheckPageReady()
        {
            try
            {
                _driver = DriverSetUp.Driver;
                var FilterBar= _driver.FindElement(By.Id("filters_olapClient"));
                FilterBar.Wait(300000).ForElement().ToBeVisible();
                return true;
            }
            catch
            {
                return false;
            }
        }


        //public bool CheckPageReady()
        //{
        //    bool ready = false;
        //    bool ready_spinner = false;
        //    bool ready_message = false;
        //    if (DriverExtensions.WaitForDisplay(Spinner))
        //    {
        //        ready_spinner = DriverExtensions.WaitForDisappear(Spinner, 120);               
        //    }
        //    if (DriverExtensions.WaitForDisplay(MessageContainer))
        //    {
        //        if(DialogButton.IsDisplayed())
        //        {
        //            string aa = DialogButton.Text.ToLower();
        //            if (DialogButton.Text.ToLower() == "ok")
        //            {
        //                DialogButton.Click();
        //                ready_message = true;
        //            }
        //            else
        //            {
        //                DialogButton.Click();
        //                DriverFactory.Driver.SwitchTo().Alert().Accept();
        //                ready_message = false;
        //            }
        //        }
        //        else
        //        { }


        //    }
        //    return ready = ready_spinner && ready_message;
        //}

        //public bool CheckPageReady()
        //{
        //    bool ready = false;
        //    bool ready_spinner = false;
        //    bool ready_message = false;
        //    try
        //    {          
        //        Spinner.Wait(10000).ForElement().ToBeVisible();              
        //        ready_spinner = false;
        //    }
        //    catch (Exception e)
        //    {
        //        try
        //        {                 
        //            Spinner.Wait(60000).ForElement().ToBeInvisible();
        //            ready_spinner = true;
        //        }
        //        catch
        //        {
        //            ready_spinner = false;
        //        }
        //    }


        //    //try
        //    //{  

        //    //    //Driver.Wait(2500).ForElement(By.Id("elementToBeRemoved")).ToNotExist();
        //    //    //Spinner.Wait(10000).ForElement().ToBeVisible();
        //    //    Spinner.Wait(60000).ForElement().ToBeInvisible();
        //    //    ready_spinner = true;
        //    //}
        //    //catch(Exception e)
        //    //{
        //    //    ready_spinner = false;
        //    //}

        //    try
        //    {
        //        MessageContainer.Wait(1000).ForElement().ToBeVisible();
        //        DialogButton.Click();
        //    }
        //    catch
        //    {
        //        ready_message = true;
        //    }
        //    return ready = ready_spinner && ready_message;
        //}

        public void SaveOrComparePics(string imageName)
        {
            _driver = DriverSetUp.Driver;
            string _ExpectPath = InitData.suiteStore.Get<string>("BaselinePath");
            string expectPath = _ExpectPath;
            string TempFolder = expectPath + "\\Temp";
            if (!Directory.Exists(TempFolder))
            {
                GaugeMessages.WriteMessage("Create--"+TempFolder);
                Directory.CreateDirectory(TempFolder);
            }
            string expectImage = Path.Combine(expectPath, imageName);
            var screenshot = _driver as ITakesScreenshot;
            Screenshot shot = screenshot.GetScreenshot();
            if (!File.Exists(expectImage))
            {
                shot.SaveAsFile(expectImage, ImageFormat.Png);
            }
            else
            {                            
                string actualImage = TempFolder + "\\actual_" + imageName;
                shot.SaveAsFile(actualImage, ImageFormat.Png);
                using (MagickImage mi_actual = new MagickImage(actualImage))
                using (MagickImage mi_expect = new MagickImage(expectImage))
                using (MagickImage mi_differ = new MagickImage())
                {
                    double distortion = mi_actual.Compare(mi_expect, ErrorMetric.Absolute, mi_differ);
                    if (distortion != 0)
                    {
                        ScreenGrabber.IsCompare = true;
                        string differImage = TempFolder + "\\differ_" + imageName;
                        mi_differ.Write(differImage);
                        int gifHeight = mi_expect.Height;
                        int gifWidth = mi_expect.Width;
                        int[] resolution = new int[] { gifWidth, gifHeight };
                        List<string> imageList = new List<string>();
                        Dictionary<string, string> imageDic = new Dictionary<string, string>();
                        imageDic.Add(expectImage, "Expect");
                        imageDic.Add(actualImage, "Actual");
                        imageDic.Add(differImage, "Differ");
                        string differGif = MagickUtility.ConvertImagesToGif(imageDic, resolution);
                        Image gifImage = Image.FromFile(differGif);
                        ScreenGrabber.Differ = MagickUtility.ImageToByteArray(gifImage);
                        distortion.Should().Be(0);
                    }                    
                }
            }
        }

        public void BackToTemplatePage()
        {
            string _HomeUrl = InitData.suiteStore.Get<string>("BaseUrl");
            _driver = DriverSetUp.Driver;
            _driver.Visit(_HomeUrl + "/OLAP/Common/Templates.htm");            
        }
    }
}
