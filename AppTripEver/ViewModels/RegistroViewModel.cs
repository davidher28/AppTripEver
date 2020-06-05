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
    public class RegistroViewModel : BaseViewModel
    {
        #region Request

        public ElegirRequest<BaseModel> CrearNuevoUsuario { get; set; }

        #endregion Request 

        #region Commands
        public ICommand CrearUsuarioCommand { get; set; }

        public ICommand ValidateNombreCommand { get; set; }

        public ICommand ValidateMailCommand { get; set; }

        public ICommand ValidateTelefonoCommand { get; set; }

        public ICommand ValidateFechaCommand { get; set; }

        public ICommand ValidateTipoIdentificacionCommand { get; set; }

        public ICommand ValidateIdentificacionCommand { get; set; }

        public ICommand ValidateUsuarioCommand { get; set; }

        public ICommand ValidateContraCommand { get; set; }
        #endregion Commands

        #region Properties
        public MessageViewPop PopUp { get; set; }

        public ValidatableObject<string> NombreUsuario { get; set; }
        public ValidatableObject<string> MailUsuario { get; set; }
        public ValidatableObject<string> TelUsuario { get; set; }
        public ValidatableObject<string> FechaUsuario { get; set; }
        public ValidatableObject<string> TipoIdentUsuario { get; set; }
        public ValidatableObject<string> IdentUsuario { get; set; }
        public ValidatableObject<string> UserUsuario { get; set; }
        public ValidatableObject<string> ContraUsuario { get; set; }

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private MessageModel message;

        private bool isCrearEnable;

        private bool isNombreEnable;

        private bool isMailEnable;

        private bool isTelEnable;

        private bool isFechaEnable;

        private bool isTipoIdentEnable;

        private bool isIdentEnable;

        private bool isUsuarioEnable;

        private bool isContraEnable;

        public NavigationService NavigationService { get; set; }

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
        public bool IsCrearEnable
        {
            get { return isCrearEnable; }
            set
            {
                isCrearEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsNombreEnable
        {
            get { return isNombreEnable; }
            set
            {
                isNombreEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsMailEnable
        {
            get { return isMailEnable; }
            set
            {
                isMailEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsTelEnable
        {
            get { return isTelEnable; }
            set
            {
                isTelEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsFechaEnable
        {
            get { return isFechaEnable; }
            set
            {
                isFechaEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsTipoIdentEnable
        {
            get { return isTipoIdentEnable; }
            set
            {
                isTipoIdentEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsIdentEnable
        {
            get { return isIdentEnable; }
            set
            {
                isIdentEnable = value;
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
        public RegistroViewModel()
        {
            PopUp = new MessageViewPop();
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            Message = new MessageModel { Message = "Datos incorrectos" };
            IsNombreEnable = false;
            IsMailEnable = false;
            IsTelEnable = false;
            IsFechaEnable = false;
            IsTipoIdentEnable = false;
            IsIdentEnable = false;
            IsUsuarioEnable = false;
            IsContraEnable = false;
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
            AddValidations();
            NavigationService = new NavigationService();
        }

        public void InitializeRequest()
        {
            string urlCrear_Usuario = Endpoints.URL_SERVIDOR + Endpoints.CREAR_USUARIO;

            CrearNuevoUsuario = new ElegirRequest<BaseModel>();
            CrearNuevoUsuario.ElegirEstrategia("POST", urlCrear_Usuario);

        }

        public void InitializeCommands()
        {
            CrearUsuarioCommand = new Command(async () => await Login(), () => isCrearEnable);
            ValidateNombreCommand = new Command(() => ValidateNombreUsuarioForm(), () => true);
            ValidateMailCommand = new Command(() => ValidateMailUsuarioForm(), () => true);
            ValidateTelefonoCommand = new Command(() => ValidateTelUsuarioForm(), () => true);
            ValidateFechaCommand = new Command(() => ValidateFechaUsuarioForm(), () => true);
            ValidateTipoIdentificacionCommand = new Command(() => ValidateTipoIdentUsuarioForm(), () => true);
            ValidateIdentificacionCommand = new Command(() => ValidateIdentUsuarioForm(), () => true);
            ValidateUsuarioCommand = new Command(() => ValidateUserUsuarioForm(), () => true);
            ValidateContraCommand = new Command(() => ValidateContraUsuarioForm(), () => true);
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

        public void AddValidations()
        {
            NombreUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El nombre del usuario es Obligatorio" });
            MailUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El mail es Obligatorio" });
            MailUsuario.Validation.Add(new EmailRule<string> { ValidationMessage = "Debe introducir un Email" });
            TelUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El Telefono es Obligatorio" });
            TelUsuario.Validation.Add(new TenDigitsRule<string> { ValidationMessage = "El telefono es incorrecto" });
            FechaUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "La fecha de nacimiento es Obligatoria" });
            TipoIdentUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El Tipo de Identificacion es Obligatorio" });
            IdentUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El numerode identificacion es Obligatorio" });
            UserUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El Usuario es Obligatorio" });
            ContraUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El id es Obligatorio" });
        }

        #endregion Initialize

        #region Methods
        public async Task Login()
        {
            if (IsCrearEnable == true)
            {
                try
                {
                    ParametersRequest parametros = new ParametersRequest();
                    parametros.Parametros.Add(NombreUsuario.Value);
                    parametros.Parametros.Add(ContraUsuario.Value);
                    APIResponse response = await CrearNuevoUsuario.EjecutarEstrategia(null, parametros);
                    if (response.IsSuccess)
                    {
                        Usuario = JsonConvert.DeserializeObject<UsuarioModel>(response.Response);
                        Console.WriteLine(Usuario.IsHost);
                        if (Usuario.IsHost == true)
                        {
                            await NavigationService.PushPage(new ChooseView(), Usuario);

                        }
                        else if (Usuario.IsHost == false)
                        {
                            Console.WriteLine("HOLAAAAAA");
                            await NavigationService.PushPage(new ServicesView(), Usuario);

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
                catch (Exception)
                {
                    //((MessageViewModel)PopUp.BindingContext).Message = "Sistema no disponible en este momento.";
                }
            }
            else
            {

            }

        }

        public async Task Registro()
        {
            await NavigationService.PushPage(new RegistroView());
        }

        private void ValidateNombreUsuarioForm()
        {
            IsNombreEnable = NombreUsuario.Validate();

            if (IsNombreEnable && IsMailEnable && IsTelEnable && IsFechaEnable && IsTipoIdentEnable && IsIdentEnable && IsUsuarioEnable && IsContraEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearUsuarioCommand).ChangeCanExecute();
            }
            }

        private void ValidateMailUsuarioForm()
        {
            IsMailEnable = MailUsuario.Validate();

            if (IsNombreEnable && IsMailEnable && IsTelEnable && IsFechaEnable && IsTipoIdentEnable && IsIdentEnable && IsUsuarioEnable && IsContraEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearUsuarioCommand).ChangeCanExecute();
            }
        }

        private void ValidateTelUsuarioForm()
        {
            IsTelEnable = TelUsuario.Validate();

            if (IsNombreEnable && IsMailEnable && IsTelEnable && IsFechaEnable && IsTipoIdentEnable && IsIdentEnable && IsUsuarioEnable && IsContraEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearUsuarioCommand).ChangeCanExecute();
            }
        }

        private void ValidateFechaUsuarioForm()
        {
            IsFechaEnable = FechaUsuario.Validate();

            if (IsNombreEnable && IsMailEnable && IsTelEnable && IsFechaEnable && IsTipoIdentEnable && IsIdentEnable && IsUsuarioEnable && IsContraEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearUsuarioCommand).ChangeCanExecute();
            }
        }

        private void ValidateTipoIdentUsuarioForm()
        {
            IsTipoIdentEnable = TipoIdentUsuario.Validate();

            if (IsNombreEnable && IsMailEnable && IsTelEnable && IsFechaEnable && IsTipoIdentEnable && IsIdentEnable && IsUsuarioEnable && IsContraEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearUsuarioCommand).ChangeCanExecute();
            }
        }

        private void ValidateIdentUsuarioForm()
        {
            IsIdentEnable = IdentUsuario.Validate();

            if (IsNombreEnable && IsMailEnable && IsTelEnable && IsFechaEnable && IsTipoIdentEnable && IsIdentEnable && IsUsuarioEnable && IsContraEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearUsuarioCommand).ChangeCanExecute();
            }
        }

        private void ValidateUserUsuarioForm()
        {
            IsUsuarioEnable = UserUsuario.Validate();

            if (IsNombreEnable && IsMailEnable && IsTelEnable && IsFechaEnable && IsTipoIdentEnable && IsIdentEnable && IsUsuarioEnable && IsContraEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearUsuarioCommand).ChangeCanExecute();
            }
        }

        private void ValidateContraUsuarioForm()
        {
            IsContraEnable = ContraUsuario.Validate();

            if (IsNombreEnable && IsMailEnable && IsTelEnable && IsFechaEnable && IsTipoIdentEnable && IsIdentEnable && IsUsuarioEnable && IsContraEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearUsuarioCommand).ChangeCanExecute();
            }            
        }
        #endregion Methods
    }
}
