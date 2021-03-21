using System.Linq;
using Infrastructure.Domain.Core;

namespace Aspros.Project.User.Domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> GetIdentityById(long id);
        IQueryable<User> ValidUser(string userName, string password);
    }
}