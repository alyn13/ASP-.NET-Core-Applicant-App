using System.Linq;
using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public JobViewModel Find(int id)
        {
            JobViewModel jobViewModel = null;
            var job = _jobRepository.Find(id);

            if (job != null)
            {
                jobViewModel = _mapper.Map<JobViewModel>(job);
            }

            return jobViewModel;
        }

        public IQueryable<Job> RetrieveAll()
        {
            return _jobRepository.RetrieveAll();
        }

        public ListViewModel FindJobs(JobSearchViewModel searchModel)
        {
            return _jobRepository.FindJobs(searchModel);
        }

        public void Create(Job job)
        {
            _jobRepository.Create(job);
        }

        public void Update(Job job)
        {
            _jobRepository.Update(job);
        }

        public void Delete(Job job)
        {
            _jobRepository.Delete(job);
        }

        public void DeleteById(int id)
        {
            _jobRepository.DeleteById(id);
        }

        public bool IsJobExists(string name)
        {
            return _jobRepository.IsJobExists(name);
        }
    }
}
