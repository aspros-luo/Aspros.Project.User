using System.Threading.Tasks;
using Aspros.Project.User.Application.Dto;

namespace Aspros.Project.User.Application.Service
{
    public interface IUserService
    {
        Task<object> CreateUser(CreateUserModel model);
        Task<object> GetUser(long id);
    }
}