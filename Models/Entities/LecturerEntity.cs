namespace StudySphere_API.Models.Entities
{
    public class LecturerEntity
    {
        public int LecturerId {  get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserEntity User { get; set; } = null!;
        public List<SubjectEntity> Subjects { get; set; }
        public LecturerEntity()
        {
            Subjects = new List<SubjectEntity>();
        }
    }
}
