using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Validation;
using AppTripEver.Views;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class InfoUserViewModel : BaseViewModel
    {
        #region Request

        public ElegirRequest<BaseModel> UpdateUser { get; set; }

        #endregion Request


        #region Commands

        public ICommand CloseCommand { get; set; }

        public ICommand UpdateUserCommand { get; set; }

        public ICommand CreateHost { get; set; }

        #endregion Commands


        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private MessageModel message;

        public ValidatableObject<string> NombreUsuario { get; set; }
        public ValidatableObject<string> MailUsuario { get; set; }
        public ValidatableObject<string> TelUsuario { get; set; }
        public ValidatableObject<string> FechaUsuario { get; set; }
        public ValidatableObject<string> TipoIdentUsuario { get; set; }
        public ValidatableObject<string> IdentUsuario { get; set; }
        public ValidatableObject<string> UserUsuario { get; set; }
        public ValidatableObject<string> ContraUsuario { get; set; }

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

        public MessageModel Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters


        #region Initialize

        public InfoUserViewModel()
        {
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(cartera);
            Message = new MessageModel() { Message = "Actualización exitosa" };
            InitializeCommands();
            InitializeRequest();
            InitializeFields();
            NombreUsuario.Value = Usuario.Nombre;
            MailUsuario.Value = Usuario.Email;
            TelUsuario.Value = Usuario.Telefono;
            FechaUsuario.Value = Usuario.FechaNacimiento;
            TipoIdentUsuario.Value = Usuario.TipoIdentificacion;
            IdentUsuario.Value = Usuario.Identificacion;
            UserUsuario.Value = Usuario.NombreUsuario;
            ContraUsuario.Value = Usuario.Contrasena;

        }

        public void InitializeRequest()
        {

            string urlUserUpdate = Endpoints.URL_SERVIDOR + Endpoints.EDITAR_USUARIO;

            UpdateUser = new ElegirRequest<BaseModel>();
            UpdateUser.ElegirEstrategia("PUT", urlUserUpdate);
        }

        public void InitializeFields()
        {
            NombreUsuario = new ValidatableObject<string>();
            MailUsuario = new ValidatableObject<string>();
            TelUsuario = new ValidatableObject<string>();
            FechaUsuario = new ValidatableObject<string>();
            TipoIdentUsuario = new ValidatableObject<string>();
            IdentUsuario = new ValidatableObject<string>();
            UserUsuario = new ValidatableObject<string>();
            ContraUsuario = new ValidatableObject<string>();
        }

        public void InitializeCommands()
        {
            UpdateUserCommand = new Command(async () => await UpdateUserForm(), () => true);
            CloseCommand = new Command(async () => await Close(), () => true);
            CreateHost = new Command(async () => await NewHost(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            Usuario = usuario;
        }

        #endregion Initialize


        #region Methods

        public async Task UpdateUserForm()
        {
            JObject vals =
                new JObject(
                    new JProperty("Mail", MailUsuario.Value ?? Usuario.Email),
                    new JProperty("Telefono", TelUsuario.Value ?? Usuario.Telefono),
                    new JProperty("Contrasena", ContraUsuario.Value ?? Usuario.Contrasena)
                    );
            string Json = vals.ToString();
            ParametersRequest parametros = new ParametersRequest();
            parametros.Parametros.Add(Usuario.IdUsuario.ToString());
            APIResponse response = await UpdateUser.EjecutarEstrategia(Usuario, parametros, Json);
            if (response.IsSuccess)
            {
                Usuario.Email = MailUsuario.Value;
                Usuario.Telefono = TelUsuario.Value;
                Usuario.Contrasena = ContraUsuario.Value;

                PopGeneralView popUp = new PopGeneralView();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
            }
            else
            {
                Message.Message = "Actualización no exitosa";
                PopGeneralView popUp = new PopGeneralView();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
            }
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task NewHost()
        {
            RegistroHostView popUp = new RegistroHostView();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync(Usuario);
            await PopupNavigation.Instance.PushAsync(popUp);

        }

        #endregion Methods
    }
}