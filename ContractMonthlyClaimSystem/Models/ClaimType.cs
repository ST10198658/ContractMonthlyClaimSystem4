namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimType: UserActivity
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
