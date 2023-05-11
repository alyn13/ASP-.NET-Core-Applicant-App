using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        [JsonProperty(PropertyName = "uname")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
