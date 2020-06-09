using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Validation;
using AppTripEver.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class ServiceInformationViewModel : BaseViewModel
    {

        #region Request
        public ElegirRequest<BaseModel> PostBooking { get; set; }

        #endregion Request 

        #region Commands
        public ICommand BookingCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        #endregion Commands

        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private UsuarioHostModel host;

        private HorarioModel horario;

        private ServiciosModel service;

        private ReservasModel booking;

        private EstadoModel bookingState;

        private MessageModel message;

        public ValidatableObject<string> FechaInicio { get; set; }

        public ValidatableObject<string> FechaFinal { get; set; }

        public ValidatableObject<string> NumPersonas { get; set; }

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

        public UsuarioHostModel Host
        {
            get { return host; }
            set
            {
                host = value;
                OnPropertyChanged();
            }
        }


        public HorarioModel Horario
        {
            get { return horario; }
            set
            {
                horario = value;
                OnPropertyChanged();
            }
        }

        public ServiciosModel Service
        {
            get { return service; }
            set
            {
                service = value;
                OnPropertyChanged();
            }
        }

        public ReservasModel Booking
        {
            get { return booking; }
            set
            {
                booking = value;
                OnPropertyChanged();
            }
        }

        public EstadoModel BookingState
        {
            get { return bookingState; }
            set
            {
                bookingState = value;
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
        public ServiceInformationViewModel()
        {
            BookingState = new EstadoModel();
            Booking = new ReservasModel(BookingState, Service, Usuario);
            NavigationService = new NavigationService();
            InitializeCommands();
            InitializeRequest();
            InitializeFields();
        }

        public void InitializeRequest()
        {
            string urlBoking = Endpoints.URL_SERVIDOR + Endpoints.CREAR_RESERVA;


            PostBooking = new ElegirRequest<BaseModel>();
            PostBooking.ElegirEstrategia("POST", urlBoking);

        }

        public void InitializeFields()
        {
            FechaInicio = new ValidatableObject<string>();
            FechaFinal = new ValidatableObject<string>();
            NumPersonas = new ValidatableObject<string>();

        }

        public void InitializeCommands()
        {
            BookingCommand = new Command(async () => await BookServiceForm(), () => true);
            CloseCommand = new Command(async () => await Close(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            //var service = parameters2 as ServiciosModel;
            Usuario = usuario;
        }

        #endregion Initialize

        #region

        public async Task BookServiceForm()
        {
            Booking.FechaInicio = FechaInicio.Value;
            Booking.FechaFin = FechaFinal.Value;
            BookingState.IdEstado = 1;
            JObject vals =
                new JObject(
                    new JProperty("numPersonas", NumPersonas.Value),
                    new JProperty("IdEstado", Booking.Estado.IdEstado),
                    new JProperty("IdUsuario", Booking.Cliente.IdUsuario),
                    new JProperty("IdServicio", Booking.Servicio.IdServicio),
                    new JProperty("fechaInicio", FechaInicio.Value),
                    new JProperty("fechaFin", FechaFinal.Value)
                    );
            string Json = vals.ToString();
            APIResponse response = await PostBooking.EjecutarEstrategia(Booking, null, Json);
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
                Message.Message = "Servicio no creado";
                MessageViewPop popUp = new MessageViewPop();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
            }
        }


        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }
        #endregion Methods
    }
}
