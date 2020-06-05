using System;
using System.Collections.Generic;
using System.Text;
using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Validation;
using AppTripEver.Validation.Rules;
using AppTripEver.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppTripEver.Behaviors;

namespace AppTripEver.ViewModels
{
    public class RegistroHostViewModel : BaseViewModel
    {
        #region Request

        public ElegirRequest<BaseModel> CrearNuevoHost { get; set; }

        #endregion Request 

        #region Commands
        public ICommand CrearHostCommand { get; set; }

        #endregion Commands

        #region Properties
        public MessageViewPop PopUp { get; set; }

        public ValidatableObject<string> MailUsuario { get; set; }
        public ValidatableObject<string> NoCuentaUsuario { get; set; }

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private UsuarioHostModel host;

        #endregion Properties

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

        public UsuarioHostModel Host
        {
            get { return host; }
            set
            {
                host = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters & Setters

        #region Initialize
        public RegistroHostViewModel()
        {
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            Host = new UsuarioHostModel(Cartera);
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
        }

        public void InitializeRequest()
        {
            string urlCrear_Host = Endpoints.URL_SERVIDOR + Endpoints.CREAR_USUARIO;
            CrearNuevoHost = new ElegirRequest<BaseModel>();
            CrearNuevoHost.ElegirEstrategia("POST", urlCrear_Host);
        }

        public void InitializeCommands()
        {
            CrearHostCommand = new Command(async () => await CrearHost(), () => true);
        }

        public void InitializeFields()
        {
            MailUsuario = new ValidatableObject<string>();
            NoCuentaUsuario = new ValidatableObject<string>();

        }
        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            Usuario = usuario;
        }

        #endregion Initialize

        #region Methods

        public async Task CrearHost()
        {
            UsuarioHostModel host = new UsuarioHostModel(Cartera)
            {
                NoCuenta = NoCuentaUsuario.Value,
                MailHost = MailUsuario.Value
            };

            APIResponse response = await CrearNuevoHost.EjecutarEstrategia(host);
        }

        #endregion Methods
    }
}
