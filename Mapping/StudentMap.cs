using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySphere_API.Models.Entities;

namespace StudySphere_API.Mapping
{
    public class StudentMap : IEntityTypeConfiguration<StudentEntity>
    {
        public void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
               builder.HasKey(s => s.StudentId);
               builder.HasMany(s => s.Grades)
                      .WithOne(s => s.Student)
                      .HasForeignKey(s => s.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
