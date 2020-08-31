using System;
using System.Collections.Generic;
using System.Text;

namespace Lodging.Models
{
    interface IValidation
    {
        string ValidateString(string input);
        string ValidateDigit(double input);

    }
}
