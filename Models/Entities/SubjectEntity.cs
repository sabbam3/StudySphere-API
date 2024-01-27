namespace StudySphere_API.Models.Entities
{
    public class SubjectEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int LecturerId {  get; set; }
        public int Credit { get; set; }
        public LecturerEntity Lecturer { get; set; } = null!;
        public List<GradeEntity> StudentGrades { get; set; }
        public SubjectEntity()
        {
            StudentGrades = new List<GradeEntity>();
        }
    }
}
