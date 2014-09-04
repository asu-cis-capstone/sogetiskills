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
        void Skill_AddCateogry(string name);

        [OperationContract]
        void Skill_AddSkill(string category, string name);

        [OperationContract]
        Profile Profile_GetByUsername(string username);
    }
}
