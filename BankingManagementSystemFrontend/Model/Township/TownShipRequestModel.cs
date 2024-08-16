namespace BankingManagementSystemFrontend.Model.Township
{
    public class TownShipRequestModel
    {
        public int TownshipId { get; set; }

        public string? TownshipCode { get; set; } = null!;

        public string TownshipName { get; set; } = null!;

        public string StateCode { get; set; } = null!;
    }
}
