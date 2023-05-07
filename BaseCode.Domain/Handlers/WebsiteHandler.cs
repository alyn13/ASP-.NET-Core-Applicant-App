using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Constants = BaseCode.Data.Constants;

namespace BaseCode.Domain.Handlers
{
    public class WebsiteHandler
    {
        private readonly IWebsiteService _websiteService;

        public WebsiteHandler(IWebsiteService websiteService)
        {
            _websiteService = websiteService;
        }

        public IEnumerable<ValidationResult> CanAdd(Website website)
        {
            var validationErrors = new List<ValidationResult>();

            if (website != null)
            {
                if (_websiteService.IsWebsiteExists(website.WebsiteUrl))
                {
                    validationErrors.Add(new ValidationResult(Constants.Website.WebsiteUrlExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Website.WebsiteEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Website website)
        {
            var validationErrors = new List<ValidationResult>();

            if (website != null)
            {
                var dbWebsite = _websiteService.Find(website.WebsiteId);

                if (dbWebsite != null)
                {
                    if (!dbWebsite.WebsiteUrl.Equals(website.WebsiteUrl) && _websiteService.IsWebsiteExists(website.WebsiteUrl))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Website.WebsiteUrlExists));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(Constants.Website.WebsiteNotExist));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Website.WebsiteEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var website = _websiteService.Find(id);
            if (website == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Website.WebsiteNotExist));
            }

            return validationErrors;
        }
    }
}
