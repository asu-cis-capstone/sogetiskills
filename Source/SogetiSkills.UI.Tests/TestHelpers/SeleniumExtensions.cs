using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SogetiSkills.UI.Tests.TestHelpers
{
    public static class SeleniumExtensions
    {
        public static void SendKeysSlowly(this IWebElement element, int delay, string keys)
        {
            if (delay == 0)
            {
                element.SendKeys(keys);
            }
            else
            {
                foreach (char key in keys ?? string.Empty)
                {
                    element.SendKeys(key.ToString());
                    Thread.Sleep(SampleData.RandomNumber(0, delay / 10));
                }
            }
        }

        public static void ClearSlowy(this IWebElement element, int delay)
        {
            if (delay == 0)
            {
                element.Clear();
            }
            else
            {
                element.Click();
                string text = element.GetAttribute("value");
                for(int i = 0; i < text.Length; i++)
                {
                    element.SendKeys(Keys.Backspace);
                }
            }
        }
    }
}
