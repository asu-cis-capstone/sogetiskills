using Ploeh.AutoFixture;
using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Tests.TestHelpers
{
    public abstract class UnitTestBase
    {
        protected readonly IFixture _fixture;

        public UnitTestBase()
        {
            _fixture = new Fixture();
            _fixture.Customize(new WebModelCustomization());
            _fixture.Inject(new SogetiSkillsDataContext());
        }
    }
}
