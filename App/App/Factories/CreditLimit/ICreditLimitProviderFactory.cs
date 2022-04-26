namespace App
{
    public interface ICreditLimitProviderFactory
    {
        ICreditLimitStatusProvider GetProvider(string companyName);
    }
}