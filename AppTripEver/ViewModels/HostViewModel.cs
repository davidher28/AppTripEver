﻿using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class HostViewModel : BaseViewModel
    {

        #region Request
        public ElegirRequest<BaseModel> GetServiciosHospedajeHost { get; set; }
        public ElegirRequest<BaseModel> GetServiciosExperienciaHost { get; set; }
        public ElegirRequest<BaseModel> GetFecha { get; set; }
        
        #endregion Request 

        #region Commands

        public ICommand CrearServicioCommand { get; set; }
        
        public ICommand SelectServiceCommand { get; set; }

        public ICommand SelectHospedajeServiceCommand { get; set; }

        public ICommand CarteraCommand { get; set; }

        public ICommand UsuarioInfoCommand { get; set; }

        #endregion Commands


        #region Properties

        private CarteraModel cartera;

        private UsuarioHostModel host;

        private ServiciosModel servicioActual;

        private HorarioModel horario;

        public NavigationService NavigationService { get; set; }

        private ServiciosModel servicioExperienciaActual;

        private ObservableCollection<ServiciosModel> serviciosHospedajeHost;

        private ObservableCollection<ServiciosModel> serviciosExperienciaHost;

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
        public ServiciosModel ServicioExperienciaActual
        {
            get { return servicioExperienciaActual; }
            set
            {
                servicioExperienciaActual = value;
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

        public ObservableCollection<ServiciosModel> ServiciosHospedajeHost
        {
            get { return serviciosHospedajeHost; }
            set
            {
                serviciosHospedajeHost = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ServiciosModel> ServiciosExperienciaHost
        {
            get { return serviciosExperienciaHost; }
            set
            {
                serviciosExperienciaHost = value;
                OnPropertyChanged();
            }
        }

        public ServiciosModel ServicioActual
        {
            get { return servicioActual; }
            set
            {
                servicioActual = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters

        #region Initialize
        public HostViewModel()
        {

            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);         
            NavigationService = new NavigationService();
            InitializeCommands();
            InitializeRequest();
            
        }

        public void InitializeRequest()
        {
            string urlServiciosIdHost = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_ALL_SERVICIOS_ID_HOST  ;
            string urlGetFecha = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_FECHA;

            GetServiciosHospedajeHost = new ElegirRequest<BaseModel>();
            GetServiciosHospedajeHost.ElegirEstrategia("GET", urlServiciosIdHost);

            GetServiciosExperienciaHost = new ElegirRequest<BaseModel>();
            GetServiciosExperienciaHost.ElegirEstrategia("GET", urlServiciosIdHost);

            
            GetFecha = new ElegirRequest<BaseModel>();
            GetFecha.ElegirEstrategia("GET", urlGetFecha);
        }

        public void InitializeCommands()
        {
            CrearServicioCommand = new Command(async () => await CrearServicio(), () => true);
            SelectServiceCommand = new Command(async () => await SelectService(), () => true);
            SelectHospedajeServiceCommand = new Command(async () => await SelectHospedajeService(), () => true);
            CarteraCommand = new Command(async () => await DisplayCartera(), () => true);
            UsuarioInfoCommand = new Command(async () => await DisplayUsuario(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var host = parameters as UsuarioHostModel;
            Host = host;
            await ListaServiciosHospedajeHost();
            await ListaServiciosExperienciaHost();
            Console.WriteLine(Host.Nombre);
        }

        #endregion Initialize

        #region Methods
        public async Task CrearServicio()
        {
            
            await NavigationService.PushPage(new CrearServicioView(),Host);
        }

        public async Task ListaServiciosHospedajeHost()
        {
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add("1");
                parametros.Parametros.Add(Host.IdHost.ToString());
                APIResponse response = await GetServiciosHospedajeHost.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    List<ServiciosModel> listaServicios = JsonConvert.DeserializeObject<List<ServiciosModel>>
                        (response.Response);
                    ServiciosHospedajeHost = new ObservableCollection<ServiciosModel>(listaServicios);
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }

        public async Task ListaServiciosExperienciaHost()
        {
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add("2");
                parametros.Parametros.Add(Host.IdHost.ToString());
                APIResponse response = await GetServiciosExperienciaHost.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    List<ServiciosModel> listaServicios = JsonConvert.DeserializeObject<List<ServiciosModel>>
                        (response.Response);
                    ServiciosExperienciaHost = new ObservableCollection<ServiciosModel>(listaServicios);

                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }

        public async Task SelectService()
        {
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(ServicioActual.IdServicio.ToString());
                APIResponse response = await GetFecha.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    Horario = JsonConvert.DeserializeObject<HorarioModel>(response.Response);
                    ServicioActual.Fecha = Horario;
                }
            }
            catch (Exception)
            {

            }
            ServiceEditViewPop popUp = new ServiceEditViewPop();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync2(Host, ServicioActual);
            await PopupNavigation.Instance.PushAsync(popUp);
        }

        public async Task SelectHospedajeService()
        {

            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(ServicioExperienciaActual.IdServicio.ToString());
                APIResponse response = await GetFecha.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    Horario = JsonConvert.DeserializeObject<HorarioModel>(response.Response);
                    ServicioExperienciaActual.Fecha = Horario;
                }
            }
            catch (Exception)
            {

            }
            ServiceEditViewPop popUp = new ServiceEditViewPop();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync2(Host, ServicioExperienciaActual);
            await PopupNavigation.Instance.PushAsync(popUp);
        }

        public async Task DisplayCartera()
        {
            CarteraHostView popUp = new CarteraHostView();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync(Host);
            await PopupNavigation.Instance.PushAsync(popUp);
        }

        public async Task DisplayUsuario()
        {
            InfoHostView popUp = new InfoHostView();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync(Host);
            await PopupNavigation.Instance.PushAsync(popUp);
        }

        #endregion Methods
    }
}
