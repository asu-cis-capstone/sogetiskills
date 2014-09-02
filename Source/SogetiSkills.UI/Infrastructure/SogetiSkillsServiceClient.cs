using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace SogetiSkills.UI.SogetiSkillsService
{
    public partial class SogetiSkillsServiceClient : IDisposable
    {
        public void Dispose()
        {
            if (this.State == CommunicationState.Faulted)
            {
                this.Abort();
            }
            else
            {
                this.Close();
            }
        }
    }
}