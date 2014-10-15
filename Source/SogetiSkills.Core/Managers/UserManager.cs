using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SogetiSkills.Core.Security;
using SogetiSkills.Core.Helpers;
using System.Data.SqlClient;

namespace SogetiSkills.Core.Managers
{
    /// <summary>
    /// Provides data access for users.
    /// </summary>
    public class UserManager : ManagerBase, IUserManager
    {
        private readonly ISaltGenerator _saltGenerator;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Instantiate a new instance of the UserManager class.
        /// </summary>
        /// <param name="saltGenerator">Salt generator used to salt user passwords.</param>
        /// <param name="passwordHasher">Password hasher used to salt and hash user passwords.</param>
        public UserManager(ISaltGenerator saltGenerator, IPasswordHasher passwordHasher)
        {
            _saltGenerator = saltGenerator;
            _passwordHasher = passwordHasher;
        }

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
        public int? GetUserIdForEmailAddress(string emailAddress)
        {
            var command = new SqlCommand("User_SelectIdByEmailAddress", GetOpenConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@emailAddress", emailAddress);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    return reader.Field<int?>("Id");
                }
            }

            return null;
        }

        /// <summary>
        /// Validates an email address and password and returns the user if they match.
        /// </summary>
        /// <param name="emailAddress">The login email address.</param>
        /// <param name="plainTextPassword">The plain text password.  It will be salted and hashed inside this method.</param>
        /// <returns>The user that matched the email address/password.  Null if there was no match.</returns>
        public async Task<User> ValidatePasswordAsync(string emailAddress, string password)
        {
            // Get the user by their email address.
            int? userId = GetUserIdForEmailAddress(emailAddress);
            if (!userId.HasValue)
            {
                return null;
            }
            var user = await LoadUserByIdAsync(userId.Value);

            // Hash the input password and check if it matches the hash we have stored in the database.
            var expected = user.Password.Hash;
            var actual = _passwordHasher.Hash(password, user.Password.Salt);

            // If the hashes match then return the user, otherwise return null.
            bool isValid = expected.SequenceEqual(actual);
            if (isValid)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loads a single user by their id.
        /// </summary>
        /// <param name="userId">The id of the user to load.</param>
        /// <returns>The user.  Null if there are no matching users.</returns>
        public async Task<User> LoadUserByIdAsync(int userId)
        {
            var command = new SqlCommand("User_SelectById", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", userId);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return ReadUserRow(reader);
                }
            }
            
            return null;
        }

        /// <summary>
        /// Loads a single user by their id.
        /// </summary>
        /// <param name="userId">The id of the user to load.</param>
        /// <returns>The user.  Null if there are no matching users.</returns>
        public User LoadUserById(int userId)
        {
            var command = new SqlCommand("User_SelectById", GetOpenConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", userId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return ReadUserRow(reader);
                }
            }

            return null;
        }

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
        public async Task<T> RegisterNewUserAsync<T>(string emailAddress, string plainTextPassword, string firstName, string lastName, string phoneNumber) where T : User
        {
            var command = new SqlCommand("User_Insert", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@emailAddress", emailAddress);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@phoneNumber", phoneNumber);

            // UserType is a discriminator column on the user table.
            if (typeof(T) == typeof(Consultant))
            {
                command.Parameters.AddWithValue("@userType", AccountTypes.CONSULTANT);
            }
            else if (typeof(T) == typeof(AccountExecutive))
            {
                command.Parameters.AddWithValue("@userType", AccountTypes.ACCOUNT_EXECUTIVE);
            }

            // Salt and hash the password before storing it.
            string passwordSalt = _saltGenerator.GenerateNewSalt();
            string passwordHash = _passwordHasher.Hash(plainTextPassword, passwordSalt);
            command.Parameters.AddWithValue("@passwordSalt", passwordSalt);
            command.Parameters.AddWithValue("@passwordHash", passwordHash);
            
            int userId = Convert.ToInt32(await command.ExecuteScalarAsync());

            return (T)(await LoadUserByIdAsync(userId));
        }

        /// <summary>
        /// Updates just the contact info for a user.
        /// </summary>
        /// <param name="userId">The id of the user to update.</param>
        /// <param name="firstName">The new first name to store for the user.</param>
        /// <param name="lastName">The new last name to store for the user.</param>
        /// <param name="emailAddress">The new email address to store for the user.</param>
        /// <param name="phoneNumber">The new phone number to store for the user.</param>
        public async Task UpdateContactInfoAsync(int userId, string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            var command = new SqlCommand("User_UpdateContactInfo", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@emailAddress", emailAddress);
            command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            await command.ExecuteNonQueryAsync();
        }

        #region Private helper methods
        // Helper methods for creating user objects from a data reader.
        private User ReadUserRow(SqlDataReader reader)
        {
            User user = null;

            // UserType is a discriminator column on the user table.
            string userType = reader.Field<string>("UserType");
            if (userType == AccountTypes.CONSULTANT)
            {
                var consultant = new Consultant();
                consultant.IsOnBeach = reader.Field<bool>("IsOnBeach");
                user = consultant;
            }
            else if (userType == AccountTypes.ACCOUNT_EXECUTIVE)
            {
                user = new AccountExecutive();
            }

            user.Id = reader.Field<int>("Id");
            user.EmailAddress = reader.Field<string>("EmailAddress");
            user.FirstName = reader.Field<string>("FirstName");
            user.LastName = reader.Field<string>("LastName");
            user.PhoneNumber = new PhoneNumber(reader.Field<string>("PhoneNumber"));
            user.Password = new HashedPassword();
            user.Password.Hash = reader.Field<string>("Password_Hash");
            user.Password.Salt = reader.Field<string>("Password_Salt");
            
            return user;
        }
        #endregion
    }
}
