using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// Helper class to holder strongly typed versions of the two possible values for the
    /// UserType discriminator column in the user table.
    /// </summary>
    public static class AccountTypes
    {
        public const string CONSULTANT = "Consultant";
        public const string ACCOUNT_EXECUTIVE = "AccountExecutive";

        public static IEnumerable<string> All
        {
            get
            {
                yield return CONSULTANT;
                yield return ACCOUNT_EXECUTIVE;
            }
        }
    }
}
