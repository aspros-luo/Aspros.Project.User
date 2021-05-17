using System.Reflection;
using Infrastructure.Interfaces.Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Aspros.Project.User.Infrastructure.Repository
{
    public class UserDbContext : DbContext, IDbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
        }
    }
}