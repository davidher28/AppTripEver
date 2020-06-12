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
    public class BookingInfoViewModel : BaseViewModel
    {

        #region Request
        public ElegirRequest<BaseModel> GetEstadoReserva { get; set; }

        public ElegirRequest<BaseModel> UpdateEstado { get; set; }

        #endregion Request 

        #region Commands
        public ICommand AcceptCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        #endregion Commands

        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private UsuarioHostModel host;

        private HorarioModel horario;

        private ServiciosModel service;

        private ReservasModel booking;

        private EstadoModel estado;

        private MessageModel message;

        private bool isCancelEnable;

        private bool isAcceptEnable;

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

        public EstadoModel Estado
        {
            get { return estado; }
            set
            {
                estado = value;
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

        public bool IsCancelEnable
        {
            get { return isCancelEnable; }
            set
            {
                isCancelEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsAcceptEnable
        {
            get { return isAcceptEnable; }
            set
            {
                isAcceptEnable = value;
                OnPropertyChanged();
            }
        }


        #endregion Getters/Setters

        #region Initialize
        public BookingInfoViewModel()
        {
            Estado = new EstadoModel();
            Horario = new HorarioModel();
            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            Service = new ServiciosModel(Horario, Host);
            Booking = new ReservasModel(Estado, Service, Usuario);
            NavigationService = new NavigationService();
            InitializeCommands();
            InitializeRequest();
            InitializeFields();
        }

        public void InitializeRequest()
        {
            string urlEstadoReserva = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_ESTADO;

            GetEstadoReserva = new ElegirRequest<BaseModel>();
            GetEstadoReserva.ElegirEstrategia("GET", urlEstadoReserva);

            string urlUpdateEstado = Endpoints.URL_SERVIDOR + Endpoints.ACTUALIZAR_ESTADO_RESERVA;
            UpdateEstado = new ElegirRequest<BaseModel>();
            UpdateEstado.ElegirEstrategia("PUT", urlUpdateEstado);
        }

        public void InitializeFields()
        {

        }

        public void InitializeCommands()
        {
            AcceptCommand = new Command(async () => await AcceptBooking(), () => true);
            CancelCommand = new Command(async () => await CancelBooking(), () => true);
            CloseCommand = new Command(async () => await Close(), () => true);
        }

        public override async Task ConstructorAsync2(object parameters, object parameters2)
        {
            var usuario = parameters as UsuarioModel;
            var booking = parameters2 as ReservasModel;
            Usuario = usuario;
            Booking = booking;

            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(Booking.IdReservas.ToString());
                APIResponse response = await GetEstadoReserva.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    Estado = JsonConvert.DeserializeObject<EstadoModel>(response.Response);
                    Booking.Estado = Estado;
                }
                else
                { 
                }
            }
            catch (Exception)
            {
            }
            if(Booking.Estado.IdEstado == 1)
            {
                IsAcceptEnable = true;
                IsCancelEnable = true;
            }
            else if (Booking.Estado.IdEstado == 2)
            {
                IsAcceptEnable = false;
                IsCancelEnable = true;
            }
            else if (Booking.Estado.IdEstado == 3)
            {
                IsAcceptEnable = false;
                IsCancelEnable = false;
            }

        }

        #endregion Initialize

        #region Methods

        public async Task AcceptBooking()
        {
            try
            {
                JObject vals =
                new JObject(
                new JProperty("IdEstado", 2)
                );
                string Json = vals.ToString();
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(Booking.IdReservas.ToString());
                APIResponse response = await UpdateEstado.EjecutarEstrategia(null, parametros, Json);
                if (response.IsSuccess)
                {

                    var page = Application.Current.MainPage.Navigation.NavigationStack[1] as NavigationPage;
                    var context = page.CurrentPage.BindingContext as HostTabbedViewModel;
                    var hostcontext = context.HostBookingsViewModel as HostBookingsViewModel;
                    await hostcontext.ListaReserva();
                    Booking.Estado.IdEstado = 2;
                    Booking.Estado.Estado = "Aceptada";
                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {
                }
            }
            catch (Exception)
            {
            }
        }

        public async Task CancelBooking()
        {
            try
            {
                JObject vals =
                new JObject(
                new JProperty("IdEstado", 3)
                );
                string Json = vals.ToString();
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(Booking.IdReservas.ToString());
                APIResponse response = await UpdateEstado.EjecutarEstrategia(null, parametros, Json);
                if (response.IsSuccess)
                {
                    var page = Application.Current.MainPage.Navigation.NavigationStack[1] as NavigationPage;
                    var context = page.CurrentPage.BindingContext as HostTabbedViewModel;
                    var hostcontext = context.HostBookingsViewModel as HostBookingsViewModel;
                    await hostcontext.ListaReserva();
                    Booking.Estado.IdEstado = 3;
                    Booking.Estado.Estado = "Rechazada";
                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {
                }
            }
            catch (Exception)
            {
            }
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }
        #endregion Methods
    }
}
