using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public class EditContactInfoPage : PageObjectBase
    {
        public EditContactInfoPage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public void FillForm(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            this.FirstNameInput.Clear();
            this.FirstNameInput.SendKeys(firstName);
            Thread.Sleep(_delay);
            
            this.LastNameInput.Clear();
            this.LastNameInput.SendKeys(lastName);
            Thread.Sleep(_delay);

            this.EmailAddressInput.Clear();
            this.EmailAddressInput.SendKeys(emailAddress);
            Thread.Sleep(_delay);

            this.PhoneNumberInput.Clear();
            this.PhoneNumberInput.SendKeys(phoneNumber);
            Thread.Sleep(_delay);
        }

        public void SubmitForm()
        {
            this.SubmitButton.Click();
            Thread.Sleep(_delay);
        }

        public IWebElement BackToProfileLink
        {
            get
            {
                return _browser.FindElement(By.PartialLinkText("Back to profile"));
            }
        }

        public IWebElement FirstNameInput
        {
            get
            {
                return _browser.FindElement(By.Id("FirstName"));
            }
        }

        public IWebElement LastNameInput
        {
            get
            {
                return _browser.FindElement(By.Id("LastName"));
            }
        }

        public IWebElement EmailAddressInput
        {
            get
            {
                return _browser.FindElement(By.Id("EmailAddress"));
            }
        }

        public IWebElement PhoneNumberInput
        {
            get
            {
                return _browser.FindElement(By.Id("PhoneNumber"));
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
