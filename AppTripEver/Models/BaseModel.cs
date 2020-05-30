using System;
using System.Collections.Generic;
using System.Text;
using AppTripEver.Services.Propagation;
using Newtonsoft.Json;

namespace AppTripEver.Models
{
    public class BaseModel : NotificationObject
    {
        #region Properties
        [JsonIgnore]
        public long ID { get; set; }
        #endregion Properties
    }
}
