using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegCore
{
    public class StudentDTO
    {

        public int Id { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Course { get; set; }
        [Required]
        public int YearLevel { get; set; }

        
        public bool IsRegistered { get; set; }
    }
}
