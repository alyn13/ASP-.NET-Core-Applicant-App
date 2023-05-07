using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using BaseCode.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Repositories;
using static BaseCode.Data.Constants;
using BaseCode.Data.Models;

namespace BaseCode.Domain.Services
{
    public class HighSchoolService : IHighSchoolService
    {
        private readonly IHighSchoolRepository _highschoolRepository;
        private readonly IMapper _mapper;

        public HighSchoolService(IHighSchoolRepository highschoolRepository, IMapper mapper)
        {
            _highschoolRepository = highschoolRepository;
            _mapper = mapper;
        }

        public HighSchoolViewModel Find(int id)
        {
            HighSchoolViewModel highschoolViewModel = null;
            var highschool = _highschoolRepository.Find(id);

            if (highschool != null)
            {
                highschoolViewModel = _mapper.Map<HighSchoolViewModel>(highschool);

            }

            return highschoolViewModel;
        }

        public IQueryable<HighSchoolEducation> RetrieveAll()
        {
            return _highschoolRepository.RetrieveAll();
        }

       /* public ListViewModel FindHighSchools(HighSchoolSearchViewModel searchModel)
        {
            return _highschoolRepository.FindHighSchools(searchModel);
        }*/

        public void Create(HighSchoolEducation highschool)
        {
            _highschoolRepository.Create(highschool);
        }

        public void Update(HighSchoolEducation highschool)
        {
            _highschoolRepository.Update(highschool);
        }

        public void Delete(HighSchoolEducation highschool)
        {
            _highschoolRepository.Delete(highschool);
        }

        public void DeleteById(int id)
        {
            _highschoolRepository.DeleteById(id);
        }

        public bool IsHighSchoolExists(string highschoolname)
        {
            return _highschoolRepository.IsHighSchoolExists(highschoolname);
        }
    }
}
