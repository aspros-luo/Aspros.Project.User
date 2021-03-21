using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aspros.Project.User.Domain;
namespace Aspros.Project.User.Infrastructure.Repository.Mappings
{
    public class UserMap : ModelBuilderExtenions.EntityMappingConfiguration<Domain.User>
    {
        public override void Map(EntityTypeBuilder<Domain.User> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("users");
            entityTypeBuilder.HasKey(i => i.Id);
            entityTypeBuilder.Property(i => i.Id).ValueGeneratedOnAdd();
            entityTypeBuilder.Property(i => i.Name).HasColumnName("name");
            entityTypeBuilder.Property(i => i.Password);
            entityTypeBuilder.Ignore(i => i.UserIdentity);
        }
    }
}