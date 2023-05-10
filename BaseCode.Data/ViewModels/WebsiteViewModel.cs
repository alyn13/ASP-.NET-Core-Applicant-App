using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class WebsiteViewModel
    {
        [Key]
        public int WebsiteId { get; set; }

        [JsonProperty("website_name")]
        public string WebsiteUrl { get; set; }
    }
}
