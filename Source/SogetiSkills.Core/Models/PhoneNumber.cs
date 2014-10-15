using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Models
{
    /// <summary>
    /// Wraps a string the represents a phone number.
    /// </summary>
    public class PhoneNumber
    {
        private string _value;

        /// <summary>
        /// Creates a new instance of the PhoneNumber class.
        /// </summary>
        public PhoneNumber() { }

        /// <summary>
        /// Creates a new instance of the PhoneNumber class from an existing phone number.
        /// </summary>
        /// <param name="value">The phone number value.</param>
        public PhoneNumber(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the raw value for the phone number.
        /// </summary>
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

        /// <summary>
        /// Get the phone number formated as (123) 456-7890.
        /// </summary>
        /// <returns>The formatted phone number</returns>
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

        /// <summary>
        /// Validate that a string represents a phone number.
        /// </summary>
        /// <param name="input">The string to validate.</param>
        /// <returns>Whether or not the string represents a phone number.</returns>
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
