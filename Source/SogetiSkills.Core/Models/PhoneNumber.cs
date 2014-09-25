using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    public class PhoneNumber
    {
        private string _value;

        public PhoneNumber() { }

        public PhoneNumber(string value)
        {
            this.Value = value;
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = RemoveNonDigits(value);
            }
        }

        public string GetFormattedValue()
        {
            if (_value == null)
            {
                return null;
            }

            return string.Format("({0}{1}{2}) {3}{4}{5}-{6}{7}{8}{9}",
                Value[0],
                Value[1],
                Value[2],
                Value[3],
                Value[4],
                Value[5],
                Value[6],
                Value[7],
                Value[8],
                Value[9]);
        }

        public static bool IsValid(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return RemoveNonDigits(input).Length == 10;
        }

        private static string RemoveNonDigits(string input)
        {
            if (input == null)
            {
                return null;
            }

            return new string(input.Where(x => char.IsDigit(x)).ToArray());
        }
    }
}
