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
        IEnumerable<SkillCategory> Skills_GetAll();
    }
}
