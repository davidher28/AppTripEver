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
    public class CrearServicioViewModel : BaseViewModel
    {
        #region Request

        public ElegirRequest<BaseModel> CrearNuevoServicio { get; set; }

        #endregion Request 

        #region Commands
        public ICommand CrearServicioCommand { get; set; }

        #endregion Commands

        #region Properties
        public MessageViewPop PopUp { get; set; }


        public ValidatableObject<string> Titulo { get; set; }
        public ValidatableObject<string> Pais { get; set; }
        public ValidatableObject<string> Ciudad { get; set; }
        public ValidatableObject<int> MaxPersonas { get; set; }
        public ValidatableObject<string> Descripcion { get; set; }
        public ValidatableObject<int> Precio { get; set; }
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

        #endregion Getters & Setters

        #region Initialize
        public CrearServicioViewModel()
        {
            PopUp = new MessageViewPop();
            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            Horario = new HorarioModel()
            {
                FechaInicio = FechaInicio.Value,
                FechaFinal = FechaInicio.Value,
                HoraInicio = FechaInicio.Value,
                HoraFinal = FechaInicio.Value,

            };
            Message = new MessageModel { Message = "Servicio creado correctamente" };
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
        }

        public void InitializeRequest()
        {
            string urlCrearServicio = Endpoints.URL_SERVIDOR + Endpoints.CREAR_SERVICIO;
            CrearNuevoServicio = new ElegirRequest<BaseModel>();
            CrearNuevoServicio.ElegirEstrategia("POST", urlCrearServicio);
        }

        public void InitializeCommands()
        {
            CrearServicioCommand = new Command(async () => await CrearServicio(), () => true);
        }

        public void InitializeFields()
        {
            Titulo = new ValidatableObject<string>();
            Pais = new ValidatableObject<string>();
            Ciudad = new ValidatableObject<string>();
            MaxPersonas = new ValidatableObject<int>();
            Descripcion = new ValidatableObject<string>();
            Precio = new ValidatableObject<int>();
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
            ServiciosModel servicio = new ServiciosModel(Horario, Host)
            {
                Titulo = Titulo.Value,
                Pais = Pais.Value,
                Ciudad = Ciudad.Value,
                NumMaxPersonas = MaxPersonas.Value,
                Descripcion = Descripcion.Value,
                Precio = Precio.Value
            };
            APIResponse response = await CrearNuevoServicio.EjecutarEstrategia(servicio);
            if (response.IsSuccess)
            {
                Host.Servicios.Add(servicio);
                MessageViewPop popUp = new MessageViewPop();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
            }
        }
        #endregion Methods
    }
}