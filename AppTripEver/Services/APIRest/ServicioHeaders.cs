using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppTripEver.Services.APIRest
{
    public class ServicioHeaders
    {
        #region Properties
        public Dictionary<string, string> Headers { get; set; }
        #endregion Properties

        #region Initialize
        public ServicioHeaders()
        {
            Headers = new Dictionary<string, string>();
            Headers.Add("ContentType", "application/json");
        }
        #endregion Initialize

        #region Métodos
        public HttpRequestMessage AgregarCabeceras(HttpRequestMessage requestMessage)
        {
            foreach(var h in Headers)
            {
                requestMessage.Headers.Add(h.Key, h.Value);
            }
            return requestMessage;
        }
        #endregion Métodos
    }
}
