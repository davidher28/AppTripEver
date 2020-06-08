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

        public ICommand ValidateMailUsuarioCommand { get; set; }

        public ICommand ValidateNoCuentaUsuarioCommand { get; set; }

        #endregion Commands

        #region Properties
        public MessageViewPop PopUp { get; set; }


        public ValidatableObject<string> MailUsuario { get; set; }
        public ValidatableObject<string> NoCuentaUsuario { get; set; }

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private UsuarioHostModel host;

        private MessageModel message;

        private bool isCrearEnable;

        private bool isMailUsuarioEnable;

        private bool isNoCuentaUsuarioEnable;

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

        public UsuarioHostModel Host
        {
            get { return host; }
            set
            {
                host = value;
                OnPropertyChanged();
            }
        }

        public bool IsCrearEnable
        {
            get { return isCrearEnable; }
            set
            {
                isCrearEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsMailUsuarioEnable
        {
            get { return isMailUsuarioEnable; }
            set
            {
                isMailUsuarioEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsNoCuentaUsuarioEnable
        {
            get { return isNoCuentaUsuarioEnable; }
            set
            {
                isNoCuentaUsuarioEnable = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters & Setters

        #region Initialize
        public RegistroHostViewModel()
        {
            PopUp = new MessageViewPop();
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            Host = new UsuarioHostModel(Cartera);
            Message = new MessageModel { Message = "Usuario Host creado correctamente" };
            isCrearEnable = false;
            isMailUsuarioEnable = false;
            isNoCuentaUsuarioEnable = false;
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
            AddValidations();
        }

        public void InitializeRequest()
        {
            string urlCrear_Host = Endpoints.URL_SERVIDOR + Endpoints.CREAR_USUARIO_HOST;
            CrearNuevoHost = new ElegirRequest<BaseModel>();
            CrearNuevoHost.ElegirEstrategia("POST", urlCrear_Host);
        }

        public void InitializeCommands()
        {
            CrearHostCommand = new Command(async () => await CrearHost(), () => isCrearEnable);
            ValidateMailUsuarioCommand = new Command(() => ValidateMailUsuarioForm(), () => true);
            ValidateNoCuentaUsuarioCommand = new Command(() => ValidateNoCuentaUsuarioUsuarioForm(), () => true);
        }

        public void InitializeFields()
        {
            MailUsuario = new ValidatableObject<string>();
            NoCuentaUsuario = new ValidatableObject<string>();

        }

        public void AddValidations()
        {
            NoCuentaUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El numero de cuenta es Obligatorio" });
            NoCuentaUsuario.Validation.Add(new TenDigitsRule<string> { ValidationMessage = "El numero de cuenta debe ser de 10 digitos" });
            MailUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El mail es Obligatorio" });
            MailUsuario.Validation.Add(new EmailRule<string> { ValidationMessage = "Debe introducir un Email" });
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
                IdUsuario = Usuario.IdUsuario,
                NoCuenta = NoCuentaUsuario.Value,
                MailHost = MailUsuario.Value,
                IdHost = null,
                IsHost = null
            };

            APIResponse response = await CrearNuevoHost.EjecutarEstrategia(host);
            if (response.IsSuccess)
            {
                MessageViewPop popUp = new MessageViewPop();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
                var navigationCount = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                var navigationStack = Application.Current.MainPage.Navigation.NavigationStack;
                Application.Current.MainPage.Navigation.RemovePage(navigationStack[1]);
            }
            else
            {
                Message.Message = "Usuario ya registrado como Host";
                MessageViewPop popUp = new MessageViewPop();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
            }
        }

        private void ValidateNoCuentaUsuarioUsuarioForm()
        {
            IsNoCuentaUsuarioEnable = NoCuentaUsuario.Validate();

            if (IsMailUsuarioEnable && IsNoCuentaUsuarioEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearHostCommand).ChangeCanExecute();
            }
        }

        private void ValidateMailUsuarioForm()
        {
            IsMailUsuarioEnable = MailUsuario.Validate();

            if (IsMailUsuarioEnable && IsNoCuentaUsuarioEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearHostCommand).ChangeCanExecute();
            }
        }

        #endregion Methods
    }
}
