using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Models;
using AppTripEver.Services.Propagation;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        #region Properties
        private MessageModel message;

        public ICommand CloseCommand { get; set; }
        #endregion Properties

        #region Getters y Setters

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

        public MessageViewModel()
        {
            CloseCommand = new Command(() => Close(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var message = parameters as MessageModel;
            Message = message;
        }

        public void Close()
        {

        }
    }
}