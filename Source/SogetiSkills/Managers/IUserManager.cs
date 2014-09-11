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
        Task<T> RegisterNewUserAsync<T>(string emailAddress, string plainTextPassword, string firstName, string lastName, string phoneNumber) where T : User;
        bool IsEmailAddressInUse(string emailAddress);
        Task<bool> IsEmailAddressInUseAsync(string emailAddress);
        Task<User> ValidatePasswordAsync(string emailAddress, string plainTextPassword);
        Task<User> LoadUserById(int userId);
    }
}
