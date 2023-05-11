using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class UserUpdateViewModel
    {
        [Required]
        [JsonProperty(PropertyName = "adminID")]
        public string AdminID { get; set; }

        
        [JsonProperty(PropertyName = "uname")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [JsonProperty(PropertyName = "email")]
        public string EmailAddress { get; set; }
    }
}
