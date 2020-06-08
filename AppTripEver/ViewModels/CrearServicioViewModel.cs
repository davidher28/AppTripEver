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
using Newtonsoft.Json.Linq;

namespace AppTripEver.ViewModels
{
    public class CrearServicioViewModel : BaseViewModel
    {
        #region Request

        public ElegirRequest<BaseModel> CrearNuevoServicio { get; set; }

        #endregion Request 

        #region Commands
        public ICommand CrearServicioCommand { get; set; }

        public ICommand ValidateTipoServicioCommand { get; set; }

        public ICommand ValidateTituloCommand { get; set; }

        public ICommand ValidatePaisCommand { get; set; }

        public ICommand ValidateCiudadCommand { get; set; }

        public ICommand ValidateMaxPersonasCommand { get; set; }

        public ICommand ValidateDescripcionCommand { get; set; }

        public ICommand ValidatePrecioCommand { get; set; }

        public ICommand ValidateFechaInicioCommand { get; set; }

        public ICommand ValidateFechaFinalCommand { get; set; }

        public ICommand ValidateHoraInicioCommand { get; set; }

        public ICommand ValidateHoraFinalCommand { get; set; }
        #endregion Commands

        #region Properties
        public MessageViewPop PopUp { get; set; }

        public ValidatableObject<string> TipoServicio { get; set; }
        public ValidatableObject<string> Titulo { get; set; }
        public ValidatableObject<string> Pais { get; set; }
        public ValidatableObject<string> Ciudad { get; set; }
        public ValidatableObject<Nullable<int>> MaxPersonas { get; set; }
        public ValidatableObject<string> Descripcion { get; set; }
        public ValidatableObject<Nullable<int>> Precio { get; set; }
        public ValidatableObject<string> FechaInicio { get; set; }
        public ValidatableObject<string> FechaFinal { get; set; }
        public ValidatableObject<string> HoraInicio { get; set; }
        public ValidatableObject<string> HoraFinal { get; set; }

        public long IdUsuario { get; set; }

        public long IdHost { get; set; }

        private HorarioModel horario;

        private CarteraModel cartera;

        private UsuarioHostModel host;

        private MessageModel message;

        private bool isCrearEnable;

        private bool isTipoServicioEnable;

        private bool isTituloEnable;

        private bool isPaisEnable;

        private bool isCiudadEnable;

        private bool isMaxPersonasEnable;

        private bool isDescripcionEnable;

        private bool isPrecioEnable;

        private bool isFechaInicioEnable;

        private bool isFechaFinalEnable;

        private bool isHoraInicioEnable;

        private bool isHoraFinalEnable;

        #endregion Properties

