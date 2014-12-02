using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SogetiSkills.Core.Models;
using SogetiSkills.UI.Tests.Integration.PageObjects;
using SogetiSkills.UI.Tests.TestHelpers;

namespace SogetiSkills.UI.Tests.Integration.Scenarios
{
    public class AccountExecutiveEndToEnd
    {
        private readonly IWebDriver _browser;
        private readonly string _rootUrl;
        private readonly int _delay;

        public AccountExecutiveEndToEnd(string rootUrl, IWebDriver browser, int delay)
        {
            _rootUrl = rootUrl;
            _browser = browser;
            _delay = delay;
        }

        public void Execute()
        {
            SignUpForANewAccount();
            FindConsultants();
            AddCanonicalSkills();
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
                AccountTypes.ACCOUNT_EXECUTIVE,
                fakeIdentity.FirstName,
                fakeIdentity.LastName,
                fakeIdentity.Phone);

            registrationPage.SubmitForm();
            Thread.Sleep(_delay);
        }

        private void FindConsultants()
        {
            int numberOfSearches = SampleData.RandomNumber(1, 3);
            for (int i = 0; i < numberOfSearches; i++)
            {
                var findConsultantsPage = new FindConsultantsPage(_rootUrl, _browser, _delay);
                findConsultantsPage.SiteNavigation.FindConsultantsLink.Click();
                
                int numbrOfSkillsToSearchFor = SampleData.RandomNumber(1, 4);
                string skills = string.Join(", ", SampleData.RandomSkillNames(numbrOfSkillsToSearchFor));
                findConsultantsPage.FillForm(false, string.Empty, string.Empty, skills);
                findConsultantsPage.SubmitForm();
                Thread.Sleep(_delay * 3);

                var searchResults = findConsultantsPage.ViewProfileLinks;
                if (searchResults.Any())
                {
                    int randomSearchResultIndex = SampleData.RandomNumber(0, searchResults.Count() - 1);
                    searchResults.ElementAt(randomSearchResultIndex).Click();
                    Thread.Sleep(_delay * 3);
                }
            }
        }

        private void AddCanonicalSkills()
        {
            var profilePage = new ProfilePage(_rootUrl, _browser, _delay);
            profilePage.SiteNavigation.ManageCanonicalSkillsLink.Click();

            var manageCanonicalSkillsPage = new ManageCanonicalSkillsPage(_rootUrl, _browser, _delay);
            int numberOfSkills = SampleData.RandomNumber(1, 8);
            foreach(string skillName in SampleData.RandomSkillNames(numberOfSkills))
            {
                manageCanonicalSkillsPage.AddCanonicalSkillLink.Click();
                var addCanonicalSkillPage = new AddCanonicalSkillPage(_rootUrl, _browser, _delay);
                addCanonicalSkillPage.FillForm(skillName);
                addCanonicalSkillPage.SubmitForm();
                Thread.Sleep(_delay);
            }
        }

        private void SignOut()
        {
            var profilePage = new ProfilePage(_rootUrl, _browser, _delay);
            profilePage.SiteNavigation.SignOutLink.Click();
            Thread.Sleep(_delay);
        }
    }
}
