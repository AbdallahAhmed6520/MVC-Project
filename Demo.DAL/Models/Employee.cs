using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length Is 50")]
        [MinLength(5, ErrorMessage = "Min Length Is 5")]
        public string Name { get; set; }

        [Range(22, 35, ErrorMessage = "Age Must Be In Range From 22 To 35")]
        public int? Age { get; set; }

        //[RegularExpression("^[0,9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
        //    ErrorMessage = "Address Must Be  Like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        //FK Optional => OnDelete : Restricted (مينفعش امسح قسم موجود فيه موظفين)
        //FK Required => OnDelete : Cascade (لما امسح قسم هيمسح الموظفين معاه)

        [InverseProperty("Employees")]

        public Department Department { get; set; }
    }
}
