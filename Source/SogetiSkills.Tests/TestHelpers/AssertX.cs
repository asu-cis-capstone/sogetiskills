using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcFlashMessages;
//using MvcFlashMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

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

        public static void IsRedirectToRouteResult(ActionResult actionResult, string controller, string actionMethod, object routeValues = null)
        {
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));

            RedirectToRouteResult redirectToRouteResult = (RedirectToRouteResult)actionResult;
            Assert.IsFalse(redirectToRouteResult.Permanent);
            Assert.AreEqual(redirectToRouteResult.RouteValues["Controller"], controller);
            Assert.AreEqual(redirectToRouteResult.RouteValues["Action"], actionMethod);

            if (routeValues != null)
            {
                var routeValueDictionary = new RouteValueDictionary(routeValues);
                foreach(var kvp  in routeValueDictionary)
                {
                    Assert.AreEqual(kvp.Value, redirectToRouteResult.RouteValues[kvp.Key]);
                }
            }
        }

        public static void FlashMessageSet(Controller controller, string flashMessageType, string message)
        {
            FlashMessageCollection flashMessages = new FlashMessageCollection(controller.TempData);
            bool flashMessageSet = flashMessages.Any(x => x.Key == flashMessageType && x.Message == message);
            Assert.IsTrue(flashMessageSet);
        }

        public static void IsRedirectResult(ActionResult actionResult, string expectedUrl)
        {
            Assert.IsInstanceOfType(actionResult, typeof(RedirectResult));
            RedirectResult redirectResult = (RedirectResult)actionResult;
            Assert.AreEqual(expectedUrl, redirectResult.Url);
        }

        public static void Is404NotFoundResult(ActionResult actionResult)
        {
            Assert.IsInstanceOfType(actionResult, typeof(HttpNotFoundResult));
        }

        public static void IsRedirectToRestrictedPage(ActionResult actionResult)
        {
            AssertX.IsRedirectToRouteResult(actionResult, MVC.Home.Name, MVC.Home.ActionNames.Restricted);
        }
    }
}
