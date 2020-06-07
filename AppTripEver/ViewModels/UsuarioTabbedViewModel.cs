﻿using AppTripEver.Configuration;
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
using System.Threading;

namespace AppTripEver.ViewModels
{
    class UsuarioTabbedViewModel : BaseViewModel
    {
        #region Request


        #endregion Request 

        #region Commands

        #endregion Commands

        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

        public ServicesViewModel ServicesViewModel { get; set; }

        public FavoritosViewModel FavoritosViewModel { get; set; }

        public ReseñasViewModel ReseñasViewModel { get; set; }

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

        #endregion Getters/Setters

        #region Initialize
        public UsuarioTabbedViewModel()
        {
            //InitializeCommands();
            NavigationService = new NavigationService();
            ServicesViewModel= new ServicesViewModel();
        }
        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            Usuario = usuario;
            await ServicesViewModel.ConstructorAsync(Usuario);
        }
        #endregion Initialize

        #region Methods

        #endregion Methods
    }
}
