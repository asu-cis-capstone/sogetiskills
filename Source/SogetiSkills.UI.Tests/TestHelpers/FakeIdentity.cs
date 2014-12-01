using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SogetiSkills.UI.Tests.TestHelpers
{
    public class FakeIdentity
    {
        public static FakeIdentity Generate()
        {
            FakeIdentity result = new FakeIdentity();

            WebClient downloader = new WebClient();
            downloader.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20100101 Firefox/15.0.1";
            string html = downloader.DownloadString("http://www.fakenamegenerator.com/gen-random-us-us.php");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var content = (from x in doc.DocumentNode.Descendants("div")
                           where HasClass(x, "content") && x.ChildNodes.Count(y => y.Name == "div" && (HasClass(y, "address") || HasClass(y, "extra"))) == 2
                           select x).Single();
            var addressDiv = content.ChildNodes.First(x => HasClass(x, "address"));
            string fullName = addressDiv.Element("h3").InnerText;
            result.FirstName = Split(fullName, " ").First();
            result.LastName = Split(fullName, " ").Last();

            string fullAddress = addressDiv.Element("div").InnerHtml.Trim();
            result.StreetAddress = Split(fullAddress, "<br>")[0];
            string cityStateZip = Split(fullAddress, "<br>")[1];
            result.Zip = Regex.Match(cityStateZip, @"\d{5}").Value;
            result.State = Regex.Match(cityStateZip, @"[A-Z]{2}").Value;
            result.City = cityStateZip.Replace(", " + result.State + " " + result.Zip, string.Empty).Trim();

            var extra = content.Descendants("ul").Single();
            var items = extra.Elements("li");
            List<Tuple<HtmlNode, HtmlNode>> pairs = new List<Tuple<HtmlNode, HtmlNode>>();
            for (int i = 0; i < items.Count() / 2; i += 2)
            {
                HtmlNode label = items.ElementAt(i);
                HtmlNode value = items.ElementAt(i + 1);
                pairs.Add(new Tuple<HtmlNode, HtmlNode>(label, value));
            }

            result.Phone = new string(FindPair(pairs, "Phone:").Item2.InnerText.Where(x => char.IsDigit(x)).ToArray());

            result.EmailAddress = FindPair(pairs, "Email Address:").Item2.Element("span").InnerText;

            result.Username = FindPair(pairs, "Username:").Item2.InnerText;
            result.Password = Guid.NewGuid().ToString("n").Substring(10) + "a1";

            string birthdayString = FindPair(pairs, "Birthday:").Item2.InnerText;
            birthdayString = birthdayString.Substring(0, birthdayString.IndexOf("(") - 1);
            result.Birthday = DateTime.Parse(birthdayString);

            // Make sure we don't get a user under 18
            if (result.Birthday.Year > (DateTime.Now.Year - 19))
            {
                result.Birthday = new DateTime(DateTime.Now.Year - 19, result.Birthday.Month, result.Birthday.Day);
            }

            result.SSN = new string(Regex.Match(FindPair(pairs, "SSN:").Item2.InnerText, @"\d{3}-\d{2}-\d{4}").Value.Where(x => char.IsDigit(x)).ToArray());
            if (string.IsNullOrWhiteSpace(result.SSN))
            {
                result.SSN = new string(DateTime.Now.Ticks.ToString().Reverse().Take(9).ToArray());
            }

            result.DriversLicenseNumber = "D" + new string(DateTime.Now.Ticks.ToString().Reverse().Take(8).ToArray());

            result.DriversLicenseExpiration = result.Birthday.AddYears(50);
            while (result.DriversLicenseExpiration < DateTime.Now.AddYears(15))
            {
                result.DriversLicenseExpiration = result.DriversLicenseExpiration.AddYears(15);
            }

            return result;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string SSN { get; set; }
        public string DriversLicenseNumber { get; set; }
        public DateTime DriversLicenseExpiration { get; set; }

        #region Extension methods
        private static bool HasClass(HtmlNode node, string cssClass)
        {
            var attribute = node.Attributes["class"];
            if (attribute != null)
            {
                string[] values = attribute.Value.Split(' ');
                return values.Contains(cssClass);
            }
            return false;
        }

        private static string[] Split(string input, string token)
        {
            return input.Split(new[] { token }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static Tuple<HtmlNode, HtmlNode> FindPair(IEnumerable<Tuple<HtmlNode, HtmlNode>> pairs, string label)
        {
            return pairs.FirstOrDefault(x => x.Item1.InnerText == label);
        }
        #endregion
    }
}
