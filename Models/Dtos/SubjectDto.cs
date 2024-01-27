namespace StudySphere_API.Models.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int LecturerId { get; set; }
        public int Credit { get; set; } 
    }
}
