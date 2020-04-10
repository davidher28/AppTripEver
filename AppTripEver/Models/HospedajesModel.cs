using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
    public class HospedajesModel : ServiciosModel
    {
        #region Properties
        private int precioNoche { get; set; }
        public string TipoAcomodacion { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public string EspecificacionDomicilio { get; set; }
        #endregion Properties

        #region Initialize
        #endregion Initialize
        #region Getters & Setters
        public int PrecioNoche
        {
            get { return precioNoche; }
            set
            {
                precioNoche = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters 
    }
}
