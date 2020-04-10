using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class ActividadesModel : NotificationObject
    {
        #region Properties
        public long ID { get; set; }
        private string duracion { get; set; }
        private LugarModel lugar { get; set; }
        private int edadMinima { get; set; }
        private string descripcion { get; set; }
        private int precio { get; set; }

        #endregion Properties 

        #region Initialize
        public ActividadesModel(LugarModel lugar)
        {
            this.lugar = lugar;
        }
        #endregion Initialize 

        #region Getters & Setters
        public string Duracion
        {
            get { return duracion; }
            set
            {
                duracion = value;
                OnPropertyChanged();
            }
        }
        public LugarModel Lugar
        {
            get { return lugar; }
            set
            {
                lugar = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters
    }
}