using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySphere_API.Models.Entities;

namespace StudySphere_API.Mapping
{
    public class LecturerMap : IEntityTypeConfiguration<LecturerEntity>
    {
        public void Configure(EntityTypeBuilder<LecturerEntity> builder)
        {
            builder.HasKey(l => l.LecturerId);
            builder.HasMany(l => l.Subjects)
                   .WithOne(l => l.Lecturer);
        }
    }
}
