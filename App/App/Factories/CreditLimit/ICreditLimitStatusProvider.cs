using System;

namespace App
{
    public interface ICreditLimitStatusProvider
    {
        CreditStatus GetStatus(string firstname, string surname, DateTime dateOfBirth);
    }
}