using System;
using System.Net.Mail;
using AppTripEver.Validation.Base;

namespace AppTripEver.Validation.Rules
{
    public class EmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            try
            {
                var valueString = value as string;
                if(value != null && !string.IsNullOrWhiteSpace(valueString))
                {
                    MailAddress m = new MailAddress(valueString);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
