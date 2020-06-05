using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AppTripEver.Models
{
    public class UsuarioHostModel : UsuarioModel
    {
        #region Properties
        [JsonIgnore]
        private string noCuenta { get; set; }
        [JsonIgnore]
        private string mailHost { get; set; }
        [JsonIgnore]
        private List<ServiciosModel> servicios { get; set; }
        #endregion Properties        

        #region Initialize
        public UsuarioHostModel(CarteraModel Cartera) : base(Cartera)
        {
            this.Cartera = Cartera;
        }
        #endregion Initialize

        #region Getters & Setters
        public string NoCuenta
        {
            get { return noCuenta; }
            set
            {
                noCuenta = value;
                OnPropertyChanged();
            }
        }
        public string MailHost
        {
            get { return mailHost; }
            set
            {
                mailHost = value;
                OnPropertyChanged();
            }
        }
        public List<ServiciosModel> Servicios
        {
            get { return servicios; }
            set
            {
                servicios = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Seters 
    }
}
