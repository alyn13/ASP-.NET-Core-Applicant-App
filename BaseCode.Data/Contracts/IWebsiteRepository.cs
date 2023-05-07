using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Models;

namespace BaseCode.Data.Contracts
{
    public interface IWebsiteRepository
    {
        Website Find(int web_id);
        IQueryable<Website> RetrieveAll();
        //ListViewModel FindWebsites(WebsiteSearchViewModel searchModel);
        void Create(Website website);
        void Update(Website website);
        void Delete(Website website);
        void DeleteById(int id);
        bool IsWebsiteExists(string websiteurl);
        string GetSortKey(string sortBy);
    }
}
