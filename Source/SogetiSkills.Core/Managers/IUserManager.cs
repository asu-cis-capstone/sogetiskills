using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Managers
{
    /// <summary>
    /// Provides data access for users.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Get the id of the user that is using an email address.  If the email address is not in use then
        /// null is returned.
        /// </summary>
        /// <remarks>
        /// This is generally used for validating that an email address is not in use during registration. Because
        /// FluentValidtion does not work with async/await yet, this method is synchronous.
        /// </remarks>
        /// <param name="emailAddress">The email address to search for.</param>
        /// <returns>The id of the user that is using the email address.</returns>
        int? GetUserIdForEmailAddress(string emailAddress);

        /// <summary>
        /// Validates an email address and password and returns the user if they match.
        /// </summary>
        /// <param name="emailAddress">The login email address.</param>
        /// <param name="plainTextPassword">The plain text password.  It will be salted and hashed inside this method.</param>
        /// <returns>The user that matched the email address/password.  Null if there was no match.</returns>
        Task<User> ValidatePasswordAsync(string emailAddress, string plainTextPassword);

        /// <summary>
        /// Loads a single user by their id.
        /// </summary>
        /// <param name="userId">The id of the user to load.</param>
        /// <returns>The user.  Null if there are no matching users.</returns>
        Task<User> LoadUserByIdAsync(int userId);

        /// <summary>
        /// Loads a single user by their id.
        /// </summary>
        /// <param name="userId">The id of the user to load.</param>
        /// <returns>The user.  Null if there are no matching users.</returns>
        /// <remarks>
        /// An synchronous version is provided so that it can be used in an action filter attribute.
        /// Async action filter attributes are not yet supported.
        /// </remarks>
        User LoadUserById(int userId);

        /// <summary>
        /// Registers and inserts a new user.
        /// </summary>
        /// <typeparam name="T">The type of user to insert - either a Consultant or Account Executive.</typeparam>
        /// <param name="emailAddress">The email address for the new user.  Also used to log in.</param>
        /// <param name="plainTextPassword">The password for the new user.</param>
        /// <param name="firstName">The new user's first name.</param>
        /// <param name="lastName">The new user's last name.</param>
        /// <param name="phoneNumber">The new user's phone number.</param>
        /// <returns>The new user with its id populated.</returns>
        Task<T> RegisterNewUserAsync<T>(string emailAddress, string plainTextPassword, string firstName, string lastName, string phoneNumber) where T : User;

        /// <summary>
        /// Updates just the contact info for a user.
        /// </summary>
        /// <param name="userId">The id of the user to update.</param>
        /// <param name="firstName">The new first name to store for the user.</param>
        /// <param name="lastName">The new last name to store for the user.</param>
        /// <param name="emailAddress">The new email address to store for the user.</param>
        /// <param name="phoneNumber">The new phone number to store for the user.</param>
        Task UpdateContactInfoAsync(int userId, string firstName, string lastName, string emailAddress, string phoneNumber);
    }
}
