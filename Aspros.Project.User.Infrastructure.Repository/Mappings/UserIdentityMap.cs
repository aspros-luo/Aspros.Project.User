using Aspros.Project.User.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aspros.Project.User.Infrastructure.Repository.Mappings
{
    public class UserIdentityMap: ModelBuilderExtenions.EntityMappingConfiguration<UserIdentity>
    {
        public override void Map(EntityTypeBuilder<UserIdentity> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("identities");
            entityTypeBuilder.HasKey(i => i.UserId);
            entityTypeBuilder.Property(i => i.UserId).HasColumnName("user_id");
            entityTypeBuilder.Property(i => i.RealName).HasColumnName("real_name");
            entityTypeBuilder.Property(i => i.IdentityNo).HasColumnName("identity_no");
        }
    }
}