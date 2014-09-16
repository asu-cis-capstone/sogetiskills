using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Managers
{
    public interface IUserManager
    {
        int? GetUserIdForEmailAddress(string emailAddress);
        Task<User> ValidatePasswordAsync(string emailAddress, string plainTextPassword);
        Task<User> LoadUserByIdAsync(int userId);

        Task<T> RegisterNewUserAsync<T>(string emailAddress, string plainTextPassword, string firstName, string lastName, string phoneNumber) where T : User;
        Task UpdateContactInfoAsync(int userId, string firstName, string lastName, string emailAddress, string phoneNumber);
    }
}
