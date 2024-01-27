namespace StudySphere_API.Models.Requests
{
    public class SubjectRequest
    {
        public string? Name { get; set; }
        public int LecturerId { get; set; }
        public int Credit { get; set; }
    }
}
