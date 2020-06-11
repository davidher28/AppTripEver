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
        public AddFechasViewModel()
        {
            BookingState = new EstadoModel();
            Horario = new HorarioModel();
            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            Service = new ServiciosModel(Horario, Host);
            Booking = new ReservasModel(BookingState, Service, Usuario);
            NavigationService = new NavigationService();
            InitializeCommands();
            InitializeRequest();
            InitializeFields();
        }

        public void InitializeRequest()
        {

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
        }

        #endregion Initialize

        #region

        public async Task BookServiceForm()
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
            if (mesInicio != mesFin & diferenciaMes==1)
            {
                if(mesInicio == 1 | mesInicio == 3 | mesInicio == 5 | mesInicio == 7 | mesInicio == 8 | mesInicio == 10 | mesInicio == 12)
                {
                    NumNoches = (31 - diaInicio) + diaFin;
                }
                else if(mesInicio == 2)
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

                NumNoches = 30*(dir-1) + (30 - diaInicio) + diaFin;
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

            CheckOutView popUp = new CheckOutView();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync2(Usuario, Booking);
            await PopupNavigation.Instance.PushAsync(popUp);
        }


        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
            //await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion Methods
    }
}
