using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
    public class UsuarioClienteModel : UsuarioModel
    {
        #region Properties
        #endregion Properties
        #region Initialize
        public UsuarioClienteModel(CarteraModel Cartera) : base(Cartera)
        {
            this.Cartera = Cartera;
        }
        #endregion Initialize
        #region Getters & Setters
        #endregion Getters & Setters
    }
}
 