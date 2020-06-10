﻿using System;
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


        #endregion Getters/Setters

        #region Initialize
        public CheckOutViewModel()
        {
            InitializeCommands();
            InitializeRequest();
        }

        public override async Task ConstructorAsync2(object parameters, object parameters2)
        {
            var usuario = parameters as UsuarioModel;
            var booking = parameters2 as ReservasModel;
            Usuario = usuario;
            Booking = booking;
        }

        public void InitializeRequest()
        {
            string urlPostBooking = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_ALL_SERVICIOS_ID;
            string urlUpdateWallet = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_ALL_SERVICIOS_ID;

            PostBooking = new ElegirRequest<BaseModel>();
            PostBooking.ElegirEstrategia("POST", urlPostBooking);

            UpdateWallet = new ElegirRequest<BaseModel>();
            UpdateWallet.ElegirEstrategia("POST", urlUpdateWallet);

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
                    parametros.Parametros.Add(Usuario.IdUsuario.ToString());
                    APIResponse response1 = await UpdateWallet.EjecutarEstrategia(Cartera, parametros, Json2);
                    if (response1.IsSuccess)
                    {
                        Message.Message = "Reserva realizada exitosamente";
                        MessageViewPop popUp = new MessageViewPop();
                        var viewModel = popUp.BindingContext;
                        await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                        await PopupNavigation.Instance.PushAsync(popUp);
                    }
                    else
                    {
                        Message.Message = "Reserva no creada";
                        MessageViewPop popUp = new MessageViewPop();
                        var viewModel = popUp.BindingContext;
                        await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                        await PopupNavigation.Instance.PushAsync(popUp);
                    }                  
                }
                else
                {
                    Message.Message = "Reserva no creada";
                    MessageViewPop popUp = new MessageViewPop();
                    var viewModel = popUp.BindingContext;
                    await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                    await PopupNavigation.Instance.PushAsync(popUp);
                }
            }
            else
            {
                Message.Message = "Fondos Insuficientes";
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
