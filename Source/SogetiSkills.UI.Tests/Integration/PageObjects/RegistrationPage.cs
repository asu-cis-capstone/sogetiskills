using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public class RegistrationPage : PageObjectBase
    {
        public RegistrationPage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public void Navigate()
        {
            _browser.Url = _rootUrl + "account/register";
            _browser.Navigate();
        }

        public void FillForm(string emailAddress, string password, string accountType, string firstName, string lastName, string phoneNumber)
        {
            this.EmailAddressInput.SendKeys(emailAddress);
            Thread.Sleep(_delay);
            this.PasswordInput.SendKeys(password);
            Thread.Sleep(_delay);
            this.ConfirmPasswordInput.SendKeys(password);
            Thread.Sleep(_delay);
            this.AccountTypeDropDown.SelectByValue(accountType);
            Thread.Sleep(_delay);
            this.FirstNameInput.SendKeys(firstName);
            Thread.Sleep(_delay);
            this.LastNameInput.SendKeys(lastName);
            Thread.Sleep(_delay);
            this.PhoneNumberInput.SendKeys(phoneNumber);
            Thread.Sleep(_delay);
        }

        public void SubmitForm()
        {
            this.SubmitButton.Click();
            Thread.Sleep(_delay);
        }

        public IWebElement EmailAddressInput
        {
            get
            {
                return _browser.FindElement(By.Id("EmailAddress"));
            }
        }

        public IWebElement PasswordInput
        {
            get
            {
                return _browser.FindElement(By.Id("Password"));
            }
        }

        public IWebElement ConfirmPasswordInput
        {
            get
            {
                return _browser.FindElement(By.Id("ConfirmPassword"));
            }
        }

        public SelectElement AccountTypeDropDown
        {
            get
            {
                return new SelectElement(_browser.FindElement(By.Id("AccountType")));
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
