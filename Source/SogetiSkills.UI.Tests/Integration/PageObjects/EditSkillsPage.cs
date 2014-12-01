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
    public class EditSkillsPage : PageObjectBase
    {
        public EditSkillsPage(string rootUrl, IWebDriver browser, int delay)
            : base (rootUrl, browser, delay)
        {
            
        }

        public void AddNewSkill(string skillName, int proficiencyLevel)
        {
            NewSkillNameInput.Clear();
            NewSkillNameInput.SendKeys(skillName);
            Thread.Sleep(500);
            
            NewSkillProficiencyDropDown.SelectByValue(proficiencyLevel.ToString());
            Thread.Sleep(500);
            
            AddSkillButton.Click();
            Thread.Sleep(500);
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
