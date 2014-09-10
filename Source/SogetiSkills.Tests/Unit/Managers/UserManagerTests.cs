using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Managers;
using SogetiSkills.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using SogetiSkills.Models;
using SogetiSkills.Security;
using Moq;

namespace SogetiSkills.Tests.Unit.Managers
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

                await subject.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith");

                var newUser = DataContext.Users.First(x => x.EmailAddress == "bill@site.com");
                Assert.IsNotNull(newUser);
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

                await subject.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith");

                var user = DataContext.Users.First(x => x.EmailAddress == "bill@site.com");
                Assert.AreEqual("hashed password", user.Password.Hash);
                Assert.AreEqual("salt", user.Password.Salt);
            }
        }

        [TestClass]
        public class EmailAddressInUse : UserManagerTests
        {
            [TestMethod]
            public async Task EmailAddressInUse_GivenAddressInUse_ReturnsTrue()
            {
                SogetiSkillsDataContext db = new SogetiSkillsDataContext();
                db.Users.Add(new Consultant
                {
                    EmailAddress = "bill@site.com",
                    FirstName = "Bill",
                    LastName = "Smith",
                    Password = new HashedPassword {  Hash = "hash", Salt = "salt" }
                });
                db.SaveChanges();
                UserManager subject = _fixture.Create<UserManager>();

                bool emailAddressIsInUse = await subject.IsEmailAddressInUseAsync("bill@site.com");

                Assert.IsTrue(emailAddressIsInUse);
            }

            [TestMethod]
            public async Task EmailAddressInUse_GivenAddressNotInUse_ReturnsFalse()
            {
                SogetiSkillsDataContext db = new SogetiSkillsDataContext();
                db.Users.Add(new Consultant
                {
                    EmailAddress = "bill@site.com",
                    FirstName = "Bill",
                    LastName = "Smith",
                    Password = new HashedPassword { Hash = "hash", Salt = "salt" }
                });
                db.SaveChanges();
                UserManager subject = _fixture.Create<UserManager>();

                bool emailAddressIsInUse = await subject.IsEmailAddressInUseAsync("some_other_address@site.com");

                Assert.IsFalse(emailAddressIsInUse);
            }
        }

        [TestClass]
        public class ValidatePasswordAsync : UserManagerTests
        {
            [TestMethod]
            public async Task ValidatePasswordAsync_GivenCorrectPassword_ReturnsTrue()
            {
                _fixture.Inject<ISaltGenerator>(new SaltGenerator());
                _fixture.Inject<IPasswordHasher>(new PasswordHasher());
                UserManager subject = _fixture.Create<UserManager>();

                await subject.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith");

                bool passwordIsCorrect = await subject.ValidatePasswordAsync("bill@site.com", "pass");

                Assert.IsTrue(passwordIsCorrect);
            }

            [TestMethod]
            public async Task ValidatePasswordAsync_GivenIncorrectPassword_ReturnsFalse()
            {
                _fixture.Inject<ISaltGenerator>(new SaltGenerator());
                _fixture.Inject<IPasswordHasher>(new PasswordHasher());
                UserManager subject = _fixture.Create<UserManager>();

                await subject.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith");

                bool passwordIsCorrect = await subject.ValidatePasswordAsync("bill@site.com", "incorrect password");

                Assert.IsFalse(passwordIsCorrect);
            }

            [TestMethod]
            public async Task ValidatePasswordAsync_GivenEmailAddressThatDoesntExist_ReturnsFalse()
            {   
                UserManager subject = _fixture.Create<UserManager>();

                bool passwordIsCorrect = await subject.ValidatePasswordAsync("does_not_exist@site.com", "password");

                Assert.IsFalse(passwordIsCorrect);
            }
        }
    }
}
