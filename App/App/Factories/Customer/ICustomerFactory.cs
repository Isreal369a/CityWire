using System;

namespace App
{
    public interface ICustomerFactory
    {
        Customer BuildCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId);
    }
}