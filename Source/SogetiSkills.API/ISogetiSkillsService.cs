using SogetiSkills.API.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace SogetiSkills.API
{
    [ServiceContract]
    public interface ISogetiSkillsService
    {
        [OperationContract]
        string GetVersion();

        [OperationContract]
        Profile Profile_GetByUsername(string username);
    }
}
