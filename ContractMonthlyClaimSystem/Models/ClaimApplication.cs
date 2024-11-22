using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimApplication: ApprovalActivity
    {
        public int Id { get; set; }

        [Display(Name = "Lecturer Name")]
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        [Display(Name = "Num Of Claim Days")]
        public int NumOfDays { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Claim Duration")]
        public int DurationId { get; set; }
        public SystemCodeDetail Duration {  get; set; }

        [Display(Name = "Claim Type")]
        public int ClaimTypeId { get; set; }
        public ClaimType ClaimType { get; set; }

        public string? Attachment {  get; set; }

        [Display(Name = "Notes")]
        public string Description { get; set; }


        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }

        [Display(Name = "Approval Notes")]
        public string ApprovalNotes { get; set; }


    }
}
