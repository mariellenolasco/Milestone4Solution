using System.Text.RegularExpressions;

namespace Lodging.Models
{
    public class Validation : IValidation
    {
        public string ValidateDigit(double input)
        {
            if (input < 0) return "Input should be nonnegative";
            return null;
        }

        public string ValidateString(string input)
        {
            if (Regex.Match(input, "[a-zA-z]+").Success) return null;
            return "Invalid string input";
        }
    }
}