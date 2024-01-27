using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySphere_API.Models.Entities;

namespace StudySphere_API.Mapping
{
    public class SubjectMap : IEntityTypeConfiguration<SubjectEntity>
    {
        public void Configure(EntityTypeBuilder<SubjectEntity> builder)
        {
            builder.HasMany(s => s.StudentGrades)
                   .WithOne(s => s.Subject)
                   .HasForeignKey(s => s.SubjectId)
                   .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
