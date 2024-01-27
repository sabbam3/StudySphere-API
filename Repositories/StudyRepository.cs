using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using StudySphere_API.Abstractions;
using StudySphere_API.Db;
using StudySphere_API.Models.Entities;

namespace StudySphere_API.Repositories
{
    public class StudyRepository : IStudyRepository
    {
        private readonly AppDbContext _db;
        public StudyRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task SaveChangesAsync()
        {
           await _db.SaveChangesAsync();
        }
        public async Task AddStudentAsync(StudentEntity entity)
        {
            await _db.Students.AddAsync(entity);
        }
        public async Task AddLecturerAsync(LecturerEntity entity)
        {
            await _db.Lecturers.AddAsync(entity);
        }
        public async Task AddSubjectAsync(SubjectEntity entity)
        {
            await _db.Subjects.AddAsync(entity);
        }
        public async Task AddGradeAsync(GradeEntity grade)
        {
            await _db.Grades.AddAsync(grade);
        }
        public async Task<List<GradeEntity>?> GetStudentsGradeAsync(int lecturerId)
        {
            return await _db.Subjects
                        .Where(l => l.LecturerId == lecturerId)
                        .SelectMany(g => g.StudentGrades)
                        .Include(student => student.Student)
                        .Include(subject => subject.Subject)
                        .ToListAsync();
        }
        public async Task<int> GetStudentIdAsync(string email)
        {
            return await _db.Students
                        .Where(s => s.Email == email)
                        .Select(s => s.StudentId)
                        .FirstOrDefaultAsync();
        }
        public async Task<int> GetLecturerIdAsync(string email)
        {
            return await _db.Lecturers
                        .Where(l => l.Email == email)
                        .Select(l => l.LecturerId)
                        .FirstOrDefaultAsync();
        }
        public async Task<List<SubjectEntity>?> GetYourSubjectsAsync(int studentId)
        {
            return await _db.Grades
                         .Where(s => s.StudentId == studentId)
                         .Select(s => s.Subject).ToListAsync();
        }
        public async Task<List<GradeEntity>?> GetStudentGradesAsync(int studentId)
        {
            return await _db.Grades
                        .Where(s => s.StudentId == studentId)
                        .ToListAsync();
        }
    }
}
