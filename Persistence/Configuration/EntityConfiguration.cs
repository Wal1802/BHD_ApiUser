using BHD.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BHD.Persistence.Configuration
{
    public abstract class EntityConfiguration<T> where T : Entity
    {
        public void ConfigureEntity(EntityTypeBuilder<T> builder)
        {
           
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ModifiedAt).IsRequired();
            
            builder.HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
    }
}
