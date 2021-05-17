using MediatR;

namespace Aspros.Project.User.Application.Commands
{
    public class UserCreateCommand : IRequest<long>
    {
        public string IdNo;
        public string Password;
        public string RealName;
        public string UserName;
    }
}