using System;
using System.Threading.Tasks;
using Aspros.Project.User.Domain.Events;
using Infrastructure.Interfaces.Core.Event;
using Infrastructure.Interfaces.Core.Interface;
using Infrastructure.Ioc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Aspros.Project.User.Domain.EventHandlers
{
    public class UserEventHandler : IEventHandler<UserIdentity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserEventHandler(IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = httpContextAccessor.HttpContext is null ? ServiceLocator.Instance.GetService<IUnitOfWork>() : httpContextAccessor.HttpContext.RequestServices.GetService<IUnitOfWork>();
        }

        public async Task Handle(UserIdentity @event)
        {
            await _unitOfWork.Add(@event);
            await _unitOfWork.CommitAsync();
        }
    }
}