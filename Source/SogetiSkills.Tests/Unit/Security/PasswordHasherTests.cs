using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Security;
using SogetiSkills.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Tests.Unit.Security
{
    [TestClass]
    public class PasswordHasherTests : UnitTestBase
    {
        [TestMethod]
        public void Hash_SaltsAndHashesThePassword()
        {
            PasswordHasher subject = new PasswordHasher();

            string actualHash = subject.Hash("password", "saltsaltsalt");

            string expectedHash = "hGf9B6k8ZACCUUuypJNM0Sv13K8btMILBiLFqR2FhflyWyRIgIjs//7o1cYCDiaVJ4lzBNgelYcgAt+avVEgysoxJqwLjwVZmI8P9F/rcmD/N4TNK6J06IAapOedoS8O2KdC2lOm5ZTqwR6pcxP2HbCHSiVwtElYEwNTZyS6uiU=";
            Assert.AreEqual(expectedHash, actualHash);
        }
    }
}
