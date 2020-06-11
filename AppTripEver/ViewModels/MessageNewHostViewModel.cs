using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Models;
using AppTripEver.Services.Propagation;
using AppTripEver.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class MessageNewHostViewModel : BaseViewModel
    {
        #region Properties
        private MessageModel message;

        public LoginViewModel LoginViewModel { get; set; }

        private LoginModel login;

        public ICommand CloseCommand { get; set; }
        #endregion Properties

        #region Getters y Setters

        public LoginModel Login
        {
            get { return login; }
            set
            {
                login = value;
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
        #endregion Getters y Setters

        public MessageNewHostViewModel()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
            Login = new LoginModel() { Nombre = null, Contra = null };
            Message = new MessageModel();
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var message = parameters as MessageModel;
            Message = message;
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
            await PopupNavigation.Instance.PopAsync();
        }
    }
}