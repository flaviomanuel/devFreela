using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DevFreela.Infrastructure.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {

           builder
            .HasKey(p => p.Id);

            builder
                .HasOne(x => x.Freelancer)
                .WithMany(x => x.FreelanceProjects)
                .HasForeignKey(x => x.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasOne(x => x.Client)
              .WithMany(x => x.OwnedProjects)
              .HasForeignKey(x => x.IdClient)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
