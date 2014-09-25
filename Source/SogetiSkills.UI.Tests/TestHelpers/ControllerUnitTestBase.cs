using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ploeh.AutoFixture;

namespace SogetiSkills.UI.Tests.TestHelpers
{
    public class ControllerUnitTestBase : UnitTestBase
    {
        protected void SetLoggedInUserId(int? userId)
        {
            var fakeHttpContext = new Mock<HttpContextBase>();
            if (userId == null)
            {
                fakeHttpContext.Setup(x => x.Request.IsAuthenticated).Returns(false);
                fakeHttpContext.Setup(x => x.User.Identity.Name).Returns(null as string);
            }
            else
            {
                fakeHttpContext.Setup(x => x.Request.IsAuthenticated).Returns(true);
                fakeHttpContext.Setup(x => x.User.Identity.Name).Returns(userId.ToString());
            }
            _fixture.Inject(fakeHttpContext);
        }
    }
}
