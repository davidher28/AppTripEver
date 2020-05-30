using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class LugarModel : BaseModel
    {
        #region Properties
        public long IdLugar { get; set; }
        public string Direccion { get; set; }
        public string Historia { get; set; }
        public string Descripcion { get; set; }
        public ActividadesModel Actividad { get; set; }

        #endregion Properties

        #region Initialize 
        public LugarModel() { }
        #endregion Initialize 

        #region Getters & Setters 
        #endregion Getters & Setters 
    }
}     