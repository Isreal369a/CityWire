using App.DateTimeProvider;
using System;

namespace App.Validators
{
    public class CustomerValidator
    {
        //TODO: make this Single Responsible by creating different validators
        private readonly IDateTimeProvider _dateTimeProvider;
        public CustomerValidator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
       
        public bool HasValidEmail(string email)
        {
            if(!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            return true;
        }

        public bool HasValidFirstNameAndSurName(string firstname, string surname)
        {
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            return true;
        }

        public bool HasValidAge(DateTime dateOfBirth)
        {
            var now = _dateTimeProvider.DateTimeNow;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        public bool HasValidCreditLimit(Customer customer)
        {
            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }
            return true;
        }
    }
}
