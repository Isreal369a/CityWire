using System;

namespace App
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly ICreditLimitProviderFactory _creditLimitProviderFactory;
        private readonly ICompanyRepository _companyRepository;

        public CustomerFactory(ICreditLimitProviderFactory creditLimitProviderFactory, ICompanyRepository companyRepository)
        {
            _creditLimitProviderFactory = creditLimitProviderFactory;
            _companyRepository = companyRepository;
        }

        public Customer BuildCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            var company = _companyRepository.GetById(companyId);
            var customer = new Customer
            {
                Company = company,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstname,
                Surname = surname
            };

            ICreditLimitStatusProvider provider = _creditLimitProviderFactory.GetProvider(company.Name);

            CreditStatus status = provider.GetStatus(firstname, surname, dateOfBirth);
            customer.HasCreditLimit = status.HasCreditLimit;
            customer.CreditLimit = status.CreditLimit;
            

            return customer;
        }
      
    }
}
