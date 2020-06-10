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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppTripEver.Behaviors;
using Newtonsoft.Json.Linq;

namespace AppTripEver.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Request

        public ElegirRequest<BaseModel> GetUsuario_Name { get; set; }
        public ElegirRequest<BaseModel> GetUsuarioHost { get; set; }

        #endregion Request 

        #region Commands
        public ICommand IniciarSesionCommand { get; set; }

        public ICommand RegistroCommand { get; set; }

        public ICommand ValidateContraUsuarioCommand { get; set; }

        public ICommand ValidateNombreUsuarioCommand { get; set; }
        #endregion Commands

        #region Properties
        public MessageViewPop PopUp { get; set; }

        private ValidatableObject<string> contraUsuario { get; set; }
        private ValidatableObject<string> nombreUsuario { get; set; }

        private UsuarioModel usuario;

        private UsuarioHostModel host;

        private CarteraModel cartera;

        private MessageModel message;

        private LoginModel loginm;

        private bool isUsuarioEnable;

        private bool isContraEnable;

        public NavigationService NavigationService { get; set; }

        #endregion Properties

        #region Getters & Setters

        public ValidatableObject<string> NombreUsuario
        {
            get { return nombreUsuario; }
            set
            {
                nombreUsuario = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> ContraUsuario
        {
            get { return contraUsuario; }
            set
            {
                contraUsuario = value;
                OnPropertyChanged();
            }
        }

        public LoginModel LoginM
        {
            get { return loginm; }
            set
            {
                loginm = value;
                OnPropertyChanged();
            }
        }

        public UsuarioModel Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
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

        public bool IsUsuarioEnable
        {
            get { return isUsuarioEnable; }
            set
            {
                isUsuarioEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsContraEnable
        {
            get { return isContraEnable; }
            set
            {
                isContraEnable = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters/Setters

        #region Initialize
        public LoginViewModel()
        {
            PopUp = new MessageViewPop();
            Cartera = new CarteraModel();
            LoginM = new LoginModel();
            Usuario = new UsuarioModel(Cartera);
            Host = new UsuarioHostModel(Cartera);
            Message = new MessageModel { Message = "Datos incorrectos" };
            IsUsuarioEnable = false;
            IsContraEnable = false;
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
            NavigationService = new NavigationService();
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var login = parameters as LoginModel;
            LoginM = login;
            NombreUsuario.Value = LoginM.Nombre;
            ContraUsuario.Value = LoginM.Contra;
        }

        public void InitializeRequest()
        {
            string urlUsuario_Name = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_USUARIO_NOMBRE;
            string urlUsuarioHost = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_USUARIO_HOST;

            GetUsuario_Name = new ElegirRequest<BaseModel>();
            GetUsuario_Name.ElegirEstrategia("GET", urlUsuario_Name);
            GetUsuarioHost = new ElegirRequest<BaseModel>();
            GetUsuarioHost.ElegirEstrategia("GET", urlUsuarioHost);

        }

        public void InitializeCommands()
        {
            IniciarSesionCommand = new Command(async () => await Login(), () => IsUsuarioEnable);
            RegistroCommand = new Command(async () => await Registro(), () => true);
            ValidateNombreUsuarioCommand = new Command(() =>  ValidateNombreUsuarioForm(), () => true);
            ValidateContraUsuarioCommand = new Command(() =>  ValidateContraUsuarioForm(), () => true);
        }

        public void InitializeFields()
        {            
            NombreUsuario = new ValidatableObject<string>();
            ContraUsuario = new ValidatableObject<string>();

            NombreUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El nombre del usuario es Obligatorio" });
            ContraUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El id es Obligatorio" });
        }

        #endregion Initialize

        #region Methods
        public async Task Login()
        {

            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(NombreUsuario.Value);
                parametros.Parametros.Add(ContraUsuario.Value);
                APIResponse response = await GetUsuario_Name.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    JToken token = JObject.Parse(response.Response);
                    Usuario = JsonConvert.DeserializeObject<UsuarioModel>(response.Response);
                    Cartera.IdCartera = (long)token.SelectToken("IdCartera");
                    Cartera.MontoTotal = (int)token.SelectToken("Monto");
                    Usuario.Cartera = Cartera;
                    if (Usuario.IsHost == 1)
                    {
                        try
                        {
                            ParametersRequest parametros2 = new ParametersRequest();
                            parametros2.Parametros.Add(Usuario.IdUsuario.ToString());
                            APIResponse response2 = await GetUsuarioHost.EjecutarEstrategia(null, parametros2);
                            if (response2.IsSuccess)
                            {
                                Host = JsonConvert.DeserializeObject<UsuarioHostModel>(response.Response);
                                Host.Cartera = Cartera;
                                Host.IdUsuario = Usuario.IdUsuario;
                                Host.Nombre = Usuario.Nombre;
                                Host.Email = Usuario.Email;
                                Host.FechaNacimiento = Usuario.FechaNacimiento;
                                Host.TipoIdentificacion = Usuario.TipoIdentificacion;
                                Host.Identificacion = Usuario.Identificacion;
                                Host.Telefono = Usuario.Telefono;
                                Host.NombreUsuario = Usuario.NombreUsuario;
                                Host.Contrasena = Usuario.Contrasena;
                                Host.IsHost = Usuario.IsHost;
                                var page = new ChooseView();
                                await NavigationService.PushPage(page, Host);
                                var navigationCount = Application.Current.MainPage.Navigation.NavigationStack.Count - 1 ;
                                Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[navigationCount-1]);
                            }
                        }
                        catch(Exception){ }
                    }
                    else if (Usuario.IsHost == 0)
                    {
                        Console.WriteLine("HOLAAAAAA");
                        await NavigationService.PushPage(new UsuarioTabbedView(), Usuario);

                    }
                }
                else
                {
                    MessageViewPop popUp = new MessageViewPop();
                    var viewModel = popUp.BindingContext;
                    await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                    await PopupNavigation.Instance.PushAsync(popUp);
                    
                }
            }
            catch(Exception)
            {
                //((MessageViewModel)PopUp.BindingContext).Message = "Sistema no disponible en este momento.";
            }
        }

        public async Task Registro()
        {
            await NavigationService.PushPage(new RegistroView());
        }

        private void ValidateNombreUsuarioForm()
        {
            IsUsuarioEnable = NombreUsuario.Validate();
            ((Command)IniciarSesionCommand).ChangeCanExecute();
        }

        private void ValidateContraUsuarioForm()
        {
            IsContraEnable = ContraUsuario.Validate();
            ((Command)IniciarSesionCommand).ChangeCanExecute();
        }

        #endregion Methods
    }
}
