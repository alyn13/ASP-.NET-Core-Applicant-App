using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class UserAdminViewModel
    {
        [JsonProperty(PropertyName = "adminID")]
        public string AdminID { get; set; }

        [JsonProperty(PropertyName = "uname")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "fname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; set; }

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }
    }
}

