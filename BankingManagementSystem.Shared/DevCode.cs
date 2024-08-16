namespace BankingManagementSystem.Shared
{
    public static class DevCode
    {
        public static string GenerateBankAccountNo()
        {
            Random random = new Random();
            int accountNumber = random.Next(100000, 999999); // Generates a number between 100000 and 999999
            string formattedAccountNumber = accountNumber.ToString("D6");
            return formattedAccountNumber;
        }

        public static string GenerateCode(this string code, string prefix, int length = 5)
        {
            string generateCode = string.Empty;
            if (string.IsNullOrWhiteSpace(code))
            {
                string defaultCode = "1";
                generateCode = $"{prefix}{defaultCode.PadLeft(length, '0')}";
                goto result;
            }

            //ode = code.Substring(1);
            code = code.Replace(prefix, "");
            int convertToInt = Convert.ToInt32(code) + 1;
            generateCode = $"{prefix}{convertToInt.ToString().PadLeft(length, '0')}";
        result:
            return generateCode;
        }
    }
}
