using Gauge.CSharp.Lib.Attribute;
using TestKit.Web.Implementation.Pages;

namespace TestKit.Web.Implementation.Actions
{
    public class DashboardSpec
    {
        private readonly DashboardPage _dashboardPage = new DashboardPage();
        [Step("Go Template Page")]
        public void GoTemplatePage()
        {
            _dashboardPage.GoTemplatePage();
        }
    }
}
