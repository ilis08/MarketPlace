using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Messages
{
    public class ResponseMessage
    {
        [JsonProperty(PropertyName = "code")]
        public object Code { get; set; }
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public object Body { get; set; }
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public object Error { get; set; }
    }
}
