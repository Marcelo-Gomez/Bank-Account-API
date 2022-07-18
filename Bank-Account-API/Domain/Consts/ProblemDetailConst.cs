using System.Diagnostics.CodeAnalysis;

namespace Domain.Consts
{
    [ExcludeFromCodeCoverage]
    public static class ProblemDetailConst
    {
        public const string InternalServerError = "The server encountered an unexpected condition that prevented it from fulfilling the request.";

        public const string BadRequestAccountNotExistError = "Informed account does not exist.";

        public const string BadRequestInvalidValueError = "Transaction value greater than the account value.";
    }
}