using AppTripEver.Models.AuxiliarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTripEver.Services.APIRest
{
    public abstract class Request<T>
    {
        #region Properties
        protected string Url { get; set; }
        protected string Verbo { get; set; }
        protected string UrlParameters { get; set; }

        private static ServicioHeaders servicioHeaders;
        #endregion Properties

        #region Getters / Setters
        protected static ServicioHeaders ServicioHeaders
        {
            get
            {
                if (servicioHeaders == null)
                {
                    servicioHeaders = new ServicioHeaders();
                }
                return servicioHeaders;
            }
        }

        #endregion Getters / Setters 

        #region Métodos 
        public abstract Task<APIResponse> SendRequest(T objecto, string Json = null);

        public async Task ConstruirURL(ParametersRequest parametros)
        {
            ParametersRequest Parametros = parametros as ParametersRequest;
            string newUrl = Url;

            if (Parametros.Parametros.Count() > 0)
            {
                newUrl = (newUrl.Substring(Url.Length - 1) == "/") ? newUrl.Remove(newUrl.Length - 1) : newUrl;
                Parametros.Parametros.ForEach(p => newUrl += "/" + p);
            }

            if (Parametros.QueryParametros.Count() > 0)
            {
                var queryParameters = await new FormUrlEncodedContent(Parametros.QueryParametros).ReadAsStringAsync();
                newUrl += queryParameters;
            }

            UrlParameters = newUrl;

        }


        #endregion Métodos
    }
}
