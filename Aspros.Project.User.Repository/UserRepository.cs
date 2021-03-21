using System;
using System.Linq;
using Aspros.Project.User.Domain.Repository;
using Infrastructure.Domain.Core;
using Infrastructure.Interfaces.Core.Interface;

namespace Aspros.Project.User.Repository
{
    public class UserRepository : BaseRepository<Domain.User>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Domain.User> GetIdentityById(long id)
        {
            return _entities.Where(x => x.Id == id);
        }

        public IQueryable<Domain.User> ValidUser(string userName, string password)
        {
            return _entities.Where(x => x.Name == userName && x.Password == password);
        }
    }
}