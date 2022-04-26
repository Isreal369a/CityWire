using App.DataAccess;
using App.DateTimeProvider;
using App.Validators;
using System;

namespace App
{
    public class CustomerService
    {        
        private readonly ICustomerFactory _customerFactory;
        private readonly ICustomerDataAccess _customerDataAccess;
        private readonly CustomerValidator _customerValidator;
        public CustomerService(CustomerValidator customerValidator, ICustomerFactory customerFactory, ICustomerDataAccess customerDataAccess)
        {
            _customerValidator = customerValidator;
            _customerFactory = customerFactory;
            _customerDataAccess = customerDataAccess;
        }       

        
        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            if(!ValidateCustomer(firname, surname, email, dateOfBirth))
            {
                return false;
            }

            Customer customer = _customerFactory.BuildCustomer(firname, surname, email, dateOfBirth, companyId);

            if (!_customerValidator.HasValidCreditLimit(customer))
            {
                return false;
            }

            _customerDataAccess.AddCustomer(customer);

            return true;
        }

        private bool ValidateCustomer(string firname, string surname, string email, DateTime dateOfBirth)
        {
            if (!_customerValidator.HasValidFirstNameAndSurName(firname, surname))
            {
                return false;
            }

            if (!_customerValidator.HasValidEmail(email))
            {
                return false;
            }

            if (!_customerValidator.HasValidAge(dateOfBirth))
            {
                return false;
            }

            return true;
        }

    }
}
