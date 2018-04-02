using Pogi.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pogi.Entities
{
    public class Member
    {
        public Member()
        {
            Phone1stType = PhoneType.Mobile;
            State = StateCode.NJ;
            CurrHandicap = (float)0.00;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        [Display(Name = "First Name")]
        [Required, MaxLength(40)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required, MaxLength(40)]
        public string LastName { get; set; }
        [Phone]
        [Display(Name = "Primary Phone")]
        public string Phone1st { get; set; }
        [Display(Name = "Primary Phone Type")]
        public PhoneType Phone1stType { get; set; }
        [Phone]
        [Display(Name = "Second Phone")]
        public string Phone2nd { get; set; }
        [Display(Name = "Second Phone Type")]
        public PhoneType Phone2ndType { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Primary Email Address")]
        public string EmailAddr1st { get; set; }
        [EmailAddress]
        [Display(Name = "Alternate Email Address")]
        public string EmailAddr2nd { get; set; }
        [Display(Name = "Record Status")]
        [DefaultValue(RecordState.Active)]
        public RecordState RecordStatus { get; set; }
        [Display(Name = "Member Status")]
        [DefaultValue(MemberState.Member)]
        public MemberState MemberStatus { get; set; }
        public string Profession { get; set; }
        [Display(Name = "Marital Status")]
        public MaritalState MaritalStatus { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [DefaultValue(StateCode.NJ)]
        public StateCode State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        [Display(Name = "GHIN")]
        public int GhinNumber { get; set; }
        [Display(Name = "Current Handicap")]
        public float  CurrHandicap { get; set; }
        [Display(Name = "Root Admin")]
        public bool RoleAdminRoot { get; set; }
        [Display(Name = "User Admin")]
        public bool RoleAdminUser { get; set; }
        [Display(Name = "Course Admin")]
        public bool RoleAdminCourse { get; set; }
        [Display(Name = "TeeTime Admin")]
        public bool RoleAdminTeeTime { get; set; }
        [Display(Name = "Score Admin")]
        public bool RoleAdminScore { get; set; }

        [Display(Name = "Tour Admin")]
        public bool RoleAdminTour { get; set; }
        [Display(Name = "About Me")]
        public string AboutMe { get; set; }
        [Display(Name = "Upload Photo")]
        public string ProfileFileName { get; set; }

    }
}
