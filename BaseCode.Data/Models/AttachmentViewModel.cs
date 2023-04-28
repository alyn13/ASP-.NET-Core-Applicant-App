using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileJO.Data.ViewModels.Common
{
    public class AttachmentViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "attachmentType")]
        public string AttachmentType { get; set; }
    }
}
