namespace ContractMonthlyClaimSystem.Models
{
    public class Lecturer: UserActivity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public int PhoneNum { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }
    }
}
