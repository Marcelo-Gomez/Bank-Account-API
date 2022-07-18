namespace Application.Helpers.Interfaces
{
    public interface IAccountOperationsHelper
    {
        void ValidateAccountExists(Guid accountId);

        void ValidateAmount(Guid accountId, double amount, double rateDiscount);

        double GetNewAccountValue(Guid accountId, double operationAmount, bool isDeposit);

        void InsertAccountTransaction(Guid accountId, double depositAmount, double depositedAmount, int transactionType);
    }
}