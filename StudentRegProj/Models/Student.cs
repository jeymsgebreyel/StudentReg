using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentRegProj.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; } //Database Level

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Course { get; set; }
        [Required]
        [DisplayName("Year Level")]
        [Range(1,4)]
        public int YearLevel { get; set; }

        [DefaultValue(false)]
        public bool IsRegistered { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}

// ID
// Date Created
// Created By
// Date Modified
// Modified By
