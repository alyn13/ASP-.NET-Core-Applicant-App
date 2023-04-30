using AutoMapper;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureMapper(IServiceCollection services)
        {
            var Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Applicant, ApplicantViewModel>();
                cfg.CreateMap<ApplicantViewModel, Applicant>();
                cfg.CreateMap<Job, JobViewModel>();
                cfg.CreateMap<JobViewModel, Job>();
            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}