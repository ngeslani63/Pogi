using Pogi.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pogi.Entities
{
    public class Member
    {
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
        [DefaultValue(0)]
        public int GhinNumber { get; set; }
        [Display(Name = "Current Handicap")]
        [DefaultValue(0)]
        public int CurrHandicap { get; set; }
        
    }
}
