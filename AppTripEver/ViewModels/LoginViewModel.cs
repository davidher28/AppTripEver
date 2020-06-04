﻿using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Validation;
using AppTripEver.Validation.Rules;
using AppTripEver.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Request

        public ElegirRequest<BaseModel> GetUsuario_Name { get; set; }

        #endregion Request 

        #region Commands
        public ICommand IniciarSesionCommand { get; set; }

        #endregion Commands

        #region Properties
        public MessageViewPop PopUp { get; set; }

        public ValidatableObject<string> ContraUsuario { get; set; }
        public ValidatableObject<string> NombreUsuario { get; set; }

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private bool isUsuarioEnable;

        private bool isContraEnable;

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

        public bool IsUsuarioEnable
        {
            get { return isUsuarioEnable; }
            set
            {
                isUsuarioEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsContraEnable
        {
            get { return isContraEnable; }
            set
            {
                isContraEnable = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters/Setters

        #region Initialize
        public LoginViewModel()
        {
            PopUp = new MessageViewPop();
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            IsUsuarioEnable = false;
            IsContraEnable = false;
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
            NavigationService = new NavigationService();
        }

        public void InitializeRequest()
        {
            string urlUsuario_Name = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_USUARIO_NOMBRE;

            GetUsuario_Name = new ElegirRequest<BaseModel>();
            GetUsuario_Name.ElegirEstrategia("GET", urlUsuario_Name);

        }

        public void InitializeCommands()
        {
            IniciarSesionCommand = new Command(async () => await Login(), () => true);
        }

        public void InitializeFields()
        {            
            NombreUsuario = new ValidatableObject<string>();
            ContraUsuario = new ValidatableObject<string>();
            NombreUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El nombre del usuario es Obligatorio" });
            ContraUsuario.Validation.Add(new RequiredRule<string> { ValidationMessage = "El id es Obligatorio" });
        }

        #endregion Initialize

        #region Methods
        public async Task Login()
        {

            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(NombreUsuario.Value);
                parametros.Parametros.Add(ContraUsuario.Value);
                APIResponse response = await GetUsuario_Name.EjecutarEstrategia(null, parametros);
                Usuario = JsonConvert.DeserializeObject<UsuarioModel>(response.Response);
                if (response.IsSuccess)
                {                    
                    IsUsuarioEnable = true;
                    IsContraEnable = true;
                    await NavigationService.PushPage(new ChooseView(), Usuario);
                }
                else
                {
                    ((MessageViewModel)PopUp.BindingContext).Message = "Datos incorrectos.";
                }
            }
            catch(Exception)
            {
                ((MessageViewModel)PopUp.BindingContext).Message = "Sistema no disponible en este momento.";
            }
        }

        #endregion Methods
    }
}