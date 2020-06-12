using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class AddFechasViewModel : BaseViewModel
    { 
        #region Request
        public ElegirRequest<BaseModel> PostBooking { get; set; }

        public ElegirRequest<BaseModel> GetFecha { get; set; }

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

        public int NumDias;

        public ValidatableObject<string> FechaInicio { get; set; }

        public ValidatableObject<string> FechaFinal { get; set; }

        public ValidatableObject<string> NumPersonas { get; set; }

        public int NumNoches { get; set; }

        public int Valor { get; set; }

        private bool fechaEnable;

        public bool VerificadorFecha = false;

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

        public bool FechaEnable
        {
            get { return fechaEnable; }
            set
            {
                fechaEnable = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters

        #region Initialize
        public AddFechasViewModel()
        {
            
            BookingState = new EstadoModel();
            Horario = new HorarioModel();
            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            Service = new ServiciosModel(Horario, Host);
            Booking = new ReservasModel(BookingState, Service, Usuario);
            NavigationService = new NavigationService();
            Message = new MessageModel() { Message = "Fechas no disponibles"};
            InitializeCommands();
            InitializeRequest();
            InitializeFields();
        }

        public void InitializeRequest()
        {
            string urlGetFecha = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_FECHA;
            GetFecha = new ElegirRequest<BaseModel>();
            GetFecha.ElegirEstrategia("GET", urlGetFecha);
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

        public override async Task ConstructorAsync2(object parameters, object parameters2)
        {
            var usuario = parameters as UsuarioModel;
            var service = parameters2 as ServiciosModel;
            Usuario = usuario;
            Service = service;
            if(Service.TipoServicio == 1)
            {
                FechaEnable = true;
            }
            else if (Service.TipoServicio == 2)
            {
                FechaEnable = false;
            }
        }

        #endregion Initialize

        #region

        public async Task BookServiceForm()
        {
            
            if (Service.TipoServicio == 1)
            {               
                Booking.FechaInicio = FechaInicio.Value;
                Booking.FechaFin = FechaFinal.Value;
                BookingState.IdEstado = 1;
                var splitListInicio = FechaInicio.Value.Split('-', (char)StringSplitOptions.RemoveEmptyEntries).ToList();
                var splitListFin = FechaFinal.Value.Split('-', (char)StringSplitOptions.RemoveEmptyEntries).ToList();
                Booking.NumPersonas = Int32.Parse(NumPersonas.Value);
                var mesInicio = Int32.Parse(splitListInicio[1]);
                var diaInicio = Int32.Parse(splitListInicio[2]);
                var mesFin = Int32.Parse(splitListFin[1]);
                var diaFin = Int32.Parse(splitListFin[2]);
                var diferenciaMes = mesFin - mesInicio;
                
                var splitListInicio2 = Service.Fecha.FechaInicio.Split('-', (char)StringSplitOptions.RemoveEmptyEntries).ToList();
                var splitListFin2 = Service.Fecha.FechaFinal.Split('-', (char)StringSplitOptions.RemoveEmptyEntries).ToList();
                var mesInicioService = Int32.Parse(splitListInicio2[1]);
                var diaInicioService = Int32.Parse(splitListInicio2[2]);
                var mesFinService = Int32.Parse(splitListFin2[1]);
                var diaFinService = Int32.Parse(splitListFin2[2]);

                if ((diaInicio >= diaInicioService && diaInicioService <= diaFin) && (mesInicio >= mesInicioService && mesInicioService <= mesFin))
                {
                    VerificadorFecha = true;
                }
                if (mesInicio != mesFin & diferenciaMes == 1)
                {
                    if (mesInicio == 1 | mesInicio == 3 | mesInicio == 5 | mesInicio == 7 | mesInicio == 8 | mesInicio == 10 | mesInicio == 12)
                    {
                        NumNoches = (31 - diaInicio) + diaFin;
                    }
                    else if (mesInicio == 2)
                    {
                        NumNoches = (28 - diaInicio) + diaFin;
                    }
                    else
                    {
                        NumNoches = (30 - diaInicio) + diaFin;
                    }
                }
                else if (mesInicio != mesFin & diferenciaMes > 1)
                {
                    var dir = mesFin - mesInicio;

                    NumNoches = 30 * (dir - 1) + (30 - diaInicio) + diaFin;
                }

                else if (mesInicio == mesFin)
                {
                    NumNoches = diaFin - diaInicio;
                }

                var Total = NumNoches * Service.Precio;

                Booking.NumNoches = NumNoches;
                Booking.Valor = Total;
                Booking.Servicio = Service;
                Booking.Cliente = Usuario;
                Booking.Titulo = Service.Titulo;
            }
            if(Service.TipoServicio == 2)
            {
                BookingState.IdEstado = 1;
                var Total = Int32.Parse(NumPersonas.Value) * Service.Precio;
                Booking.Valor = Total;
                Booking.FechaInicio = Service.Fecha.FechaFinal;
                Booking.FechaFin = Service.Fecha.FechaFinal;
                BookingState.IdEstado = 1;
                Booking.NumNoches = 0;
                Booking.NumPersonas = Int32.Parse(NumPersonas.Value);
                Booking.Servicio = Service;
                Booking.Cliente = Usuario;
                Booking.Titulo = Service.Titulo;
            }
            if (Service.TipoServicio == 1)
            {
                if (VerificadorFecha)
                {
                    CheckOutView popUp = new CheckOutView();
                    var viewModel = popUp.BindingContext;
                    await ((BaseViewModel)viewModel).ConstructorAsync2(Usuario, Booking);
                    await PopupNavigation.Instance.PushAsync(popUp);
                }else if (VerificadorFecha == false)
                {
                    PopGeneralView popUp = new PopGeneralView();
                    var viewModel = popUp.BindingContext;
                    await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                    await PopupNavigation.Instance.PushAsync(popUp);
                }
            }else
            {
                CheckOutView popUp = new CheckOutView();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync2(Usuario, Booking);
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
