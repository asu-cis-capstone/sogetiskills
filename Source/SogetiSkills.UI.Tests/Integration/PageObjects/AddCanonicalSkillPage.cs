using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SogetiSkills.UI.Tests.TestHelpers;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public class AddCanonicalSkillPage : PageObjectBase
    {
        public AddCanonicalSkillPage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public void FillForm(string newSkillName)
        {
            SkillNameInput.ClearSlowy(_delay);
            SkillNameInput.SendKeysSlowly(_delay, newSkillName);
        }

        public void SubmitForm()
        {
            SubmitButton.Click();
        }

        public IWebElement SkillNameInput
        {
            get
            {
                return _browser.FindElement(By.Id("Name"));
            }
        }

        public IWebElement SubmitButton
        {
            get
            {
                return _browser.FindElement(By.CssSelector("input[type=submit]"));
            }
        }
    }
}
