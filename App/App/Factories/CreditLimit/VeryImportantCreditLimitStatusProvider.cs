using System;

namespace App
{
    public class VeryImportantCreditLimitStatusProvider : ICreditLimitStatusProvider
    {
        public CreditStatus GetStatus(string firstname, string surname, DateTime dateOfBirth)
        {
            var status = new CreditStatus();
            status.HasCreditLimit = false;

            return status;
        }
    }
}