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
        protected Mock<HttpContextBase> _fakeHttpContext;
        protected Mock<IUserManager> _fakeUserManager;
        protected Mock<IAuthCookieHelper> _fakeAuthCookieHelper;

        public AccountControllerTests()
        {
            _fakeHttpContext = new Mock<HttpContextBase>();
            _fixture.Inject(_fakeHttpContext);

            _fakeUserManager = new Mock<IUserManager>();
            _fixture.Inject(_fakeUserManager);

            _fakeAuthCookieHelper = new Mock<IAuthCookieHelper>();
            _fixture.Inject(_fakeAuthCookieHelper);
        }

        [TestClass]
        public class SignIn : AccountControllerTests
        {
            [TestMethod]
            public void SignIn_GivenAnAuthenticatedRequest_ClearsTheAuthCookie()
            {
                _fakeHttpContext.Setup(x => x.Request.IsAuthenticated).Returns(true);
                AccountController subject = _fixture.Create<AccountController>();

                subject.SignIn();

                _fakeAuthCookieHelper.Verify(x => x.ClearAuthCookie(_fakeHttpContext.Object));
            }

            [TestMethod]
            public void SignIn_ReturnsAViewResult()
            {
                _fakeHttpContext.Setup(x => x.Request.IsAuthenticated).Returns(false);
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
                _fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "pass")).Returns(Task.FromResult((User)_consultant123));

                SignInViewModel correctUsernamePassword = new SignInViewModel { EmailAddress = "user@site.com", Password = "pass" };
                AccountController subject = _fixture.Create<AccountController>();

                await subject.SignIn(correctUsernamePassword);

                _fakeAuthCookieHelper.Verify(x => x.SetAuthCookie(123, It.IsAny<HttpContextBase>()));
            }

            [TestMethod]
            public async Task SignIn_GivenCorrectCredentials_RedirectsToUsersProfile()
            {
                _fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "pass")).Returns(Task.FromResult((User)_consultant123));
                SignInViewModel correctUsernamePassword = new SignInViewModel { EmailAddress = "user@site.com", Password = "pass" };
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = await subject.SignIn(correctUsernamePassword);

                AssertX.IsRedirectToRouteResult(actionResult, MVC.Profile.Name, MVC.Profile.ActionNames.Details, new { userId = 123 });
            }

            [TestMethod]
            public async Task SignIn_GivenRelativeReturnUrl_RedirectsToReturnUrl()
            {
                _fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "pass")).Returns(Task.FromResult((User)_consultant123));
                _fakeHttpContext.Setup(x => x.Request.Url).Returns(new Uri("http://sogetiskills.com/"));
                SignInViewModel correctUsernamePassword = new SignInViewModel 
                { 
                    EmailAddress = "user@site.com", 
                    Password = "pass",
                    ReturnUrl = "/some_page.aspx"
                };
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = await subject.SignIn(correctUsernamePassword);

                AssertX.IsRedirectResult(actionResult, "/some_page.aspx");
            }

            [TestMethod]
            public async Task SignIn_GivenAbsoluteReturnUrlToSameDomain_RedirectsToReturnUrl()
            {
                _fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "pass")).Returns(Task.FromResult((User)_consultant123));
                _fakeHttpContext.Setup(x => x.Request.Url).Returns(new Uri("http://sogetiskills.com/"));
                SignInViewModel correctUsernamePassword = new SignInViewModel
                {
                    EmailAddress = "user@site.com",
                    Password = "pass",
                    ReturnUrl = "http://sogetiskills.com/some_page.aspx"
                };
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = await subject.SignIn(correctUsernamePassword);

                AssertX.IsRedirectResult(actionResult, "http://sogetiskills.com/some_page.aspx");
            }

            [TestMethod]
            public async Task SignIn_GivenReturnUrlToDifferentDomain_RedirectsToUsersProfile()
            {
                _fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "pass")).Returns(Task.FromResult((User)_consultant123));
                _fakeHttpContext.Setup(x => x.Request.Url).Returns(new Uri("http://sogetiskills.com/"));
                SignInViewModel correctUsernamePassword = new SignInViewModel
                {
                    EmailAddress = "user@site.com",
                    Password = "pass",
                    ReturnUrl = "http://www.google.com/"
                };
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = await subject.SignIn(correctUsernamePassword);

                AssertX.IsRedirectToRouteResult(actionResult, MVC.Profile.Name, MVC.Profile.ActionNames.Details, new { userId = 123 });
            }

            [TestMethod]
            public async Task SignIn_GivenInorrectCredentials_RedisplaysTheForm()
            {
                _fakeUserManager.Setup(x => x.ValidatePasswordAsync("user@site.com", "incorrectpass")).Returns(Task.FromResult(null as User));
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
                AccountController subject = _fixture.Create<AccountController>();

                subject.SignOut();

                _fakeAuthCookieHelper.Verify(x => x.ClearAuthCookie(It.IsAny<HttpContextBase>()));
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
        public class Register : AccountControllerTests
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
                _fakeUserManager
                    .Setup(x => x.RegisterNewUserAsync<Consultant>(_validRegistrationInput.EmailAddress, _validRegistrationInput.Password, _validRegistrationInput.FirstName, _validRegistrationInput.LastName, _validRegistrationInput.PhoneNumber))
                    .Returns(Task.FromResult(new Consultant { Id = 123 }));

                _validRegistrationInput.AccountType = "Consultant";
                AccountController subject = _fixture.Create<AccountController>();

                await subject.Register(_validRegistrationInput);

                _fakeUserManager.Verify(x => x.RegisterNewUserAsync<Consultant>("bill@site.com", "pass", "Bill", "Smith", "1234567890"));
            }

            [TestMethod]
            public async Task Register_GivenValidInputWithAccountExecutiveAccountType_RegistersNewConsultant()
            {
                _fakeUserManager
                    .Setup(x => x.RegisterNewUserAsync<AccountExecutive>(_validRegistrationInput.EmailAddress, _validRegistrationInput.Password, _validRegistrationInput.FirstName, _validRegistrationInput.LastName, _validRegistrationInput.PhoneNumber))
                    .Returns(Task.FromResult(new AccountExecutive { Id = 123 }));
                
                _validRegistrationInput.AccountType = "AccountExecutive";
                AccountController subject = _fixture.Create<AccountController>();

                await subject.Register(_validRegistrationInput);

                _fakeUserManager.Verify(x => x.RegisterNewUserAsync<AccountExecutive>("bill@site.com", "pass", "Bill", "Smith", "1234567890"));
            }

            [TestMethod]
            public async Task Register_GivenValidInput_LogsTheUserIn()
            {
                _fakeUserManager
                    .Setup(x => x.RegisterNewUserAsync<Consultant>(_validRegistrationInput.EmailAddress, _validRegistrationInput.Password, _validRegistrationInput.FirstName, _validRegistrationInput.LastName, _validRegistrationInput.PhoneNumber))
                    .Returns(Task.FromResult(new Consultant { Id = 123 }));

                AccountController subject = _fixture.Create<AccountController>();

                await subject.Register(_validRegistrationInput);

                _fakeAuthCookieHelper.Verify(x => x.SetAuthCookie(123, It.IsAny<HttpContextBase>()));
            }

            [TestMethod]
            public async Task Register_GivenValidInput_RedirectsToTheNewUsersProfilePage()
            {
                _fakeUserManager
                    .Setup(x => x.RegisterNewUserAsync<Consultant>(_validRegistrationInput.EmailAddress, _validRegistrationInput.Password, _validRegistrationInput.FirstName, _validRegistrationInput.LastName, _validRegistrationInput.PhoneNumber))
                    .Returns(Task.FromResult(new Consultant { Id = 123 }));
                AccountController subject = _fixture.Create<AccountController>();

                var actionResult = await subject.Register(_validRegistrationInput);

                AssertX.IsRedirectToRouteResult(actionResult, MVC.Profile.Name, MVC.Profile.ActionNames.Details);
            }
        }
    }
}
