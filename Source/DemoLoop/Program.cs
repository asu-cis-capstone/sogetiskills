using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SogetiSkills.UI.Tests.Integration.Scenarios;
using WebMatrix.Data;

namespace DemoLoop
{
    public class Program
    {
        private static int DELAY = int.Parse(ConfigurationManager.AppSettings["Delay"]);
        private static string ROOT_URL = ConfigurationManager.AppSettings["RootUrl"];

        [STAThread]
        public static void Main(string[] args)
        {
            IWebDriver browser = null;
            try
            {
                using (browser = new ChromeDriver())
                {
                    while (true)
                    {
                        CleanUpDatabase();

                        browser.Manage().Window.Maximize();
                        var consultantEndToEnd = new ConsultantEndToEnd(ROOT_URL, browser, DELAY);
                        consultantEndToEnd.Execute();

                        var accountExecutiveEndToEnd = new AccountExecutiveEndToEnd(ROOT_URL, browser, DELAY);
                        accountExecutiveEndToEnd.Execute();                        
                    }
                }
            }
            catch { }
            finally
            {
                if (browser != null)
                {
                    browser.Dispose();
                }
            }
        }

        private static void CleanUpDatabase()
        {
            var db = Database.Open("SogetiSkills");
            db.Execute("UPDATE Skills SET IsCanonical = 0 WHERE IsCanonical = 1");

            if ((int)db.QueryValue("SELECT COUNT(*) FROM Users") > 20)
            {
                db.Execute("DELETE ConsultantSkill");
                db.Execute("DELETE Skills");
                db.Execute("DELETE Resumes");
                db.Execute("DELETE Users");
            }
        }
    }
}
