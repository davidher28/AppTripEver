using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Validation;
using AppTripEver.Views;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
namespace AppTripEver.ViewModels
{
    public class InfoHostViewModel : BaseViewModel
    {
        #region Request

        public ElegirRequest<BaseModel> UpdateHost { get; set; }

        #endregion Request


        #region Commands

        public ICommand CloseCommand { get; set; }

        public ICommand UpdateUserCommand { get; set; }

        #endregion Commands


        #region Properties

        private UsuarioHostModel host;

        private CarteraModel cartera;

        private MessageModel message;

        public ValidatableObject<string> NoCuentaHost { get; set; }
        public ValidatableObject<string> MailHost { get; set; }

        #endregion Properties


        #region Getters & Setters

        public UsuarioHostModel Host
        {
            get { return host; }
            set
            {
                host = value;
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

        public InfoHostViewModel()
        {
            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            Message = new MessageModel() { Message = "Actualización exitosa" };
            InitializeCommands();
            InitializeRequest();
            InitializeFields();
            NoCuentaHost.Value = Host.NoCuenta;
            MailHost.Value = Host.MailHost;
        }

        public void InitializeRequest()
        {

            string urlHostUpdate = Endpoints.URL_SERVIDOR + Endpoints.EDITAR_HOST;

            UpdateHost = new ElegirRequest<BaseModel>();
            UpdateHost.ElegirEstrategia("PUT", urlHostUpdate);
        }

        public void InitializeFields()
        {
            NoCuentaHost = new ValidatableObject<string>();
            MailHost = new ValidatableObject<string>();
        }

        public void InitializeCommands()
        {
            UpdateUserCommand = new Command(async () => await UpdateUserForm(), () => true);
            CloseCommand = new Command(async () => await Close(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var host = parameters as UsuarioHostModel;
            Host = host;
        }

        #endregion Initialize


        #region Methods

        public async Task UpdateUserForm()
        {
            JObject vals =
                new JObject(
                    new JProperty("NoCuenta", NoCuentaHost.Value ?? Host.NoCuenta),
                    new JProperty("MailHost", MailHost.Value ?? Host.MailHost),
                    new JProperty("IdUsuario", Host.IdUsuario)
                    );
            string Json = vals.ToString();
            ParametersRequest parametros = new ParametersRequest();
            parametros.Parametros.Add(Host.IdHost.ToString());
            APIResponse response = await UpdateHost.EjecutarEstrategia(Host, parametros, Json);
            if (response.IsSuccess)
            {
                Host.NoCuenta = NoCuentaHost.Value ?? Host.NoCuenta;
                Host.MailHost = MailHost.Value ?? Host.MailHost;

                PopGeneralView popUp = new PopGeneralView();
                var viewModel = popUp.BindingContext;
                await ((BaseViewModel)viewModel).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(popUp);
            }
            else
            {
                Message.Message = "Actualización no exitosa";
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
