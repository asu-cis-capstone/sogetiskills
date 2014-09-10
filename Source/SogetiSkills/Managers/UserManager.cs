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

        public async Task<T> RegisterNewUserAsync<T>(string emailAddress, string plainTextPassword, string firstName, string lastName) where T : User
        {
            var user = _db.Set<T>().Create();

            user.EmailAddress = emailAddress;
            user.FirstName = firstName;
            user.LastName = lastName;
            var hashedPassword = new HashedPassword();
            hashedPassword.Salt = _saltGenerator.GenerateNewSalt();
            hashedPassword.Hash = _passwordHasher.Hash(plainTextPassword, hashedPassword.Salt);
            user.Password = hashedPassword;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public bool IsEmailAddressInUse(string emailAddress)
        {
            return _db.Users.Any(x => x.EmailAddress == emailAddress);
        }

        public async Task<bool> IsEmailAddressInUseAsync(string emailAddress)
        {
            return await _db.Users.AnyAsync(x => x.EmailAddress == emailAddress);
        }

        public virtual async Task<bool> ValidatePasswordAsync(string emailAddress, string password)
        {
            var userPassword = await GetHashedPasswordForEmailAddressAsync(emailAddress);
            var expected = userPassword.Hash;
            var actual = _passwordHasher.Hash(password, userPassword.Salt);

            bool isValid = expected.SequenceEqual(actual);
            return isValid;
        }

        private async Task<HashedPassword> GetHashedPasswordForEmailAddressAsync(string emailAddress)
        {
            return await _db.Users.Where(x => x.EmailAddress == emailAddress)
                .Select(x => x.Password)
                .FirstAsync();
        }
    }
}
