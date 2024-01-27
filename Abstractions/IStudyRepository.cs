using StudySphere_API.Models.Entities;

namespace StudySphere_API.Abstractions
{
    public interface IStudyRepository
    {
        Task SaveChangesAsync();
        Task AddLecturerAsync(LecturerEntity entity);
        Task AddStudentAsync(StudentEntity entity);
        Task AddSubjectAsync(SubjectEntity entity);
        Task AddGradeAsync(GradeEntity grade);
        Task<int> GetStudentIdAsync(string email);
        Task<int> GetLecturerIdAsync(string email);
        Task<List<GradeEntity>?> GetStudentsGradeAsync(int lecturerId);
        Task<List<SubjectEntity>?> GetYourSubjectsAsync(int studentId);
        Task<List<GradeEntity>?> GetStudentGradesAsync(int studentId);
    }
}
