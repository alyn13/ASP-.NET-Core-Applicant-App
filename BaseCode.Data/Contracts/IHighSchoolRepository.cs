using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Models;

namespace BaseCode.Data.Contracts
{
    public interface IHighSchoolRepository
    {
        HighSchoolEducation Find(int app_id);
        IQueryable<HighSchoolEducation> RetrieveAll();
        // ListViewModel FindHighSchools(HighSchoolSearchViewModel searchModel);
        void Create(HighSchoolEducation highschool);
        void Update(HighSchoolEducation highschool);
        void Delete(HighSchoolEducation highschool);
        void DeleteById(int id);
        bool IsHighSchoolExists(string highschoolname);
        string GetSortKey(string sortBy);
    }
}
