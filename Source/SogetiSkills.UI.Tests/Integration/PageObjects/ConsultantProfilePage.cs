using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public class ConsultantProfilePage : PageObjectBase
    {
        public ConsultantProfilePage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public void Navigate(int consultantId)
        {
            string profileUrl = string.Format("{0}/profile/{1}", _rootUrl, consultantId).ToLower();
            if (_browser.Url.ToLower() != profileUrl)
            {
                _browser.Url = profileUrl;
                _browser.Navigate();
            }
        }

        public int ConsultantId
        {
            get
            {
                string url = _browser.Url.ToLower();
                int indexOfLastSlash = url.LastIndexOf('/');
                string consultantId = url.Substring(indexOfLastSlash + 1);
                return int.Parse(consultantId);
            }
        }

        public IWebElement OnTheBeachSpan
        {
            get
            {
                return _browser.FindElement(By.CssSelector("span[data-bind~=setBeachStatusOff]"));
            }
        }

        public IWebElement NotOnTheBeachSpan
        {
            get
            {
                return _browser.FindElement(By.CssSelector("span[data-bind~=setBeachStatusOn]"));
            }
        }

        public void ToggleBeachStatus()
        {
            if (OnTheBeachSpan.Displayed)
            {
                OnTheBeachSpan.Click();
            }
            else
            {
                NotOnTheBeachSpan.Click();
            }
            Thread.Sleep(Math.Max(_delay, 500));
        }

        public IWebElement EditContactInfoLink
        {
            get
            {
                return _browser.FindElement(By.PartialLinkText("Edit contact info"));
            }
        }

        public IWebElement EditSkillsLink
        {
            get
            {
                var link = _browser.FindElements(By.PartialLinkText("Add skills")).FirstOrDefault();
                if (link == null)
                {
                    _browser.FindElements(By.PartialLinkText("Edit skills")).FirstOrDefault();
                }
                return link;
            }
        }

        public IWebElement UploadResumeLink
        {
            get
            {
                return _browser.FindElement(By.PartialLinkText("Upload a resume"));
            }
        }
    }
}
