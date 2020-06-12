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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppTripEver.Behaviors;
using Newtonsoft.Json.Linq;


namespace AppTripEver.ViewModels
{
    public class CanjearCodeViewModel : BaseViewModel
    {
        #region Request
        public ElegirRequest<BaseModel> UpdateCartera { get; set; }

        public ElegirRequest<BaseModel> CanjearCode { get; set; }

        public ValidatableObject<Nullable<int>> CodigoTarjeta { get; set; }

        #endregion Request 

        #region Commands
        public ICommand RecargarCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #endregion Commands

        #region Properties


        private UsuarioModel usuario;

        private CarteraModel cartera;

        private UsuarioHostModel host;

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

        #endregion Getters/Setters

        #region Initialize
        public CanjearCodeViewModel()
        {
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            Host = new UsuarioHostModel(Cartera);
            NavigationService = new NavigationService();
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
        }

        public void InitializeRequest()
        {
            string UrlUpdateCartera = Endpoints.URL_SERVIDOR + Endpoints.ACTUALIZAR_CARTERA;

            UpdateCartera = new ElegirRequest<BaseModel>();
            UpdateCartera.ElegirEstrategia("PUT", UrlUpdateCartera);

            string UrlCanjearCodigo = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_TARJETAREGALO;

            CanjearCode = new ElegirRequest<BaseModel>();
            CanjearCode.ElegirEstrategia("GET", UrlCanjearCodigo);



        }

        public void InitializeCommands()
        {
            RecargarCommand = new Command(async () => await Recargar(), () => true);
            CloseCommand = new Command(async () => await Close(), () => true);
        }

        public void InitializeFields()
        {
            CodigoTarjeta = new ValidatableObject<Nullable<int>>();
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            var host = parameters as UsuarioHostModel;
            Usuario = usuario;
            Host = host;
        }

        #endregion Initialize

        #region Methods

        public async Task Recargar()
        {
            try
            {
                ParametersRequest parametros2 = new ParametersRequest();
                parametros2.Parametros.Add(CodigoTarjeta.Value.ToString());
                APIResponse response = await CanjearCode.EjecutarEstrategia(null, parametros2);
                if (response.IsSuccess)
                {
                    JToken token = JObject.Parse(response.Response);
                    int nuevo = Usuario.Cartera.MontoTotal + (int)token.SelectToken("Monto");
                    JObject vals2 =
                        new JObject(
                        new JProperty("Monto", nuevo),
                        new JProperty("IdUsuario", Usuario.IdUsuario)
                        );
                    string Json2 = vals2.ToString();
                    ParametersRequest parametros = new ParametersRequest();
                    parametros.Parametros.Add(Usuario.Cartera.IdCartera.ToString());
                    APIResponse response1 = await UpdateCartera.EjecutarEstrategia(Cartera, parametros, Json2);
                    if (response1.IsSuccess)
                    {
                        var page = Application.Current.MainPage.Navigation.NavigationStack[1] as NavigationPage;
                        var context = page.CurrentPage.BindingContext as UsuarioTabbedViewModel;
                        var hostcontext = context.ServicesViewModel as ServicesViewModel;
                        hostcontext.Usuario.Cartera.MontoTotal = nuevo;
                        await PopupNavigation.Instance.PopAsync();
                    }
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
