using SogetiSkills.Helpers;
using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SogetiSkills.Managers
{
    public class AuthenticationManager
    {
        private readonly SogetiSkillsDataContext _db;
        private readonly SaltGenerator _salt;

        public AuthenticationManager(SogetiSkillsDataContext db, SaltGenerator salt)
        {
            _db = db;
            _salt = salt;
        }

        public virtual HashedPassword HashNewPassword(string password)
        {
            return HashPassword(password, _salt.Generate()); 
        }

        public virtual HashedPassword HashPassword(string password, byte[] salt)
        {
            var hashedPassword = new HashedPassword();
            hashedPassword.Salt = salt;
            using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password, hashedPassword.Salt))
            {
                hashedPassword.Hash = hasher.GetBytes(256);
            }
            return hashedPassword;
        }

        public virtual async Task SetPasswordAsync(string username, string password)
        {
            var newHashedPassword = HashNewPassword(password);

            var userPassword = await GetHashedPasswordForUsernameAsync(username);
            userPassword.Salt = newHashedPassword.Salt;
            userPassword.Hash = newHashedPassword.Hash;
            await _db.SaveChangesAsync();
        }

        public virtual async Task<bool> ValidatePasswordAsync(string username, string password)
        {
            var userPassword = await GetHashedPasswordForUsernameAsync(username);
            var expected = userPassword.Hash;
            var actual = HashPassword(password, userPassword.Salt).Hash;

            bool isValid = expected.SequenceEqual(actual);
            return isValid;
        }

        private async Task<HashedPassword> GetHashedPasswordForUsernameAsync(string username)
        {
            return await _db.Users.Where(x => x.Username == username)
                .Select(x => x.Password)
                .FirstAsync();
        }
    }
}
