using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using SogetiSkills.UI.Tests.Integration.Scenarios;

namespace DemoLoop
{
    public class Program
    {
        private static int DELAY = int.Parse(ConfigurationManager.AppSettings["Delay"]);
        private static string ROOT_URL = ConfigurationManager.AppSettings["RootUrl"];

        [STAThread]
        public static void Main(string[] args)
        {
            using (var browser = new ChromeDriver())
            {
                browser.Manage().Window.Maximize();
                for (int i = 0; i < 5; i++)
                {
                    var consultantEndToEnd = new ConsultantEndToEnd(ROOT_URL, browser, DELAY);
                    consultantEndToEnd.Execute();
                }
            }
        }
    }
}
