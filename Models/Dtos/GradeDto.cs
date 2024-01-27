namespace StudySphere_API.Models.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public double Score {  get; set; }
        public int StudentId { get; set; }
        public int SubjectId {  get; set; }

    }
}
