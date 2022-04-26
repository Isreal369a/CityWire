namespace App
{
    public class CreditLimitProviderFactory : ICreditLimitProviderFactory
    {
        private readonly ICustomerCreditService _customerCreditService;

        public CreditLimitProviderFactory(ICustomerCreditService customerCreditService)
        {
            _customerCreditService = customerCreditService;
        }

        public ICreditLimitStatusProvider GetProvider(string companyName)
        {

            if (companyName == "VeryImportantClient")
            {
                return new VeryImportantCreditLimitStatusProvider();
            }

            if (companyName == "ImportantClient")
            {
                return new ImportantCreditLimitStatusProvider(_customerCreditService);
            }

            return new DefaultCreditLimitStatusProvider(_customerCreditService);
        }
    }
}