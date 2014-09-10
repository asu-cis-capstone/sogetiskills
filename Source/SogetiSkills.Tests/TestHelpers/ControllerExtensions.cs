using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SogetiSkills.Tests.TestHelpers
{
    public static class ControllerExtensions
    {
        public static void AddModelError(this Controller controller)
        {
            controller.ModelState.AddModelError(string.Empty, "Error message.");
        }
    }
}
