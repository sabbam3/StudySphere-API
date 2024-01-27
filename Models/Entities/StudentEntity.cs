namespace StudySphere_API.Models.Entities
{
    public class StudentEntity
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Grade { get; set; }
        public double GPA { get; set; }
        public UserEntity User { get; set; } = null!;
        public List<GradeEntity> Grades { get; set; }
        public StudentEntity()
        {
            Grades = new List<GradeEntity>();
        }

    }
}
