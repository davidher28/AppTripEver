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

        public ValidatableObject<string> FechaInicio { get; set; }

        public ValidatableObject<string> FechaFinal { get; set; }

        public ValidatableObject<string> NumPersonas { get; set; }

        public NavigationService NavigationService { get; set; }

        private string labelTipo;

        private string labelHoraI;

        private string labelHoraF;

        private string labelFechaI;

        private string labelFechaF;

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

        public string LabelTipo
        {
            get { return labelTipo; }
            set
            {
                labelTipo = value;
                OnPropertyChanged();
            }
        }

        public string LabelHoraI
        {
            get { return labelHoraI; }
            set
            {
                labelHoraI = value;
                OnPropertyChanged();
            }
        }

        public string LabelHoraF
        {
            get { return labelHoraF; }
            set
            {
                labelHoraF = value;
                OnPropertyChanged();
            }
        }

        public string LabelFechaI
        {
            get { return labelFechaI; }
            set
            {
                labelFechaI = value;
                OnPropertyChanged();
            }
        }

        public string LabelFechaF
        {
            get { return labelFechaF; }
            set
            {
                labelFechaF = value;
                OnPropertyChanged();
            }
        }


        #endregion Getters/Setters

        #region Initialize
        public ServiceInformationViewModel()
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
            string urlBoking = Endpoints.URL_SERVIDOR + Endpoints.CREAR_RESERVA;

            PostBooking = new ElegirRequest<BaseModel>();
            PostBooking.ElegirEstrategia("POST", urlBoking);

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
            BookingCommand = new Command(async () => await Book(), () => true);
            CloseCommand = new Command(async () => await Close(), () => true);
        }

        public override async Task ConstructorAsync2(object parameters, object parameters2)
        {
            var usuario = parameters as UsuarioModel;
            var service = parameters2 as ServiciosModel;
            Usuario = usuario;
            Service = service;
            if (Service.TipoServicio == 1){
                LabelTipo = "Noche";
                LabelHoraI = "Check In:";
                LabelHoraF = "Check Out:";
                LabelFechaI = "Disponible Desde:";
                LabelFechaF = "Disponible Hasta:";
            }
            else
            {
                LabelTipo = "Persona:";
                LabelHoraI = "Hora Inicio:";
                LabelHoraF = "Hora Fin:";
                LabelFechaI = "Inicia:";
                LabelFechaF = "Finaliza:";
            }
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(Service.IdServicio.ToString());
                APIResponse response = await GetFecha.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    Horario = JsonConvert.DeserializeObject<HorarioModel>(response.Response);
                    Service.Fecha = Horario;
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }

        #endregion Initialize

        #region Methods

        public async Task Book()
        {
            AddFechasView view = new AddFechasView();
            var context = view.BindingContext;
            await ((BaseViewModel)context).ConstructorAsync2(Usuario, Service);
            PopupNavigation.Instance.PushAsync(view);
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
            //var page = Application.Current.MainPage.Navigation.NavigationStack[1] as NavigationPage;
            //var page2 = page.Pages.<>3_source[0] as NavigationPage;
            //var context = page.CurrentPage.BindingContext as UsuarioTabbedViewModel;
            //var hostcontext = context.ServicesViewModel as ServicesViewModel;
            //Device.BeginInvokeOnMainThread(() => hostcontext.CollectionView.SelectedItem = null);
        }
        #endregion Methods
    }
}
