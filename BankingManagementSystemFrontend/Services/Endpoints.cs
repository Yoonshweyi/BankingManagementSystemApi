namespace BankingManagementSystemFrontend.Services
{
    public static class Endpoints
    {
        public static string State { get; } = "api/State";
        public static string User { get; } = "api/User";
        public static string Account { get; } = "api/Account";

        public static string Deposit { get; } = "api/Transaction/Deposit";

        public static string Transfer { get; } = "/api/Transaction/Transfer";

        public static string Withdrawl { get; } = "/api/Transaction/Withdrawl";

        public static string Transaction { get; } = "/api/Transaction";

        public static string TransactionHistory { get; } = "api/Transaction/TransactionHistory";



        public static string WithPagination(this string url, int pageNo, int pageSize)
        {
            return $"{url}/{pageNo}/{pageSize}";
        }
    }
}
