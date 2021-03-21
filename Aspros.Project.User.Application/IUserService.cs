using System.Threading.Tasks;
using Aspros.Project.User.Application.Dto;
using Aspros.Project.User.Application.Service;
using Aspros.Project.User.Domain.Repository;
using Infrastructure.Interfaces.Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Aspros.Project.User.Application
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<object> CreateUser(CreateUserModel model)
        {
            var user = new Domain.User(model.userName, model.password);
            await _unitOfWork.Add(user);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<object> GetUser(long id)
        {
            var user = await _userRepository.GetIdentityById(id).FirstOrDefaultAsync();
            return user;
        }
    }
}