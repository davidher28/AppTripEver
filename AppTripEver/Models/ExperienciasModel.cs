using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
    public class ExperienciasModel : ServiciosModel
    {
        #region Properties
        public List<ActividadesModel> Agenda { get; set; }
        #endregion Properties

        #region Initialize
        public ExperienciasModel(List<ActividadesModel> Agenda, HorarioModel Fecha, UsuarioHostModel Creador) : base(Fecha, Creador)
        {
            this.Agenda = Agenda;
            this.Fecha = Fecha;
            this.Creador = Creador;
        }
        #endregion Initialize

        #region Getters & Setters
        #endregion Getters & Setters
    }
}   