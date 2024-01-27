using Microsoft.AspNetCore.Authorization;

namespace StudySphere_API.Models.Entities
{
    public class GradeEntity
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public int SubjectId { get; set; }  
        public int StudentId { get; set; }
        public SubjectEntity Subject { get; set; } = null!;
        public StudentEntity Student { get; set; } = null!;
    }
}
