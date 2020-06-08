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
    public class MessageViewModel : BaseViewModel
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

        public MessageViewModel()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
            //LoginViewModel = new LoginViewModel();
            Login = new LoginModel() { Nombre = null, Contra = null };
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var message = parameters as MessageModel;
            Message = message;
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
            var context = Application.Current.MainPage.Navigation.NavigationStack[0].BindingContext;
            await ((BaseViewModel)context).ConstructorAsync(Login);
            await Application.Current.MainPage.Navigation.PopAsync();
            //await LoginViewModel.ConstructorAsync(Login);
            //var navigationCount = Application.Current.MainPage.Navigation.NavigationStack.Count - 1 ;
            //Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[navigationCount]);
            //Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[navigationCount]);

            //for (var count = 1; count <= navigationCount; count++)
            //{
            //    if (Application.Current.MainPage.Navigation.NavigationStack.Count == 0)
            //        return;
            //    var page = Application.Current.MainPage.Navigation.NavigationStack[navigationCount - count];
            //    Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[navigationCount - count]);
            //}
            //await Application.Current.MainPage.Navigation.PushModalAsync(new LoginView());
        }
    }
}