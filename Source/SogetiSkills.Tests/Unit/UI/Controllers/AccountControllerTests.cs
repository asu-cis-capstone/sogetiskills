using SogetiSkills.Tests.TestHelpers;
using SogetiSkills.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Moq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using SogetiSkills.UI.ViewModels.Account;
using SogetiSkills.Managers;
using SogetiSkills.Models;
using SogetiSkills.UI.Helpers.Security;

namespace SogetiSkills.Tests.Unit.UI.Controllers
{
    public class AccountControllerTests : UnitTestBase
    {
        protected Consultant _consultant123 = new Consultant { Id = 123 };
        protected AccountExecutive _accountExecutive456 = new AccountExecutive { Id = 456 };

        [TestClass]
        public class SignIn : AccountControllerTests
        {
            [TestMethod]
            public void SignIn_GivenAnAuthenticatedRequest_ClearsTheAuthCookie()
            {
                var fakeHttpContext = new Mock<HttpContextBase>();
                fakeHttpContext.Setup(x => x.Request.IsAuthenticated).Returns(true);
                _fixture.Inject(fakeHttpContext);

                var fakeAuthentication = new Mock<IAuthCookieHelper>();
                _fixture.Inject(fakeAuthentication);

                AccountController subject = _fixture.Create<AccountController>();

                subject.SignIn();

                fakeAuthentication.Verify(x => x.ClearAuthCookie(fakeHttpContext.Object));
            }

            [TestMethod]
            public void SignIn_ReturnsAViewResult()
            {
                var fakeHttpContext = new Mock<HttpContextBase>();
                fakeHttpContext.Setup(x => x.Request.IsAuthenticated).Returns(false);
                _fixture.Inject(fakeHttpContext);
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = subject.SignIn();

                AssertX.IsViewResult(actionResult);
            }

            [TestMethod]
            public async Task SignIn_GivenInvalidInput_RedisplaysTheForm()
            {
                var model = new SignInViewModel();
                AccountController subject = _fixture.Create<AccountController>();
                subject.AddModelError();

                ActionResult actionResult = await subject.SignIn(model);

                AssertX.IsViewResultWithModel(actionResult, model);
            }

            [TestMethod]
            public async Task SignIn_GivenCorrectCredentials_LogsTheUserIn()
            {
                var fakeAuthCookieHelper = new Mock<IAuthCookieHelper>();
                _fixture.Inject(fakeAuthCookieHelper);
                var fakeUserManager = new Mock<IUserManager>();
                fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "pass")).Returns(Task.FromResult((User)_consultant123));
                _fixture.Inject(fakeUserManager);

                SignInViewModel correctUsernamePassword = new SignInViewModel { EmailAddress = "user@site.com", Password = "pass" };
                AccountController subject = _fixture.Create<AccountController>();

                await subject.SignIn(correctUsernamePassword);

                fakeAuthCookieHelper.Verify(x => x.SetAuthCookie(123, It.IsAny<HttpContextBase>()));
            }