        #region Getters & Setters
        public HorarioModel Horario
        {
            get { return horario; }
            set
            {
                horario = value;
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

        public bool IsTipoServicioEnable
        {
            get { return isTipoServicioEnable; }
            set
            {
                isTipoServicioEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsTituloEnable
        {
            get { return isTituloEnable; }
            set
            {
                isTituloEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsPaisEnable
        {
            get { return isPaisEnable; }
            set
            {
                isPaisEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsCiudadEnable
        {
            get { return isCiudadEnable; }
            set
            {
                isCiudadEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsMaxPersonasEnable
        {
            get { return isMaxPersonasEnable; }
            set
            {
                isMaxPersonasEnable = value;
                OnPropertyChanged();
            }
        }
        public bool IsDescripcionEnable
        {
            get { return isDescripcionEnable; }
            set
            {
                isDescripcionEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsPrecioEnable
        {
            get { return isPrecioEnable; }
            set
            {
                isPrecioEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsFechaInicioEnable
        {
            get { return isFechaInicioEnable; }
            set
            {
                isFechaInicioEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsFechaFinalEnable
        {
            get { return isFechaFinalEnable; }
            set
            {
                isFechaFinalEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsHoraInicioEnable
        {
            get { return isHoraInicioEnable; }
            set
            {
                isHoraInicioEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsHoraFinalEnable
        {
            get { return isHoraFinalEnable; }
            set
            {
                isHoraFinalEnable = value;
                OnPropertyChanged();
            }
        }


        #endregion Getters & Setters

        #region Initialize
        public CrearServicioViewModel()
        {
            PopUp = new MessageViewPop();
            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            Horario = new HorarioModel();
            Message = new MessageModel { Message = "Servicio creado correctamente" };
            IsCrearEnable = false;
            IsTipoServicioEnable = false;
            IsTituloEnable = false;
            IsPaisEnable = false;
            IsCiudadEnable = false;
            IsMaxPersonasEnable = false;
            IsDescripcionEnable = false;
            IsFechaInicioEnable = false;
            IsFechaFinalEnable = false;
            IsHoraInicioEnable = false;
            IsHoraFinalEnable = false;
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
            AddValidations();
        }

        public void InitializeRequest()
        {
            string urlCrearServicio = Endpoints.URL_SERVIDOR + Endpoints.CREAR_SERVICIO;
            CrearNuevoServicio = new ElegirRequest<BaseModel>();
            CrearNuevoServicio.ElegirEstrategia("POST", urlCrearServicio);
        }

        public void InitializeCommands()
        {
            CrearServicioCommand = new Command(async () => await CrearServicio(), () => IsCrearEnable);
            ValidateTipoServicioCommand = new Command(() => ValidateTipoServicioCommandForm(), () => true);
            ValidateTituloCommand = new Command(() => ValidateTituloCommandForm(), () => true);
            ValidatePaisCommand = new Command(() => ValidatePaisCommandForm(), () => true);
            ValidateCiudadCommand = new Command(() => ValidateCiudadCommandForm(), () => true);
            ValidateMaxPersonasCommand = new Command(() => ValidateMaxPersonasCommandForm(), () => true);
            ValidateDescripcionCommand = new Command(() => ValidateDescripcionCommandForm(), () => true);
            ValidateFechaInicioCommand = new Command(() => ValidateFechaInicioCommandForm(), () => true);
            ValidateFechaFinalCommand = new Command(() => ValidateFechaFinalCommandoForm(), () => true);
            ValidateHoraInicioCommand = new Command(() => ValidateHoraInicioCommandForm(), () => true);
            ValidateHoraFinalCommand = new Command(() => ValidateHoraFinalCommandForm(), () => true);
        }


        public void InitializeFields()
        {
            TipoServicio = new ValidatableObject<string>();
            Titulo = new ValidatableObject<string>();
            Pais = new ValidatableObject<string>();
            Ciudad = new ValidatableObject<string>();
            MaxPersonas = new ValidatableObject<Nullable<int>>();
            Descripcion = new ValidatableObject<string>();
            Precio = new ValidatableObject<Nullable<int>>();
            FechaInicio = new ValidatableObject<string>();
            FechaFinal = new ValidatableObject<string>();
            HoraInicio = new ValidatableObject<string>();
            HoraFinal = new ValidatableObject<string>();
        }

        public void AddValidations()
        {

            TipoServicio.Validation.Add(new RequiredRule<string> { ValidationMessage = "El nombre del usuario es Obligatorio" });
            Titulo.Validation.Add(new RequiredRule<string> { ValidationMessage = "El mail es Obligatorio" });
            Pais.Validation.Add(new RequiredRule<string> { ValidationMessage = "Debe introducir un Email" });
            Ciudad.Validation.Add(new RequiredRule<string> { ValidationMessage = "El Telefono es Obligatorio" });
            Descripcion.Validation.Add(new RequiredRule<string> { ValidationMessage = "La fecha de nacimiento es Obligatoria" });
            FechaInicio.Validation.Add(new RequiredRule<string> { ValidationMessage = "El numerode identificacion es Obligatorio" });
            FechaFinal.Validation.Add(new RequiredRule<string> { ValidationMessage = "El Usuario es Obligatorio" });
            HoraInicio.Validation.Add(new RequiredRule<string> { ValidationMessage = "El id es Obligatorio" });
            HoraFinal.Validation.Add(new RequiredRule<string> { ValidationMessage = "El id es Obligatorio" });

        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioHostModel;
            Host = usuario;
        }

        #endregion Initialize

        #region Methods

        public async Task CrearServicio()
        {
            Horario.FechaInicio = FechaInicio.Value;
            Horario.FechaFinal = FechaFinal.Value;
            Horario.HoraInicio = HoraInicio.Value;
            Horario.HoraFinal = HoraFinal.Value;
            ServiciosModel servicio = new ServiciosModel(Horario, Host)
            {
                Titulo = Titulo.Value,
                Pais = Pais.Value,
                Ciudad = Ciudad.Value,
                NumMaxPersonas = (int)MaxPersonas.Value,
                Descripcion = Descripcion.Value,
                Precio = (int)Precio.Value
            };
            if (TipoServicio.Value == "0")
            {
                TipoServicio.Value = "1";
            }
            else if (TipoServicio.Value == "1")
            {
                TipoServicio.Value = "2";
            }
            JObject vals =
                new JObject(
                    new JProperty("Titulo", servicio.Titulo),
                    new JProperty("Pais", servicio.Pais),
                    new JProperty("Ciudad", servicio.Ciudad),
                    new JProperty("MaxPersonas", servicio.NumMaxPersonas.ToString()),
                    new JProperty("Descripcion", servicio.Descripcion),
                    new JProperty("Precio", servicio.Precio.ToString()),
                    new JProperty("IdHost", Host.IdHost.ToString()),
                    new JProperty("IdTipoServicio", TipoServicio.Value),
                    new JProperty("FechaInicio", Horario.FechaInicio),
                    new JProperty("FechaFin", Horario.FechaFinal),
                    new JProperty("HoraInicio", Horario.HoraInicio),
                    new JProperty("HoraFin", Horario.HoraFinal)
                    );
            string Json = vals.ToString();
            APIResponse response = await CrearNuevoServicio.EjecutarEstrategia(servicio, null, Json);
            if (response.IsSuccess)
            {
                Console.WriteLine("Todo bien");
                //Host.Servicios.Add(servicio);
                MessageViewPop popUp = new MessageViewPop();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
            }
            else
            {
                Message.Message = "Usuario host ya existente";
                MessageViewPop popUp = new MessageViewPop();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
            }
        }

        private void ValidateTipoServicioCommandForm()
        {
            IsTipoServicioEnable = TipoServicio.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidateTituloCommandForm()
        {
            IsTituloEnable = Titulo.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                 IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidatePaisCommandForm()
        {
            IsPaisEnable = Pais.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidateCiudadCommandForm()
        {
            IsCiudadEnable = Ciudad.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                 IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidateMaxPersonasCommandForm()
        {
            IsMaxPersonasEnable = MaxPersonas.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidateDescripcionCommandForm()
        {
            IsDescripcionEnable = Descripcion.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                 IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidateFechaInicioCommandForm()
        {
            IsFechaInicioEnable = FechaInicio.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidateFechaFinalCommandoForm()
        {
            IsFechaFinalEnable = FechaFinal.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                 IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidateHoraInicioCommandForm()
        {
            IsHoraInicioEnable = HoraInicio.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        private void ValidateHoraFinalCommandForm()
        {
            IsHoraFinalEnable = HoraFinal.Validate();

            if (IsTipoServicioEnable && IsTituloEnable && IsPaisEnable && IsCiudadEnable && IsDescripcionEnable &&
                 IsFechaInicioEnable && IsFechaFinalEnable && IsHoraInicioEnable && IsHoraFinalEnable)
            {
                IsCrearEnable = true;
                ((Command)CrearServicioCommand).ChangeCanExecute();
            }
        }

        #endregion Methods
    }
}