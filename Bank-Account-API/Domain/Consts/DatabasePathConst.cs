using System.Diagnostics.CodeAnalysis;

namespace Domain.Consts
{
    [ExcludeFromCodeCoverage]
    public static class DatabasePathConst
    {
        public const string AccountPath = "../Account.json";

        public const string AccountHistoryPath = "../AccountHistory.json";
    }
}