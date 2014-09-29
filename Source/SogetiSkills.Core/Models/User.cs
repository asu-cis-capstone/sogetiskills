using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// Abstract base class for a user of the system.  Users are either consultants or
    /// account executives.  The user class holds contact and login information which
    /// are applicable to both user types.
    /// </summary>
    public abstract class User
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public HashedPassword Password { get; set; }
    }
}
