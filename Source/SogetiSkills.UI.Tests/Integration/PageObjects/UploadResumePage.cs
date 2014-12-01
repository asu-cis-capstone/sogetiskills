using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SogetiSkills.UI.Tests.Integration.PageObjects
{
    public class UploadResumePage : PageObjectBase
    {
        public UploadResumePage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public void FillForm(string resumeFilePath)
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new InvalidOperationException("UploadResumePage.FillForm requires a single thread apartment.");
            }

            Clipboard.SetText(resumeFilePath);
            FileInput.Click();
            Thread.Sleep(1000);
            SendKeys.SendWait("^v");
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(1000);
        }

        public void SumitForm()
        {
            SubmitButton.Click();
        }

        public IWebElement BackToProfileLink
        {
            get
            {
                return _browser.FindElement(By.PartialLinkText("Back to profile"));
            }
        }

        public IWebElement FileInput
        {
            get
            {
                return _browser.FindElement(By.CssSelector("input[type=file]"));
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
