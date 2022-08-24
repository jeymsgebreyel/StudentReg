using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentRegProj.Models;

namespace StudentRegProj.Data
{
    public class StudentRegProjContext : DbContext
    {
        public StudentRegProjContext (DbContextOptions<StudentRegProjContext> options)
            : base(options)
        {
        }

        public DbSet<StudentRegProj.Models.Student> Student { get; set; } = default!;
    }
}
