
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
    public class BookingUserInfoViewModel : BaseViewModel
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

        private string background;

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

        public string Background
        {
            get { return background; }
            set
            {
                background = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters

        #region Initialize
        public BookingUserInfoViewModel()
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

        }

        public void InitializeFields()
        {

        }

        public void InitializeCommands()
        {
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
            if (Booking.Estado.IdEstado == 1)
            {
                Background = "#677BA6";
            }
            else if (Booking.Estado.IdEstado == 2)
            {
                Background = "#96E3AC";
            }
            else if (Booking.Estado.IdEstado == 3)
            {
                Background = "#E0897A";
            }
        }

        #endregion Initialize

        #region Methods


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
