using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SogetiSkills.Core.Models;
using SogetiSkills.UI.Tests.Integration.PageObjects;
using SogetiSkills.UI.Tests.TestHelpers;

namespace SogetiSkills.UI.Tests.Integration.Scenarios
{
    public class ConsultantEndToEnd
    {
        private readonly IWebDriver _browser;
        private readonly string _rootUrl;
        private readonly int _delay;

        public ConsultantEndToEnd(string rootUrl, IWebDriver browser, int delay)
        {
            _rootUrl = rootUrl;
            _browser = browser;
            _delay = delay;
        }

        public void Execute()
        {
            SignUpForANewAccount();
            ChangeContactInfo();
            AddAndRemoveSkills();
            UploadResume();
            ToggleBeachStatus();
            SignOut();
        }

        private void SignUpForANewAccount()
        {
            var fakeIdentity = FakeIdentity.Generate();

            var registrationPage = new RegistrationPage(_rootUrl, _browser, _delay);
            registrationPage.Navigate();
            
            registrationPage.FillForm(
                fakeIdentity.EmailAddress,
                fakeIdentity.Password,
                AccountTypes.CONSULTANT,
                fakeIdentity.FirstName,
                fakeIdentity.LastName,
                fakeIdentity.Phone);
            
            registrationPage.SubmitForm();
        }

        private void ChangeContactInfo()
        {
            var consultantProfilePage = new ConsultantProfilePage(_rootUrl, _browser, _delay);            
            consultantProfilePage.EditContactInfoLink.Click();

            var fakeIdentity = FakeIdentity.Generate();
            var editContactInfoPage = new EditContactInfoPage(_rootUrl, _browser, _delay);
            editContactInfoPage.FillForm(
                fakeIdentity.FirstName,
                fakeIdentity.LastName,
                fakeIdentity.EmailAddress,
                fakeIdentity.Phone);
            editContactInfoPage.SubmitForm();
        }

        private void AddAndRemoveSkills()
        {
            var consultantProfilePage = new ConsultantProfilePage(_rootUrl, _browser, _delay);
            consultantProfilePage.EditSkillsLink.Click();

            var editSkillsPage = new EditSkillsPage(_rootUrl, _browser, _delay);
            int numberOfSkills = SampleData.RandomNumber(1, 10);
            foreach(string skillName in SampleData.RandomSkillNames(numberOfSkills))
            {
                int proficiencyLevel = SampleData.RandomNumber(1, 5);
                editSkillsPage.AddNewSkill(skillName, proficiencyLevel);
            }
            editSkillsPage.BackToProfileLink.Click();
        }

        private void UploadResume()
        {
            var consultantProfilePage = new ConsultantProfilePage(_rootUrl, _browser, _delay);
            consultantProfilePage.UploadResumeLink.Click();

            var uploadResumePage = new UploadResumePage(_rootUrl, _browser, _delay);
            string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string sampleResumeFilePath = Path.Combine(workingDirectory, "Resume.pdf");
            uploadResumePage.FillForm(sampleResumeFilePath);
            uploadResumePage.SumitForm();
        }

        private void ToggleBeachStatus()
        {
            var consultantProfilePage = new ConsultantProfilePage(_rootUrl, _browser, _delay);            

            int numberOfToggles = SampleData.RandomNumber(1, 10);
            for (int i = 0; i < numberOfToggles; i++)
            {
                consultantProfilePage.ToggleBeachStatus();
            }
        }

        private void SignOut()
        {
            var consultantProfilePage = new ConsultantProfilePage(_rootUrl, _browser, _delay);
            consultantProfilePage.SignOutLink.Click();
        }
    }
}
