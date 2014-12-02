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
    public class EditSkillsPage : PageObjectBase
    {
        public EditSkillsPage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public void AddNewSkill(string skillName, int proficiencyLevel)
        {
            NewSkillNameInput.ClearSlowy(_delay);
            NewSkillNameInput.SendKeysSlowly(_delay, skillName);
            
            NewSkillProficiencyDropDown.SelectByValue(proficiencyLevel.ToString());
            Thread.Sleep(_delay);

            AddSkillButton.Click();
        }

        public IWebElement BackToProfileLink
        {
            get
            {
                return _browser.FindElement(By.PartialLinkText("Back to profile"));
            }
        }

        public IWebElement NewSkillNameInput
        {
            get
            {
                return _browser.FindElement(By.Id("newSkillText"));
            }
        }
        
        public SelectElement NewSkillProficiencyDropDown
        {
            get
            {
                return new SelectElement(_browser.FindElement(By.Id("newSkillProficiencyLevel")));
            }
        }

        public IWebElement AddSkillButton
        {
            get
            {
                return _browser.FindElement(By.CssSelector("button[data-bind~=addSkill]"));
            }
        }
    }
}
