using System.Threading;
using System.Threading.Tasks;
using Aspros.Project.User.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aspros.Project.User.Application.Queries.Handlers
{
    public class UserGetQueryHandler:IRequestHandler<UserGetQuery,Domain.User>
    {
        private readonly IUserRepository _userRepository;

        public UserGetQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Domain.User> Handle(UserGetQuery request, CancellationToken cancellationToken)
        {
            var user =await  _userRepository.GetIdentityById(request.UserId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return user;
        }
    }
}