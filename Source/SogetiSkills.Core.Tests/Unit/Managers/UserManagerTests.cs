using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Core.Managers;
using SogetiSkills.Core.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using SogetiSkills.Core.Models;
using SogetiSkills.Core.Security;
using Moq;
using WebMatrix.Data;

namespace SogetiSkills.Core.Tests.Unit.Managers
{
    public class UserManagerTests : DbUnitTestBase
    {
        [TestClass]
        public class RegisterNewUser : UserManagerTests
        {
            [TestMethod]
            public async Task RegisterNewUser_ShouldInsertNewUser()
            {
                _fixture.Inject<ISaltGenerator>(new SaltGenerator());
                _fixture.Inject<IPasswordHasher>(new PasswordHasher());
                UserManager subject = _fixture.Create<UserManager>();

                await subject.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith", "1234567890");

                int count = (int)TestDatabase.QueryValue("SELECT COUNT(*) FROM Users WHERE EmailAddress = 'bill@site.com'");

                Assert.AreEqual(1, count);
            }

            [TestMethod]
            public async Task RegisterNewUser_SaltsAndHashesThePassword()
            {
                var fakePasswordHasher = new Mock<IPasswordHasher>();
                fakePasswordHasher.Setup(x => x.Hash(It.IsAny<string>(), It.IsAny<string>())).Returns("hashed password");
                _fixture.Inject(fakePasswordHasher);
                var fakeSaltGenerator = new Mock<ISaltGenerator>();
                fakeSaltGenerator.Setup(x => x.GenerateNewSalt()).Returns("salt");
                _fixture.Inject(fakeSaltGenerator);
                UserManager subject = _fixture.Create<UserManager>();

                await subject.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith", "1234567890");

                var user = TestDatabase.QuerySingle("SELECT TOP 1 * FROM Users WHERE EmailAddress = 'bill@site.com'");
                Assert.AreEqual("hashed password", user.Password_Hash);
                Assert.AreEqual("salt", user.Password_Salt);
            }
        }

        [TestClass]
        public class GetUserIdForEmailAddress : UserManagerTests
        {
            [TestMethod]
            public void GetUserIdForEmailAddress_GivenAddressInUse_ReturnsTheUserId()
            {
                TestDatabase.Execute(
                    @"INSERT INTO Users (UserType, EmailAddress, FirstName, LastName, PhoneNumber, Password_Hash, Password_Salt)
                      VALUES ('Consultant', 'bill@site.com', 'Bill', 'Smith', '1234567890', 'hash', 'salt')");
                UserManager subject = _fixture.Create<UserManager>();

                int? userId = subject.GetUserIdForEmailAddress("bill@site.com");

                Assert.IsNotNull(userId);
            }

            [TestMethod]
            public void GetUserIdForEmailAddress_GivenAddressNotInUse_ReturnsNull()
            {
                TestDatabase.Execute(
                     @"INSERT INTO Users (UserType, EmailAddress, FirstName, LastName, PhoneNumber, Password_Hash, Password_Salt)
                      VALUES ('Consultant', 'bill@site.com', 'Bill', 'Smith', '1234567890', 'hash', 'salt')");
                UserManager subject = _fixture.Create<UserManager>();

                int? userId = subject.GetUserIdForEmailAddress("some_other_address@site.com");

                Assert.IsNull(userId);
            }
        }

        [TestClass]
        public class ValidatePasswordAsync : UserManagerTests
        {
            [TestMethod]
            public async Task ValidatePasswordAsync_GivenCorrectPassword_ReturnsUser()
            {
                _fixture.Inject<ISaltGenerator>(new SaltGenerator());
                _fixture.Inject<IPasswordHasher>(new PasswordHasher());
                UserManager subject = _fixture.Create<UserManager>();

                await subject.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith", "1234567890");

                var user = await subject.ValidatePasswordAsync("bill@site.com", "pass");

                Assert.IsNotNull(user);
                Assert.AreEqual("bill@site.com", user.EmailAddress);
            }

            [TestMethod]
            public async Task ValidatePasswordAsync_GivenIncorrectPassword_ReturnsNull()
            {
                _fixture.Inject<ISaltGenerator>(new SaltGenerator());
                _fixture.Inject<IPasswordHasher>(new PasswordHasher());
                UserManager subject = _fixture.Create<UserManager>();

                await subject.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith", "1234567890");

                User user = await subject.ValidatePasswordAsync("bill@site.com", "incorrect password");

                Assert.IsNull(user);
            }

            [TestMethod]
            public async Task ValidatePasswordAsync_GivenEmailAddressThatDoesntExist_ReturnsNull()
            {   
                UserManager subject = _fixture.Create<UserManager>();

                User user = await subject.ValidatePasswordAsync("does_not_exist@site.com", "password");

                Assert.IsNull(user);
            }
        }
    }
}
