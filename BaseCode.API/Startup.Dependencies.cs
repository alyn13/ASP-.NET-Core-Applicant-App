﻿using BaseCode.API.Authentication;
using BaseCode.Data;
using BaseCode.Data.Contracts;
using BaseCode.Data.Repositories;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureDependencies(IServiceCollection services)
        {            
            // Common
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ClaimsProvider, ClaimsProvider>();

            // Services
            services.AddScoped<IClientService, ClientService>();            
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IUserService, UserService>();

            // Repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
