using System;
using AppTripEver.Validation.Base;

namespace AppTripEver.Validation.Rules
{
    public class TenDigitsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            bool response = false;
            if (value != null)
            {
                var str = value as string;
                if(str.Length == 10)
                {
                    response = true;
                }
            }
            return response;
        }

        
    }
    
}
