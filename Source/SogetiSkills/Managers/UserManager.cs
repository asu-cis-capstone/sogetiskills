using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SogetiSkills.Security;
using SogetiSkills.Helpers;
using System.Data.SqlClient;

namespace SogetiSkills.Managers
{
    public class UserManager : ManagerBase, IUserManager
    {
        private readonly ISaltGenerator _saltGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public UserManager(ISaltGenerator saltGenerator, IPasswordHasher passwordHasher)
        {
            _saltGenerator = saltGenerator;
            _passwordHasher = passwordHasher;
        }

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

        public async Task<User> ValidatePasswordAsync(string emailAddress, string password)
        {
            var command = new SqlCommand("User_SelectByEmailAddress", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@emailAddress", emailAddress);

            User user = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
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
                }
            }
            
            if (user == null)
            {
                return null;
            }
            var expected = user.Password.Hash;
            var actual = _passwordHasher.Hash(password, user.Password.Salt);

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

        public async Task<User> LoadUserByIdAsync(int userId)
        {
            var command = new SqlCommand("User_SelectById", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", userId);

            User user = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
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
                }
            }
            return user;
        }

        public async Task<T> RegisterNewUserAsync<T>(string emailAddress, string plainTextPassword, string firstName, string lastName, string phoneNumber) where T : User
        {
            var command = new SqlCommand("User_Insert", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@emailAddress", emailAddress);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
           
            if (typeof(T) == typeof(Consultant))
            {
                command.Parameters.AddWithValue("@userType", AccountTypes.CONSULTANT);
            }
            else if (typeof(T) == typeof(AccountExecutive))
            {
                command.Parameters.AddWithValue("@userType", AccountTypes.ACCOUNT_EXECUTIVE);
            }

            string passwordSalt = _saltGenerator.GenerateNewSalt();
            string passwordHash = _passwordHasher.Hash(plainTextPassword, passwordSalt);
            command.Parameters.AddWithValue("@passwordSalt", passwordSalt);
            command.Parameters.AddWithValue("@passwordHash", passwordHash);
            
            int userId = Convert.ToInt32(await command.ExecuteScalarAsync());

            return (T)(await LoadUserByIdAsync(userId));
        }

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
    }
}
