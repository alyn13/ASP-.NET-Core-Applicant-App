using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IJobRepository
    {
        Job Find(int id);
        IQueryable<Job> RetrieveAll();
        ListViewModel FindJobs(JobSearchViewModel searchModel);
        void Create(Job job);
        void Update(Job job);
        void Delete(Job job);
        void DeleteById(int id);
        bool IsJobExists(string name);    
        string GetSortKey(string sortBy);
    }
}
