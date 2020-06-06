using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AppTripEver.Models
{
    public class UsuarioHostModel : UsuarioModel
    {
        #region Properties

        [JsonProperty("IdHost")]
        private Nullable<long> idHost { get; set; }

        [JsonProperty("NoCuenta")]
        private string noCuenta { get; set; }

        [JsonProperty("MailHost")]
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

        [JsonIgnore]
        public Nullable<long> IdHost
        {
            get { return idHost; }
            set
            {
                idHost = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string NoCuenta
        {
            get { return noCuenta; }
            set
            {
                noCuenta = value;
                OnPropertyChanged();
            }
        }
        
        [JsonIgnore]
        public string MailHost
        {
            get { return mailHost; }
            set
            {
                mailHost = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
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
