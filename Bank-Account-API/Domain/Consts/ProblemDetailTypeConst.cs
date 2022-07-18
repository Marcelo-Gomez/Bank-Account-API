using System.Diagnostics.CodeAnalysis;

namespace Domain.Consts
{
    [ExcludeFromCodeCoverage]
    public static class ProblemDetailTypeConst
    {
        public const string BadRequest = "https://tools.ietf.org/html/rfc7231#section-6.5.1";

        public const string InternalServerError = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
    }
}