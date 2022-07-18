using Application.Commands.Response;

namespace Application.Queries.Interfaces
{
    public interface IStatementQuery
    {
        StatementResponse GetStatement(Guid accountId);
    }
}