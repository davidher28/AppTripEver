﻿using System;
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
    public class MessageServicioViewModel : BaseViewModel
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

        public MessageServicioViewModel()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
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
        }
    }
}

