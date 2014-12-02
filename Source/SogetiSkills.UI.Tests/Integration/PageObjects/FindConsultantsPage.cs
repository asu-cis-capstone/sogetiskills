using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SogetiSkills.UI.Tests.TestHelpers;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public class FindConsultantsPage : PageObjectBase
    {
        public FindConsultantsPage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public void FillForm(bool beachOnly, string lastName, string emailAddress, string skills)
        {
            if (beachOnly)
            {
                BeachOnlyDropDown.SelectByText("Yes");
            }
            else
            {
                BeachOnlyDropDown.SelectByText("No");
            }
            Thread.Sleep(_delay);

            LastNameInput.ClearSlowy(_delay);
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                LastNameInput.SendKeysSlowly(_delay, lastName);
            }

            EmailAddressInput.ClearSlowy(_delay);
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                EmailAddressInput.SendKeysSlowly(_delay, emailAddress);                
            }

            SkillsInput.ClearSlowy(_delay);
            SkillsInput.SendKeysSlowly(_delay, skills);    
        }

        public void SubmitForm()
        {
            SubmitButton.Click();
        }

        public SelectElement BeachOnlyDropDown
        {
            get
            {
                return new SelectElement(_browser.FindElement(By.Id("beachStatus")));
            }
        }

        public IWebElement LastNameInput
        {
            get
            {
                return _browser.FindElement(By.Id("lastName"));
            }
        }

        public IWebElement EmailAddressInput
        {
            get
            {
                return _browser.FindElement(By.Id("emailAddress"));
            }
        }

        public IWebElement SkillsInput
        {
            get
            {
                return _browser.FindElement(By.Id("skills"));
            }
        }

        public IWebElement SubmitButton
        {
            get
            {
                return _browser.FindElement(By.CssSelector("button[type=submit]"));
            }
        }

        public IEnumerable<IWebElement> ViewProfileLinks
        {
            get
            {
                return _browser.FindElements(By.CssSelector(".btn.btn-default.center-block")).ToList();
            }
        }
    }
}
