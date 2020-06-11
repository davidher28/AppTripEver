using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Models;
using AppTripEver.Configuration;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace AppTripEver.ViewModels
{
    public class CheckOutViewModel : BaseViewModel
    {

        #region Request

        public ElegirRequest<BaseModel> PostBooking { get; set; }

        public ElegirRequest<BaseModel> UpdateWallet { get; set; }

        #endregion Request

        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private ReservasModel booking;

        private MessageModel message;

        private int precio;

        private string tipo;

        private int labelTipo;

        private string labelServicio;

        public ICommand CloseCommand { get; set; }

        public ICommand BookingCommand { get; set; }

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

        public int Precio
        {
            get { return precio; }
            set
            {
                precio = value;
                OnPropertyChanged();
            }
        }

        public int LabelTipo
        {
            get { return labelTipo; }
            set
            {
                labelTipo = value;
                OnPropertyChanged();
            }
        }

        public string LabelServicio
        {
            get { return labelServicio; }
            set
            {
                labelServicio = value;
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

        public MessageModel Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public string Tipo
        {
            get { return tipo; }
            set
            {
                tipo = value;
                OnPropertyChanged();
            }
        }


        #endregion Getters/Setters

        #region Initialize
        public CheckOutViewModel()
        {
            Message = new MessageModel { Message = "Reserva creada correctamente" };
            InitializeCommands();
            InitializeRequest();
        }

        public override async Task ConstructorAsync2(object parameters, object parameters2)
        {
            var usuario = parameters as UsuarioModel;
            var booking = parameters2 as ReservasModel;
            Usuario = usuario;
            Booking = booking;
            if (Booking.Servicio.TipoServicio == 1)
            {
               Precio = Booking.Valor / Booking.NumNoches;
               Tipo = "noche";
               LabelTipo = Booking.NumNoches;
               LabelServicio = "Hospedaje";
            }
            else if (Booking.Servicio.TipoServicio == 2)
            {
                Precio = Booking.Valor / Booking.NumPersonas;
                Tipo = "persona";
                LabelTipo = Booking.NumPersonas;
                LabelServicio = "Experiencia";
            }
            
        }

        public void InitializeRequest()
        {
            string urlPostBooking = Endpoints.URL_SERVIDOR + Endpoints.CREAR_RESERVA;
            string urlUpdateWallet = Endpoints.URL_SERVIDOR + Endpoints.ACTUALIZAR_CARTERA;

            PostBooking = new ElegirRequest<BaseModel>();
            PostBooking.ElegirEstrategia("POST", urlPostBooking);

            UpdateWallet = new ElegirRequest<BaseModel>();
            UpdateWallet.ElegirEstrategia("PUT", urlUpdateWallet);

        }

        public void InitializeCommands()
        {
            BookingCommand = new Command(async () => await CreateBooking(), () => true);
            CloseCommand = new Command(async () => await Close(), () => true);

        }

        #endregion Initialize

        #region Methods

        public async Task CreateBooking()
        {
            if(Booking.Valor <= Usuario.Cartera.MontoTotal)
            {
                JObject vals =
                new JObject(
                    new JProperty("numPersonas", Booking.NumPersonas),
                    new JProperty("IdEstado", Booking.Estado.IdEstado),
                    new JProperty("IdUsuario", Booking.Cliente.IdUsuario),
                    new JProperty("IdServicio", Booking.Servicio.IdServicio),
                    new JProperty("fechaInicio", Booking.FechaInicio),
                    new JProperty("fechaFin", Booking.FechaFin),
                    new JProperty("valor", Booking.Valor),
                    new JProperty("numNoches", Booking.NumNoches)
                    );
                string Json = vals.ToString();
                APIResponse response = await PostBooking.EjecutarEstrategia(Booking, null, Json);
                if (response.IsSuccess)
                {
                    Usuario.Cartera.MontoTotal = Usuario.Cartera.MontoTotal - Booking.Valor;

                    JObject vals2 =
                        new JObject(
                        new JProperty("Monto", Usuario.Cartera.MontoTotal),
                        new JProperty("IdUsuario", Usuario.IdUsuario)
                        );
                    string Json2 = vals2.ToString();
                    ParametersRequest parametros = new ParametersRequest();
                    parametros.Parametros.Add(Usuario.Cartera.IdCartera.ToString());
                    APIResponse response1 = await UpdateWallet.EjecutarEstrategia(Cartera, parametros, Json2);
                    if (response1.IsSuccess)
                    {
                        PopGeneralView popUp = new PopGeneralView();
                        var viewModel = popUp.BindingContext;
                        await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                        await PopupNavigation.Instance.PushAsync(popUp);
                    }
                    else
                    {
                        Message.Message = "Reserva no creada";
                        PopGeneralView popUp = new PopGeneralView();
                        var viewModel = popUp.BindingContext;
                        await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                        await PopupNavigation.Instance.PushAsync(popUp);
                    }                  
                }
                else
                {
                    Message.Message = "Reserva no creada";
                    PopGeneralView popUp = new PopGeneralView();
                    var viewModel = popUp.BindingContext;
                    await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                    await PopupNavigation.Instance.PushAsync(popUp);
                }
            }
            else
            {
                Message.Message = "Fondos Insuficientes";
                PopGeneralView popUp = new PopGeneralView();
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
