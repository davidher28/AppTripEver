using System;
using AppTripEver.Validation.Base;

namespace AppTripEver.Validation.Rules
{
    public class RequiredRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            bool response = false;
            if(value != null)
            {
                var str = value as string;
                response = !string.IsNullOrWhiteSpace(str);
            }
            return response;
        }

        public RequiredRule()
        {
        }
    }
}
