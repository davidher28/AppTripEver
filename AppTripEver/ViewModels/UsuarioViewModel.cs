using System;
using System.Collections.Generic;
using System.Text;
using AppTripEver.Services.Propagation;
using AppTripEver.Services.APIRest;
using AppTripEver.Models;
using Newtonsoft.Json;
using AppTripEver.Configuration;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppTripEver.Models.AuxiliarModels;

namespace AppTripEver.ViewModels
{

    public class UsuarioViewModel : NotificationObject
    {
        public ElegirRequest<BaseModel> GetUsuarios { get; set; }
        public ElegirRequest<BaseModel> GetUsuario { get; set; }
        public ElegirRequest<UsuarioModel> CreateUsuario { get; set; }
        public ElegirRequest<UsuarioModel> EditUsuario { get; set; }
        public ElegirRequest<BaseModel> DeleteUsuario { get; set; }

        public ICommand IniciarSesionCommand { get; set; }
        public UsuarioViewModel()
        {
            string urlUsuarios = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_ALL_USUARIOS;
            string urlUsuario = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_USUARIO;
            string urlCrearUsuario = Endpoints.URL_SERVIDOR + Endpoints.CREAR_USUARIO;
            string urlEditarUsuario = Endpoints.URL_SERVIDOR + Endpoints.EDITAR_USUARIO;
            string urlEliminarUsuario = Endpoints.URL_SERVIDOR + Endpoints.ELIMINAR_USUARIO;

            GetUsuarios = new ElegirRequest<BaseModel>();
            GetUsuarios.ElegirEstrategia("GET", urlUsuarios);

            GetUsuario = new ElegirRequest<BaseModel>();
            GetUsuario.ElegirEstrategia("GET", urlUsuario);

            CreateUsuario = new ElegirRequest<UsuarioModel>();
            CreateUsuario.ElegirEstrategia("POST", urlCrearUsuario);

            EditUsuario = new ElegirRequest<UsuarioModel>();
            EditUsuario.ElegirEstrategia("PUT", urlEditarUsuario);

            DeleteUsuario = new ElegirRequest<BaseModel>();
            DeleteUsuario.ElegirEstrategia("DELETE", urlEliminarUsuario);

            IniciarSesionCommand = new Command(async () => await SeleccionarUsuario(), () => true);

        }
        public async Task SeleccionarUsuario()
        {
            ParametersRequest parametros = new ParametersRequest();
            parametros.Parametros.Add("1");
            APIResponse response = await GetUsuario.EjecutarEstrategia(null, parametros);
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(response.Response);
        }
    }
}
 