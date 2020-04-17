using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTripEver.Services.APIRest
{
    /* Verbos GET y DELETE */
    public class RequestParametros<T> : Request<T>
    {
        public RequestParametros(string verbo, string url) 
        {
            Url = url;
            Verbo = verbo; 
        }

        #region Métodos
        public override async Task<APIResponse> SendRequest(T objecto)
        {
            APIResponse respuesta = new APIResponse()
            {
                Code = 400,
                IsSuccess = false,
                Response = ""
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var verboHttp = (Verbo == "GET") ? HttpMethod.Get : HttpMethod.Delete;
                    await this.ConstruirURL(objecto);
                    HttpRequestMessage requestMessage = new HttpRequestMessage(verboHttp, Url);
                    requestMessage = ServicioHeaders.AgregarCabeceras(requestMessage);
                    HttpResponseMessage HttpResponse = await client.SendAsync(requestMessage);
                    respuesta.Code = Convert.ToInt32(HttpResponse.StatusCode);
                    respuesta.IsSuccess = HttpResponse.IsSuccessStatusCode;
                    respuesta.Response = await HttpResponse.Content.ReadAsStringAsync();
                }
            }
            catch(Exception)
            {
                respuesta.Response = "Error al momento de llamar al servidor";

            }

            return respuesta;
        }

        private async Task ConstruirURL(T parametros)
        {
            ParametersRequest Parametros = parametros as ParametersRequest;
            
            if (Parametros.Parametros.Count() > 0 )
            {
                Url = (Url.Substring(Url.Length - 1) == "/") ? Url.Remove(Url.Length - 1) : Url;
                Parametros.Parametros.ForEach(p => Url += "/" + p);
            }

            if (Parametros.QueryParametros.Count() > 0)
            {
                var queryParameters = await new FormUrlEncodedContent(Parametros.QueryParametros).ReadAsStringAsync();
                Url += queryParameters;
            }

        }

        private object FormUrlEncodedContent(Dictionary<string, string> queryParametros)
        {
            throw new NotImplementedException();
        }
        #endregion Métodos

    }
}
