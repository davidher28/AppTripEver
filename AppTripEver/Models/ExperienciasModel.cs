using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
    public class ExperienciasModel : ServiciosModel
    {
        #region Properties
        public List<ActividadesModel> Agenda;
        #endregion Properties
        #region Initialize
        public ExperienciasModel(List<ActividadesModel> Agenda)
        {
            this.Agenda = Agenda;
        }
        #endregion Initialize
        #region Getters & Setters
        #endregion Getters & Setters
    }
}   