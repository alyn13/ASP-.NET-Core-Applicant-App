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

        [JsonProperty(PropertyName = "fname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Address { get; set; }
    }
}
