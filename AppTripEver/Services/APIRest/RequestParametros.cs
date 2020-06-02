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
        public RequestParametros(string url, string verbo) 
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
                    client.Timeout = TimeSpan.FromSeconds(50);
                    HttpRequestMessage requestMessage = new HttpRequestMessage(verboHttp, UrlParameters);
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
        #endregion Métodos

    }
}
