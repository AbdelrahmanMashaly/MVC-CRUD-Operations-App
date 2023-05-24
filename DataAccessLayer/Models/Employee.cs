using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage ="Name is Max 50 chars")]
        public string Name { get; set; }
        public int? Age { get; set; }
       
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        public string EmailAddress { get; set; }
        
        public string PhoneNumber { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateOfCreation { get; set; }
        public string ImageName { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