            [TestMethod]
            public async Task SignIn_GivenCorrectCredentials_RedirectsToTheHomePage()
            {
                var fakeUserManager = new Mock<IUserManager>();
                fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "pass")).Returns(Task.FromResult((User)_consultant123));
                _fixture.Inject(fakeUserManager);

                SignInViewModel correctUsernamePassword = new SignInViewModel { EmailAddress = "user@site.com", Password = "pass" };
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = await subject.SignIn(correctUsernamePassword);

                AssertX.IsRedirectToRouteResult(actionResult, MVC.Home.Name, MVC.Home.ActionNames.Index);
            }

            [TestMethod]
            public async Task SignIn_GivenInorrectCredentials_RedisplaysTheForm()
            {
                var fakeUserManager = new Mock<IUserManager>();
                fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "incorrectpass")).Returns(Task.FromResult(null as User));
                _fixture.Inject(fakeUserManager);

                SignInViewModel incorrectUsernamePassword = new SignInViewModel { EmailAddress = "user@site.com", Password = "incorrectpass" };
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = await subject.SignIn(incorrectUsernamePassword);

                AssertX.IsViewResultWithModel(actionResult, incorrectUsernamePassword);
            }
        }

        [TestClass]
        public class SignOut : AccountControllerTests
        {
            [TestMethod]
            public void SignOut_ClearsTheAuthCookie()
            {
                var fakeAuthentication = new Mock<IAuthCookieHelper>();
                _fixture.Inject(fakeAuthentication);

                AccountController subject = _fixture.Create<AccountController>();

                subject.SignOut();

                fakeAuthentication.Verify(x => x.ClearAuthCookie(It.IsAny<HttpContextBase>()));
            }

            [TestMethod]
            public void SignOut_RedirectsTheUserToTheSignInPageWithFlashMessage()
            {
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = subject.SignOut();

                AssertX.FlashMessageSet(subject, "info", "You have been signed out.");
                AssertX.IsRedirectToRouteResult(actionResult, MVC.Account.Name, MVC.Account.ActionNames.SignIn);
            }
        }

        [TestClass]
        public class Register : UnitTestBase
        {
            private RegisterViewModel _validRegistrationInput = new RegisterViewModel
                {
                    EmailAddress = "bill@site.com",
                    Password = "pass",
                    ConfirmPassword = "pass",
                    FirstName = "Bill",
                    LastName = "Smith",
                    PhoneNumber = "1234567890",
                    AccountType = "Consultant"
                };
  
            [TestMethod]
            public void Register_ReturnsAViewResult()
            {
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = subject.Register();

                AssertX.IsViewResultWithModelOfType<RegisterViewModel>(actionResult);
            }

            [TestMethod]
            public async Task Register_GivenInvalidInput_RedisplaysTheForm()
            {
                AccountController subject = _fixture.Create<AccountController>();
                subject.AddModelError();

                var viewModel = new RegisterViewModel();
                ActionResult actionResult = await subject.Register(viewModel);

                AssertX.IsViewResultWithModel(actionResult, viewModel);
            }

            [TestMethod]
            public async Task Register_GivenValidInputWithConsultantAccountType_RegistersNewConsultant()
            {
                var fakeUserManager = new Mock<IUserManager>();
                _fixture.Inject(fakeUserManager.Object);
                fakeUserManager
                    .Setup(x => x.RegisterNewUserAsync<Consultant>(_validRegistrationInput.EmailAddress, _validRegistrationInput.Password, _validRegistrationInput.FirstName, _validRegistrationInput.LastName, _validRegistrationInput.PhoneNumber))
                    .Returns(Task.FromResult(new Consultant { Id = 123 }));

                _validRegistrationInput.AccountType = "Consultant";
                AccountController subject = _fixture.Create<AccountController>();

                await subject.Register(_validRegistrationInput);

                fakeUserManager.Verify(x => x.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith", "1234567890"));
            }

            [TestMethod]
            public async Task Register_GivenValidInputWithAccountExecutiveAccountType_RegistersNewConsultant()
            {
                var fakeUserManager = new Mock<IUserManager>();
                _fixture.Inject(fakeUserManager);
                fakeUserManager
                    .Setup(x => x.RegisterNewUserAsync<AccountExecutive>(_validRegistrationInput.EmailAddress, _validRegistrationInput.Password, _validRegistrationInput.FirstName, _validRegistrationInput.LastName, _validRegistrationInput.PhoneNumber))
                    .Returns(Task.FromResult(new AccountExecutive { Id = 123 }));
                
                _validRegistrationInput.AccountType = "AccountExecutive";
                AccountController subject = _fixture.Create<AccountController>();

                await subject.Register(_validRegistrationInput);

                fakeUserManager.Verify(x => x.RegisterNewUserAsync<AccountExecutive>("bill@site.com", "pass", "Bill", "Smith", "1234567890"));
            }

            [TestMethod]
            public async Task Register_GivenValidInput_LogsTheUserIn()
            {
                var fakeAuthenticationHelper = new Mock<IAuthCookieHelper>();
                _fixture.Inject(fakeAuthenticationHelper);
                
                var fakeUserManager = new Mock<IUserManager>();
                fakeUserManager
                    .Setup(x => x.RegisterNewUserAsync<Consultant>(_validRegistrationInput.EmailAddress, _validRegistrationInput.Password, _validRegistrationInput.FirstName, _validRegistrationInput.LastName, _validRegistrationInput.PhoneNumber))
                    .Returns(Task.FromResult(new Consultant { Id = 123 }));
                _fixture.Inject(fakeUserManager);

                AccountController subject = _fixture.Create<AccountController>();

                await subject.Register(_validRegistrationInput);

                fakeAuthenticationHelper.Verify(x => x.SetAuthCookie(123, It.IsAny<HttpContextBase>()));
            }

            [TestMethod]
            public async Task Register_GivenValidInput_RedirectsToTheNewUsersProfilePage()
            {
                var fakeAuthenticationHelper = new Mock<IAuthCookieHelper>();
                _fixture.Inject(fakeAuthenticationHelper.Object);
                var fakeUserManager = new Mock<IUserManager>();
                fakeUserManager
                    .Setup(x => x.RegisterNewUserAsync<Consultant>(_validRegistrationInput.EmailAddress, _validRegistrationInput.Password, _validRegistrationInput.FirstName, _validRegistrationInput.LastName, _validRegistrationInput.PhoneNumber))
                    .Returns(Task.FromResult(new Consultant { Id = 123 }));
                _fixture.Inject(fakeUserManager);
                AccountController subject = _fixture.Create<AccountController>();

                var actionResult = await subject.Register(_validRegistrationInput);

                AssertX.IsRedirectToRouteResult(actionResult, MVC.Profile.Name, MVC.Profile.ActionNames.Details);
            }
        }
    }
}
