using StudySphere_API.Models.Authentication;
using StudySphere_API.Models.Dtos;
using StudySphere_API.Models.Entities;
using StudySphere_API.Models.Requests;

namespace StudySphere_API.Abstractions
{
    public interface IStudyService
    {
        Task AddStudentAsync(Register user, UserEntity userEntity);
        Task AddLecturerAsync(Register user, UserEntity userEntity);
        Task AddSubjectAsync(SubjectRequest request);
        Task AddGradeAsync(GradeRequest request);
        Task AddScoreAsync(string email, int studentId, double score);
        Task ChooseSubjectAsync(int subjectId, string email);
        Task<int> GetStudentIdAsync(string email);
        Task<List<GradeDto>?> GetStudentsGradeAsync(string email);
        Task<List<SubjectDto>?> GetYourSubjectsAsync(string email);
        Task<List<GradeDto>?> GetStudentGradesAsync(string email);
    }
}
