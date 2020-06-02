using AppTripEver.Models.AuxiliarModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTripEver.Services.APIRest
{
    /* Verbos POST y PUT */
    public class RequestBody<T> : Request<T>
    {
        public RequestBody(string url, string verbo)
        {
            Url = url;
            Verbo = verbo;
        }
        public override async Task<APIResponse> SendRequest(T objecto)
        {
            APIResponse respuesta = new APIResponse()
            {
                Code = 400,
                IsSuccess = false,
                Response = ""
            };

            string objetoJson = JsonConvert.SerializeObject(objecto);
            HttpContent content = new StringContent(objetoJson, Encoding.UTF8);

            try
            {
                using (var client = new HttpClient())
                {
                    var verboHttp = (Verbo == "POST") ? HttpMethod.Post : HttpMethod.Put;
                    HttpRequestMessage requestMessage = new HttpRequestMessage(verboHttp, UrlParameters);
                    //requestMessage = ServicioHeaders.AgregarCabeceras(requestMessage);
                    requestMessage.Content = content;
                    client.Timeout = TimeSpan.FromSeconds(50);
                    HttpResponseMessage HttpResponse = client.SendAsync(requestMessage).Result;
                    respuesta.Code = Convert.ToInt32(HttpResponse.StatusCode);
                    respuesta.IsSuccess = HttpResponse.IsSuccessStatusCode;
                    respuesta.Response = await HttpResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                respuesta.Response = "Error al momento de llamar al servidor";

            }
            return respuesta;
        }
    }
}
