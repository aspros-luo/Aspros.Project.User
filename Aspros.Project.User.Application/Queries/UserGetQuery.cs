using Aspros.Project.User.Application.Dto;
using MediatR;

namespace Aspros.Project.User.Application.Queries
{
    public class UserGetQuery:IRequest<Domain.User>
    {
        public long UserId { get; set; }
    }
}