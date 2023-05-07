using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Domain.Contracts
{
    public interface IHighSchoolService
    {
        HighSchoolViewModel Find(int app_id);
        IQueryable<HighSchoolEducation> RetrieveAll();
        // ListViewModel FindHighSchools(HighSchoolSearchViewModel searchModel);
        void Create(HighSchoolEducation highschool);
        void Update(HighSchoolEducation highschool);
        void Delete(HighSchoolEducation highschool);
        void DeleteById(int id);
        bool IsHighSchoolExists(string highschoolname);
    }
}
