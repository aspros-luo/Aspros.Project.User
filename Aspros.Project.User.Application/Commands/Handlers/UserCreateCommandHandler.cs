using System.Threading;
using System.Threading.Tasks;
using Aspros.Project.User.Domain.Repository;
using Infrastructure.Interfaces.Core.Interface;
using MediatR;

namespace Aspros.Project.User.Application.Commands.Handlers
{
    public class UserCreateCommandHandler:IRequestHandler<UserCreateCommand,long>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserCreateCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.User(request.UserName, request.Password);
            await _unitOfWork.Add(user);
            var result = await _unitOfWork.CommitAsync();
            return result ? user.Id : 0;
        }
    }
}
