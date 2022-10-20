using Paycompute.Entity;
using System.ComponentModel.DataAnnotations;

namespace Paycompute.Models
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee Number is required"),
           RegularExpression(@"^[A-Z]{3,3}[0-9]{3}$")]
        public string EmployeeNo { get; set; }
        [Required(ErrorMessage = "First Name is required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name = "Middle Name")]
        public string MidleName { get; set; }
        [Required(ErrorMessage = "Last Name is required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "First Name")]
        public string LastName { get; set; }
        [Required, MaxLength(200)]
        public string FullName { 
        
            get
            {
                return FirstName + (string.IsNullOrEmpty(MidleName)? " " :(" " + (char?)MidleName[0] + ".").ToUpper()) + LastName;
            }

        }
        
        public string Gender { get; set; }
        [Display(Name = "Photo")]
        public IFormFile ImageUrl { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Job Role is required"), StringLength(100)]
        public string Designation { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(50), Display(Name = " NI NO.")]
        [RegularExpression(@"[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$")]
        public string NationalInsuranceNo { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Student Loan")]
        public StudentLoan StudentLoan { get; set; }
        [Display(Name = "Union Member")]
        public UnionMember UnionMember { get; set; }
        [Required, StringLength(150)]
        public string Address { get; set; }
        [Required, StringLength(50)]
        public string City { get; set; }
        [Required, StringLength(50), Display(Name = "Post Code")]
        public string Postcode { get; set; }

    }
}
