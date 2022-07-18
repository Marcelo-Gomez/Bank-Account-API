namespace Application.Queries.Interfaces
{
    public interface IAccountQuery
    {
        bool AccountExists(Guid AccountId);

        double GetCurrentAccountAmount(Guid AccountId);
    }
}