using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using AppTripEver.Services.Propagation;
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
            CloseCommand = new Command(() => Close(), () => true);
        }

        public void Close()
        {

        }
    }
}