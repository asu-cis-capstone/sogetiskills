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
        /// <summary>
        /// The id of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's email address.  This is also used when the user is logging in.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The user's phone number.
        /// </summary>
        public PhoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// The user's salted and hashed password.
        /// </summary>
        public HashedPassword Password { get; set; }
    }
}
