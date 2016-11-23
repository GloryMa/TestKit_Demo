using FluentAssertions;
using Gauge.CSharp.Lib.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestKit.Web.Implementation.Pages;

namespace TestKit.Web.Implementation.Actions
{
    public class AnalyzerSpec
    {
        private readonly AnalyzerPage _analyzerPage = new AnalyzerPage();
        //private readonly ScreenGrabber _screenGrabber = new ScreenGrabber();
        [Step("Export Result As Csv File")]
        public void ExportCsv()
        {
            _analyzerPage.ExportCsv();
        }
        [Step("Compare Result for <Template> in Case <CaseId>")]
        public void CompareImage(string Template, string CaseId)
        {            
            string imageName = "Case" + CaseId + "_" + Template + ".png";
            _analyzerPage.SaveOrComparePics(imageName);
        }
        [Step("Back To Template Page")]
        public void BackToTemplatePage()
        {
            _analyzerPage.BackToTemplatePage();
        }

        [Step("Check Analyzer Page Ready")]
        public void CheckAnalyzerPageReady()
        {
            _analyzerPage.CheckPageReady().Should().BeTrue();
        }
    }
}
