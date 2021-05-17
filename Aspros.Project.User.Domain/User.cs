using System;
using System.Threading.Tasks;
using Aspros.Project.User.Domain.Events;
using AutoMapper;
using Infrastructure.Domain.Core;
using Infrastructure.Interfaces.Core.Event;
using Infrastructure.Ioc.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Aspros.Project.User.Domain
{
    public class User : IAggregateRoot
    {
        private IEventBus _eventBus;

        public User()
        {
        }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public long Id { get; set; }

        public string Name { get; }

        public string Password { get; }

        public virtual UserIdentity UserIdentity { get; set; }

        public async Task Identity()
        {
            try
            {
                var userIdentity = Mapper.Map<UserIdentity>(this);

                _eventBus = ServiceLocator.Instance.GetService<IEventBus>();
                await _eventBus.Publish(userIdentity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}