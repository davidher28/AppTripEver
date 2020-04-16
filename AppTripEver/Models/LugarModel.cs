using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class LugarModel : NotificationObject 
    {
        #region Properties
        public long ID { get; set; }
        public string Direccion { get; set; }
        public string Historia { get; set; }
        public string Descripcion { get; set; }
        /* public ActividadesModel Actividad { get; set; } Creo que esto no va, fue lo que no pregunté */

        #endregion Properties

        #region Initialize 
        public LugarModel() { }
        #endregion Initialize 

        #region Getters & Setters 
        #endregion Getters & Setters 
    }
}     