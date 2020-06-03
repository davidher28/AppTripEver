using System;
namespace AppTripEver.Validation.Base
{
    public interface IValidationRule<in T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);

    }
}

