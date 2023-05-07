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
                /*cfg.CreateMap<Applicant, ApplicantViewModel>()
                    .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                    //.ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website.Select(w => new WebsiteViewModel { WebsiteUrl = w.WebsiteUrl })))
                    .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill.Select(s => s.SkillName)))
                   // .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill.Select(s => new SkillViewModel { SkillName = s.SkillName } )))
                    .ForMember(dest => dest.College, opt => opt.MapFrom(src => src.College.Select(c => c.CollegeName)))
                    .ForMember(dest => dest.HighSchool, opt => opt.MapFrom(src => src.HighSchool.HighSchoolName))
                    .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => src.WorkExperience.Select(e => e.Position)))
                   
                ;
                cfg.CreateMap<ApplicantViewModel, Applicant>()
                    //.ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website.Select(w => new Website { WebsiteUrl = w.WebsiteUrl })))
                    //.ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website.Select(w => new Website { WebsiteUrl = w})))
                    .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill.Select(s => new Skill { SkillName = s })))
                    .ForMember(dest => dest.College, opt => opt.MapFrom(src => src.College.Select(c => new CollegeEducation { CollegeName = c.CollegeName })))
                    .ForMember(dest => dest.HighSchool, opt => opt.MapFrom(src => new HighSchoolEducation { HighSchoolName = src.HighSchool.HighSchoolName} ))
                    .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => src.WorkExperience.Select(e => new Experience { Position = e.Position })))*/

                cfg.CreateMap<Applicant, ApplicantViewModel>();
                /*.ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website.Select(w => new WebsiteViewModel { WebsiteUrl = w.WebsiteUrl })))
                .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill.Select(s => s.SkillName)))
                .ForMember(dest => dest.College, opt => opt.MapFrom(src => src.College.Select(c => new CollegeViewModel { CollegeName = c.CollegeName, Degree = c.Degree, YearStarted = c.YearStarted, YearEnded = c.YearEnded })))
                .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => src.WorkExperience.Select(e => new ExperienceViewModel { Position = e.Position })))
                .ForMember(dest => dest.HighSchool, opt => opt.MapFrom(src => new HighSchoolViewModel { HighSchoolName = src.HighSchool.HighSchoolName }));*/

                cfg.CreateMap<ApplicantViewModel, Applicant>();
                   /*.ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website.Select(w => new Website { WebsiteUrl = w.WebsiteUrl })))
                   .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill.Select(s => new Skill { SkillName = s })))
                   .ForMember(dest => dest.College, opt => opt.MapFrom(src => src.College.Select(c => new CollegeEducation { CollegeName = c.CollegeName })))
                   .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => src.WorkExperience.Select(e => new Experience { Position = e.Position })))
                   .ForMember(dest => dest.HighSchool, opt => opt.MapFrom(src => new HighSchoolEducation { HighSchoolName = src.HighSchool.HighSchoolName }));*/
                
               
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

/*.ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website.Select(w => w.WebsiteUrl)))
                   .ForMember(dest => dest.Skill, opt => opt.MapFrom(src => src.Skill.Select(s => s.SkillName)))
                   .ForMember(dest => dest.College, opt => opt.MapFrom(src => src.College.Select(c => c.CollegeName)))
                   .ForMember(dest => dest.HighSchool, opt => opt.MapFrom(src => src.HighSchool.HighSchoolName))
                   .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => src.WorkExperience.Select(e => e.Position)))*/