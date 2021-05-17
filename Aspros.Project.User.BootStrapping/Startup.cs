using System;
using Aspros.Project.User.Application;
using Aspros.Project.User.Application.Service;
using Aspros.Project.User.Domain.Repository;
using Aspros.Project.User.Infrastructure.Repository;
using Aspros.Project.User.Repository;
using Infrastructure.Interfaces.Core;
using Infrastructure.Interfaces.Core.Event;
using Infrastructure.Interfaces.Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
// using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Aspros.Project.User.BootStrapping
{
    public static class Startup
    {
        public static void Configure(this IServiceCollection service, string connectionString, string rabbitMq)
        {
            //            service.AddEntityFrameworkSqlServer().AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));

            service.AddDbContext<UserDbContext>(options => options.UseMySQL(connectionString));

            service.AddCap(x =>
            {
                x.UseEntityFramework<UserDbContext>();
                x.UseMySql(connectionString);
                x.UseRabbitMQ(rabbitMq);
            });

            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient<IDbContext, UserDbContext>();
            service.AddTransient<IWorkContext, WorkContext>();
            service.AddTransient<IEventBus, EventBus>();

            service.AddScoped<IUserRepository, UserRepository>();
            // service.AddScoped<IOrgMerchantRepository, OrgMerchantRepository>();
            service.AddScoped<IUserService, UserService>();

//             Mapper.Initialize(cfg =>
//             {
//                 cfg.CreateMap<OrgMerchant, OrgMerchantDTO>()
// //                    .ForMember(dest => dest.org, opt => opt.MapFrom(src => src.UserId))
//                     ;
//                 cfg.CreateMap<Domain.Org, OrgDTO>()
//                     .ForMember(dest => dest.OrgType, opt => opt.MapFrom(src => src.OrgType.GetKeyValue()))
//                     .ForMember(dest => dest.SexType, opt => opt.MapFrom(src => src.SexType.GetKeyValue()))
//                     .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetKeyValue()))
//                     ;
//
//                 cfg.CreateMap<Domain.Org, OrgDetailDTO>()
//                     .ForMember(dest => dest.OrgType, opt => opt.MapFrom(src => src.OrgType.GetKeyValue()))
//                     .ForMember(dest => dest.SexType, opt => opt.MapFrom(src => src.SexType.GetKeyValue()))
//                     .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetKeyValue()))
//                     ;
//
//                 cfg.CreateMap<Domain.Org, SimpleOrgDTO>();
//             });
        }
    }
}