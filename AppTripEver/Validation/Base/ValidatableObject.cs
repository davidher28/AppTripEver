using System;
using System.Collections.Generic;
using System.Linq;
using AppTripEver.Services.Propagation;
using AppTripEver.Validation.Base;

namespace AppTripEver.Validation
{
    public class ValidatableObject<T> : NotificationObject
    {
        public List<IValidationRule<T>> Validation { get; set; }

        private List<string> errors;

        public bool IsValid { get; set; }

        private T value;

        public ValidatableObject()
        {
            IsValid = true;
            errors = new List<string>();
            Validation = new List<IValidationRule<T>>();
        }

        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public List<string> Errors
        {
            get { return errors; }
            set
            {
                this.errors = value;
                OnPropertyChanged();
            }
        }

        public bool Validate()
        {
            Errors.Clear();
            IEnumerable<string> errorsValidation = Validation.Where(value => !value.Check(Value))
                .Select(value => value.ValidationMessage);

            Errors = errorsValidation.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

    }
}
