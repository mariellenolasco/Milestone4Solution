namespace Lodging.Models
{
    internal interface IValidation
    {
        string ValidateString(string input);

        string ValidateDigit(double input);
    }
}