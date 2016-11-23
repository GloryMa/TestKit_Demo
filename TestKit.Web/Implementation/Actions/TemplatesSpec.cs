using Gauge.CSharp.Lib.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestKit.Web.Implementation.Pages;

namespace TestKit.Web.Implementation.Actions
{
    public class TemplatesSpec
    {
        private readonly TemplatesPage _templatePage = new TemplatesPage();
        [Step("Run Template <templateName>")]
        public void RunTemplate(string templateName)
        {
            _templatePage.RunTemplate(templateName);
        }

        [Step("Logout From FMWeb")]
        public void Logout()
        {
            _templatePage.Logout();
        }
    }
}
