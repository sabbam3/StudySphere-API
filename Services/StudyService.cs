using StudySphere_API.Abstractions;
using StudySphere_API.Models.Authentication;
using StudySphere_API.Models.Dtos;
using StudySphere_API.Models.Entities;
using StudySphere_API.Models.Requests;
using StudySphere_API.Repositories;

namespace StudySphere_API.Services
{
    public class StudyService : IStudyService
    {
        private readonly IStudyRepository _repository;
        public StudyService(IStudyRepository repository)
        {
            _repository = repository;
        }
        public async Task AddStudentAsync(Register user, UserEntity userEntity)
        {
            StudentEntity entity = new StudentEntity();
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.Email = user.Email;
          //calkea gasatani  entity.Grade = GradeCalculator(user.Email);
            entity.User = userEntity;
            await _repository.AddStudentAsync(entity);
        }
        public async Task AddLecturerAsync(Register user, UserEntity userEntity)
        {
            LecturerEntity entity = new LecturerEntity();
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.Email = user.Email;
            entity.User = userEntity;
            await _repository.AddLecturerAsync(entity);
        }

        public async Task AddSubjectAsync(SubjectRequest request)
        {
          SubjectEntity entity = new SubjectEntity();
           entity.LecturerId = request.LecturerId;
           entity.Credit = request.Credit;
           entity.Name = request.Name;
           await _repository.AddSubjectAsync(entity);
           await _repository.SaveChangesAsync();
        }

        public async Task AddGradeAsync(GradeRequest request)
        {
           GradeEntity entity = new GradeEntity();
           entity.StudentId = request.StudentId;
           entity.SubjectId = request.SubjectId;
           await _repository.AddGradeAsync(entity);
           await _repository.SaveChangesAsync();
        }
        public async Task ChooseSubjectAsync(int subjectId, string email)
        {
            GradeEntity entity = new GradeEntity();
            entity.StudentId = await GetStudentIdAsync(email);
            entity.SubjectId = subjectId;
            await _repository.AddGradeAsync(entity);
            await _repository.SaveChangesAsync();
        }
        public async Task AddScoreAsync(string email, int studentId, double score)
        {
            var lecturerId = await _repository.GetLecturerIdAsync(email);
            var gradeList = await _repository.GetStudentsGradeAsync(lecturerId);
            if (gradeList != null)
            {
                var grade = gradeList.Where(s => s.StudentId == studentId).FirstOrDefault();
                if (grade != null)
                grade.Score = score;
                await _repository.SaveChangesAsync();
            }

        }
        public async Task<int> GetStudentIdAsync(string email)
        {
            return await _repository.GetStudentIdAsync(email);
        }
        public async Task<List<GradeDto>?> GetStudentsGradeAsync(string email)
        {
            var lecturerId = await _repository.GetLecturerIdAsync(email);
            var grades = await _repository.GetStudentsGradeAsync(lecturerId);
            var gradeList = grades.Select(s => new GradeDto
            {
                Id = s.Id,
                StudentId = s.StudentId,
                SubjectId = s.SubjectId,
                Score = s.Score
            }).ToList();
            return gradeList;
        }
        public async Task<List<SubjectDto>?> GetYourSubjectsAsync(string email)
        {
            var studentId = await _repository.GetStudentIdAsync(email);
            var subjects = await _repository.GetYourSubjectsAsync(studentId);
            var subjectList = subjects.Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                LecturerId = s.LecturerId,
                Credit = s.Credit
            }).ToList();
            return subjectList;
        }
        public async Task<List<GradeDto>?> GetStudentGradesAsync(string email)
        {
            var studentId = await _repository.GetStudentIdAsync(email);
            var grades = await _repository.GetStudentGradesAsync(studentId);
            var gradeList = grades.Select(s => new GradeDto
            {
                Id = s.Id,
                StudentId = s.StudentId,
                SubjectId = s.SubjectId,
                Score = s.Score
            }).ToList();
            return gradeList;
        }
    }

}
