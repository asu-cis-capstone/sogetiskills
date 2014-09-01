using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SogetiSkills.API.Contracts.DataContracts
{
    [DataContract(Namespace = Constants.DataContractsNamespace)]
    public class SkillCategory
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public IEnumerable<Skill> Skills { get; set; }
    }
}