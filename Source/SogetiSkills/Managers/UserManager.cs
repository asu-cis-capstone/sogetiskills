using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SogetiSkills.Security;

namespace SogetiSkills.Managers
{
    public class UserManager : IUserManager
    {
        private readonly SogetiSkillsDataContext _db;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public UserManager(SogetiSkillsDataContext db, 
            ISaltGenerator saltGenerator,
            IPasswordHasher passwordHasher)
        {
            _db = db;
            _saltGenerator = saltGenerator;
            _passwordHasher = passwordHasher;
        }

        public int? GetUserIdForEmailAddress(string emailAddress)
        {
            int userId = _db.Users.Where(x => x.EmailAddress == emailAddress)
                .Select(x => x.Id)
                .FirstOrDefault();
            if (userId == 0)
            {
                return null;
            }
            else
            {
                return userId;
            }
        }

        public async Task<User> ValidatePasswordAsync(string emailAddress, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.EmailAddress == emailAddress);
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
            return await _db.Users.FindAsync(userId);
        }

        public async Task<T> RegisterNewUserAsync<T>(string emailAddress, string plainTextPassword, string firstName, string lastName, string phoneNumber) where T : User
        {
            var user = _db.Set<T>().Create();

            user.EmailAddress = emailAddress;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = new PhoneNumber(phoneNumber);
            var hashedPassword = new HashedPassword();
            hashedPassword.Salt = _saltGenerator.GenerateNewSalt();
            hashedPassword.Hash = _passwordHasher.Hash(plainTextPassword, hashedPassword.Salt);
            user.Password = hashedPassword;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task UpdateContactInfoAsync(int userId, string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            var user = await _db.Users.FindAsync(userId);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.EmailAddress = emailAddress;
            user.PhoneNumber = new PhoneNumber(phoneNumber);
            await _db.SaveChangesAsync();
        }
    }
}
