using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcFlashMessages;
//using MvcFlashMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SogetiSkills.Tests.TestHelpers
{
    public static class AssertX
    {
        public static void IsViewResult(ActionResult actionResult)
        {
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        public static void IsViewResultWithModelOfType<TModel>(ActionResult actionResult)
        {
            IsViewResult(actionResult);
            ViewResult viewResult = (ViewResult)actionResult;
            Assert.AreEqual(viewResult.Model.GetType(), typeof(TModel));
        }

        public static void IsViewResultWithModel(ActionResult actionResult, object expectedModel)
        {
            IsViewResult(actionResult);
            ViewResult viewResult = (ViewResult)actionResult;
            Assert.AreEqual(viewResult.Model, expectedModel);
        }

        public static void IsRedirectToRouteResult(ActionResult actionResult, string controller, string actionMethod)
        {
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));

            RedirectToRouteResult redirectToRouteResult = (RedirectToRouteResult)actionResult;
            Assert.IsFalse(redirectToRouteResult.Permanent);
            Assert.AreEqual(redirectToRouteResult.RouteValues["Controller"], controller);
            Assert.AreEqual(redirectToRouteResult.RouteValues["Action"], actionMethod);
        }

        public static void FlashMessageSet(Controller controller, string flashMessageType, string message)
        {
            FlashMessageCollection flashMessages = new FlashMessageCollection(controller.TempData);
            bool flashMessageSet = flashMessages.Any(x => x.Key == flashMessageType && x.Message == message);
            Assert.IsTrue(flashMessageSet);
        }
    }
}
