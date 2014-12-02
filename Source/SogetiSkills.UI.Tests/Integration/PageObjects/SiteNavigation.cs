using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public class SiteNavigation
    {
        private readonly IWebDriver _browser;

        public SiteNavigation(IWebDriver browser)
        {
            _browser = browser;
        }

        public IWebElement SignOutLink
        {
            get
            {
                return _browser.FindElement(By.LinkText("Sign out"));
            }
        }

        public IWebElement SignInLink
        {
            get
            {
                return _browser.FindElement(By.LinkText("Sign in"));
            }
        }

        public IWebElement YourProfileLink
        {
            get
            {
                return _browser.FindElement(By.LinkText("Your profile"));
            }
        }

        public IWebElement FindConsultantsLink
        {
            get
            {
                return _browser.FindElement(By.LinkText("Find consultants"));
            }
        }

        public IWebElement ManageCanonicalSkillsLink
        {
            get
            {
                return _browser.FindElement(By.LinkText("Manage canonical skills"));
            }
        }
    }
}
