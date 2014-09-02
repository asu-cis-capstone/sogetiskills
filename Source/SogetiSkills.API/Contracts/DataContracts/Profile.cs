using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SogetiSkills.API.Contracts.DataContracts
{
    [DataContract(Namespace = Constants.DataContractsNamespace)]
    public class Profile
    {
        [DataMember]
        public virtual string Username { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string Bio { get; set; }

        [DataMember]
        public IEnumerable<Skill> Skills { get; set; }
    }
}