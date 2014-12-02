using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SogetiSkills.UI.Tests.TestHelpers;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public class ManageCanonicalSkillsPage : PageObjectBase
    {
        public ManageCanonicalSkillsPage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public IWebElement AddCanonicalSkillLink
        {
            get
            {
                return _browser.FindElement(By.PartialLinkText("Add canonical skill"));
            }
        }
    }
}
