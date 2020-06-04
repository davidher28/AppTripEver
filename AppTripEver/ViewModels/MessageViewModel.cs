using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Services.Propagation;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class MessageViewModel : NotificationObject
    {
        #region Properties
        private string message;

        public ICommand CloseCommand { get; set; }
        #endregion Properties

        #region Getters y Setters

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters y Setters

        public MessageViewModel()
        {
            CloseCommand = new Command( async () => await Close(), () => true);
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}