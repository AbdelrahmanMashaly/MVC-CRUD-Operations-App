using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace ProjectMVC.PL.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You Must Enter The name")]
        [MaxLength(50, ErrorMessage = "Name is Max 50 chars")]
        [MinLength(5, ErrorMessage = "Name is Min 50 chars")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "Age Must be between 22 to 60")]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be Like (123-Country) ")]
        public string Address { get; set; }
        
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public string ImageName { get; set; }

        public IFormFile Image { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime DateOfCreation { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
