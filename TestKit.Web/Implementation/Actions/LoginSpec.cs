using Gauge.CSharp.Lib;
using Gauge.CSharp.Lib.Attribute;
using TestKit.Web.Implementation.Pages;
using TestKit.Web.Implementation.Utility;

namespace TestKit.Web.Implementation.Actions
{
    public class LoginSpec
    {
        private readonly LoginPage _loginPage = new LoginPage();       
        [Step("Navigate To Home Page")]
        public void NavigateToHomepage()
        {
            _loginPage.NavigateToHomepage();
        }

        [Step("Login To Dashboard Page with <username> and <password>")]
        public void Login(string username, string password)
        {
            _loginPage.LoginFMWeb(username, password);
        }
    }
}
