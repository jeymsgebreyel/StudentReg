using StudentRegCore;

namespace StudentPortal.ViewModel
{
    public class StudentViewModel
    {
        //LIST OF STUDENTS
        public List<StudentDTO> StudentList { get; set; }

        //REGISTRATION OF NEW STUDENT
        public StudentDTO NewStudent { get; set; }

        //VIEW OR UPDATE OF EXISTING STUDENT
        public StudentDTO Student { get; set; }
    }
}
