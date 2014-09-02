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
using SogetiSkills.UI.Infrastructure.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using SogetiSkills.UI.ViewModels.Account;

namespace SogetiSkills.Tests.Unit.UI.Controllers
{
    public class AccountControllerTests : UnitTestBase
    {
        [TestClass]
        public class SignIn : AccountControllerTests
        {
            [TestMethod]
            public void SignIn_GivenAnAuthenticatedRequest_ClearsTheAuthCookie()
            {
                var fakeHttpContext = new Mock<HttpContextBase>();
                fakeHttpContext.Setup(x => x.Request.IsAuthenticated).Returns(true);
                _fixture.Inject(fakeHttpContext);

                var fakeAuthentication = new Mock<IAuthentication>();
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
                subject.ModelState.AddModelError("", "Any error.");

                ActionResult actionResult = await subject.SignIn(model);

                AssertX.IsViewResultWithModel(actionResult, model);
            }

            [TestMethod]
            public async Task SignIn_GivenCorrectCredentials_LogsTheUserIn()
            {
                var fakeAuthentication = new Mock<IAuthentication>();
                fakeAuthentication.Setup(x => x.ValidateUsernamePasswordAsync("user", "pass")).Returns(Task.FromResult(true));
                _fixture.Inject(fakeAuthentication);

                SignInViewModel correctUsernamePassword = new SignInViewModel { Username = "user", Password = "pass" };
                AccountController subject = _fixture.Create<AccountController>();

                await subject.SignIn(correctUsernamePassword);

                fakeAuthentication.Verify(x => x.SetAuthCookie("user", It.IsAny<HttpContextBase>()));
            }

            [TestMethod]
            public async Task SignIn_GivenCorrectCredentials_RedirectsToTheHomePage()
            {
                var fakeAuthentication = new Mock<IAuthentication>();
                fakeAuthentication.Setup(x => x.ValidateUsernamePasswordAsync("user", "pass")).Returns(Task.FromResult(true));
                _fixture.Inject(fakeAuthentication);

                SignInViewModel correctUsernamePassword = new SignInViewModel { Username = "user", Password = "pass" };
                AccountController subject = _fixture.Create<AccountController>();

                ActionResult actionResult = await subject.SignIn(correctUsernamePassword);

                AssertX.IsRedirectToRouteResult(actionResult, MVC.Home.Name, MVC.Home.ActionNames.Index);
            }

            [TestMethod]
            public async Task SignIn_GivenInorrectCredentials_RedisplaysTheForm()
            {
                var fakeAuthentication = new Mock<IAuthentication>();
                fakeAuthentication.Setup(x => x.ValidateUsernamePasswordAsync("user", "incorrectpass")).Returns(Task.FromResult(false));
                _fixture.Inject(fakeAuthentication);

                SignInViewModel incorrectUsernamePassword = new SignInViewModel { Username = "user", Password = "incorrectpass" };
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
                var fakeAuthentication = new Mock<IAuthentication>();
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
    }
}
