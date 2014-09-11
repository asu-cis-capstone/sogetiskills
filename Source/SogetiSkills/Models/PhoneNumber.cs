using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Models
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

        private string RemoveNonDigits(string input)
        {
            if (input == null)
            {
                return null;
            }

            return new string(input.Where(x => char.IsDigit(x)).ToArray());
        }

        public string GetFormattedValue()
        {
            if (_value == null)
            {
                return null;
            }

            return string.Format("({0}{1}{2}) {3}{4}{5}-{6}{7}{8}{9}", _value.ToArray());
        }
    }
}
