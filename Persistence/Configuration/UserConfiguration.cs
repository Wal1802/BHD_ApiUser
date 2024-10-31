using BHD.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHD.Persistence.Configuration
{
    public class UserConfiguration : EntityConfiguration<User>, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureEntity(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(264);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(1024);
            builder.Property(x => x.LastLogin).IsRequired();
            builder.Property(x => x.Token).HasMaxLength(1024);
            builder.Property(x => x.IsActive).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
