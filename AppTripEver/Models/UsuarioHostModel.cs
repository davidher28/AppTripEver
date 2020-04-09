using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
    public class UsuarioHostModel : UsuarioModel
    {
        #region Properties
        private long noCuenta { get; set; }
        private string direcFacturacion { get; set; }  
        private List<ServiciosModel> servicios { get; set; }
        #endregion Properties        

        #region Initialize
        public UsuarioHostModel(CarteraModel Cartera) : base(Cartera)
        {
            this.Cartera = Cartera;
        }
        #endregion Initialize

        #region Getters & Setters
        public long NoCuenta
        {
            get { return noCuenta; }
            set
            {
                noCuenta = value;
                OnPropertyChanged();
            }
        }
        public string DirecFacturacion
        {
            get { return direcFacturacion; }
            set
            {
                direcFacturacion = value;
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
