using AutoMapper;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureMapper(IServiceCollection services)
        {
            var Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Applicant, ApplicantViewModel>()
                   .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website.Select(w => new WebsiteViewModel { WebsiteUrl = w.WebsiteUrl })))
                   .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill.Select(s => new SkillViewModel { SkillName = s.SkillName } )))
                   .ForMember(dest => dest.College, opt => opt.MapFrom(src => src.College.Select(c => new CollegeViewModel { CollegeEducId = c.CollegeEducId, CollegeName = c.CollegeName, Degree = c.Degree, YearStarted = c.YearStarted, YearEnded = c.YearEnded })))
                   .ForMember(dest => dest.HighSchool, opt => opt.MapFrom(src => new HighSchoolViewModel { HighSchoolEducId = src.HighSchool.HighSchoolEducId, HighSchoolName = src.HighSchool.HighSchoolName, YearStarted = src.HighSchool.YearStarted, YearEnded = src.HighSchool.YearEnded }))
                   .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => src.WorkExperience.Select(e => new ExperienceViewModel 
                     { ExperienceId = e.ExperienceId, IsFirstJob = e.IsFirstJob, CompanyName = e.CompanyName, Position = e.Position, Street = e.Street,
                      Barangay = e.Barangay, City = e.City, Province = e.Province, ZipCode = e.ZipCode, Country = e.Country, CurrentlyWorking = e.CurrentlyWorking,
                       TimeStarted = e.TimeStarted, TimeEnded = e.TimeEnded })))
                   ;
                cfg.CreateMap<ApplicantViewModel, Applicant>()
                   .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website.Select(w => new Website { WebsiteUrl = w.WebsiteUrl })))
                   .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill.Select(s => new Skill { SkillName = s.SkillName })))
                   .ForMember(dest => dest.College, opt => opt.MapFrom(src => src.College.Select(c => new CollegeEducation { CollegeName = c.CollegeName, Degree = c.Degree, YearStarted = c.YearStarted, YearEnded = c.YearEnded })))
                   .ForMember(dest => dest.HighSchool, opt => opt.MapFrom(src => new HighSchoolEducation { HighSchoolEducId = src.HighSchool.HighSchoolEducId, HighSchoolName = src.HighSchool.HighSchoolName, YearStarted = src.HighSchool.YearStarted, YearEnded = src.HighSchool.YearEnded }))
                   .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => src.WorkExperience.Select(e => new Experience
                   {
                       ExperienceId = e.ExperienceId,
                       IsFirstJob = e.IsFirstJob,
                       CompanyName = e.CompanyName,
                       Position = e.Position,
                       Street = e.Street,
                       Barangay = e.Barangay,
                       City = e.City,
                       Province = e.Province,
                       ZipCode = e.ZipCode,
                       Country = e.Country,
                       CurrentlyWorking = e.CurrentlyWorking,
                       TimeStarted = e.TimeStarted,
                       TimeEnded = e.TimeEnded
                   })))
                   ;
                
               
                cfg.CreateMap<Website, WebsiteViewModel>();
                cfg.CreateMap<WebsiteViewModel, Website>();
                cfg.CreateMap<Skill, SkillViewModel>();
                cfg.CreateMap<SkillViewModel, Skill>();
                cfg.CreateMap<CollegeEducation, CollegeViewModel>();
                cfg.CreateMap<CollegeViewModel, CollegeEducation>();
                cfg.CreateMap<HighSchoolEducation, HighSchoolViewModel>();
                cfg.CreateMap<HighSchoolViewModel, HighSchoolEducation>();
                cfg.CreateMap<Experience, ExperienceViewModel>();
                cfg.CreateMap<ExperienceViewModel, Experience>();

                cfg.CreateMap<Job, JobViewModel>();
                cfg.CreateMap<JobViewModel, Job>();

            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}

