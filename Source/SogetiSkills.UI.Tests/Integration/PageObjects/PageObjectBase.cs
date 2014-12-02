using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public abstract class PageObjectBase
    {
        protected readonly IWebDriver _browser;
        protected readonly string _rootUrl;
        protected readonly int _delay;

        public PageObjectBase(string rootUrl, IWebDriver browser, int delay)
        {
            _rootUrl = rootUrl;
            _browser = browser;
            _delay = delay;
        }

        public SiteNavigation SiteNavigation
        {
            get
            {
                return new SiteNavigation(_browser);
            }
        }
    }
}
