using Infrastructure.Domain.Core;
using Infrastructure.Interfaces.Core.Event;

namespace Aspros.Project.User.Domain.Events
{
    public class UserIdentity : IAggregateRoot, IEvent
    {
        public long UserId { get; set; }
        public string RealName { get; set; }
        public string IdentityNo { get; set; }
    }
}