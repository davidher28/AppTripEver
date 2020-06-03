 using System;
using System.Collections.Generic;
using System.Text;
using AppTripEver.Services.Propagation;
using AppTripEver.Services.APIRest;
using AppTripEver.Views;
using AppTripEver.Models;
using Newtonsoft.Json;
using AppTripEver.Configuration;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppTripEver.Models.AuxiliarModels;
using System.Collections.ObjectModel;

using AppTripEver.Validation;
using AppTripEver.Validation.Rules;

namespace AppTripEver.ViewModels
{

    public class UsuarioViewModel : NotificationObject
    {

        #region Request
        public ElegirRequest<BaseModel> GetUsuarios { get; set; }
        public ElegirRequest<BaseModel> GetUsuario { get; set; }
        public ElegirRequest<UsuarioModel> CreateUsuario { get; set; }
        public ElegirRequest<UsuarioModel> EditUsuario { get; set; }
        public ElegirRequest<BaseModel> DeleteUsuario { get; set; }
        #endregion

        #region Attributes

        public MessageViewPop PopUp { get; set; }

        public ValidatableObject<string> BusquedaUsuario{ get; set; }

        public ValidatableObject<string> NombreUsuario{ get; set; }

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private bool isGuardarEnable;

        private bool isEliminarEnable;

        private bool isGuardarEditar;

        private bool isBuscarEnable;

        private ObservableCollection<UsuarioModel> usuarios;


        #endregion

        #region Comandos

        public ICommand IniciarSesionCommand { get; set; }

        public ICommand ListaUsuariosCommand { get; set; }

        public ICommand SelectUsuarioCommand { get; set; }

        public ICommand CrearUsuarioCommand { get; set; }

        public ICommand EditarUsuarioCommand { get; set; }

        public ICommand EliminarUsuarioCommand { get; set; }

        public ICommand ValidateBusquedaCommand { get; set; }

        public ICommand ValidateNombreUsuarioCommand { get; set; }

        #endregion

        #region Getters & Setters

        public UsuarioModel Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                OnPropertyChanged();
            }
        }

        public CarteraModel Cartera
        {
            get { return cartera; }
            set
            {
                cartera = value;
                OnPropertyChanged();
            }
        }

        public bool IsGuardarEnable
        {
            get { return isGuardarEnable; }
            set
            {
                isGuardarEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsEliminarEnable
        {
            get { return isEliminarEnable; }
            set
            {
                isEliminarEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsGuardarEditar
        {
            get { return isGuardarEditar; }
            set
            {
                isGuardarEditar = value;
                OnPropertyChanged();
            }
        }

        public bool IsBuscarEnable
        {
            get { return isBuscarEnable; }
            set
            {
                isBuscarEnable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UsuarioModel> Usuarios
        {
            get { return usuarios; }
            set
            {
                usuarios = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Initialize

        public UsuarioViewModel()
        {
            PopUp = new MessageViewPop();
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            IsGuardarEnable = false;
            IsEliminarEnable = false;
            IsGuardarEditar = false;
            IsBuscarEnable = false;
            Usuarios = new ObservableCollection<UsuarioModel>();
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
        }

        public void InitializeRequest()
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
        }

        public void InitializeCommands()
        {
            IniciarSesionCommand = new Command(async () => await SeleccionarUsuario(), () => true);
            ListaUsuariosCommand = new Command(async () => await ListaUsuarios(), () => true);
            SelectUsuarioCommand = new Command(async () => await SeleccionarUsuario(), () => IsBuscarEnable);
            //CrearUsuarioCommand = new Command(async () => await NuevoUsuario(), () => true);
            //EliminarUsuarioCommand = new Command(async () => await DeleteUsuario(), () => IsEliminarEnable);
            //EditarUsuarioCommand = new Command(async () => await GuardarUsuario(), () => IsGuardarEnable);
            //ValidateBusquedaCommand = new Command(async () => await ValidateBusquedaForm(), () => true);
            //ValidateNombreUsuarioCommand = new Command(async () => await ValidateNombreUsuarioForm(), () => true);
        }

        public void InitializeFields()
        {
            BusquedaUsuario = new ValidatableObject<string>();
            NombreUsuario = new ValidatableObject<string>();

            BusquedaUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El id es Obligatorio"});
            NombreUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El nombre del usuario es Obligatorio"});
        }

        #endregion

        public async Task SeleccionarUsuario()
        {
            ParametersRequest parametros = new ParametersRequest();
            parametros.Parametros.Add("1");
            APIResponse response = await GetUsuario.EjecutarEstrategia(null, parametros);
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(response.Response);
        }

        public async Task ListaUsuarios()
        {
            APIResponse response = await GetUsuarios.EjecutarEstrategia(null);
            if (response.IsSuccess)
            {
                List<UsuarioModel> listaUsuarios = JsonConvert.DeserializeObject<List<UsuarioModel>>(response.Response);
                Usuarios = new ObservableCollection<UsuarioModel>(listaUsuarios);
            }
            else
            {
                ((MessageViewModel)PopUp.BindingContext).Message = "Error al cargar los usuarios";
            }
        }
    }
}
 