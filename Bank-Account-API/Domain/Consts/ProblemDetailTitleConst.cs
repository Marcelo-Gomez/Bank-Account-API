using System.Diagnostics.CodeAnalysis;

namespace Domain.Consts
{
    [ExcludeFromCodeCoverage]
    public static class ProblemDetailTitleConst
    {
        public const string BadRequestHttp = "The request is invalid";

        public const string BadRequestValidation = "One or more validation errors occurred.";

        public const string InternalServerError = "Internal server error.";
    }
}