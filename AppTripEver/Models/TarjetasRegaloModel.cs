using AppTripEver.Services.Propagation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
   public class TarjetasRegaloModel : BaseModel
    {
        #region Properties
        public long IdTarjetasRegalo { get; set; }
        public string Direccion { get; set; }
        public UsuarioModel Comprador { get; set; }
        public UsuarioModel Destinatario { get; set; }
        private bool activo { get; set; }
        public string FechaVencimiento { get; set; }
        #endregion Properties

        #region Initialize
        public TarjetasRegaloModel(UsuarioModel Comprador)
        {
            this.Comprador = Comprador;
        }
        #endregion Initialize

        #region Getters & Setters
        public bool Activo
        {
            get { return activo; }
            set
            {
                activo = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters 
    }
}
