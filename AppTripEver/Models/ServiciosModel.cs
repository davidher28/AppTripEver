using AppTripEver.Services.Propagation;
using System.Collections.Generic;

namespace AppTripEver.Models
{
    public class ServiciosModel : BaseModel
    {
        #region Properties
        public long IdServicio { get; set; }
        private string titulo { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        private int numMaxPersonas { get; set; }
        public string Ubicacion { get; set; }
        private string descripcion { get; set; }
        private HorarioModel fecha { get; set; }
        private int precio { get; set; }
        private List<ReseñasModel> reseñas { get; set; }
        
        public UsuarioHostModel Creador { get; set; }

        public ReservasModel Reserva { get; set; } 
        #endregion Properties

        #region Initialize
        public ServiciosModel(HorarioModel fecha, UsuarioHostModel Creador)
        {
            this.fecha = fecha;
            this.Creador = Creador;
        }
        #endregion Initialize

        #region Getters & Setters
        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnPropertyChanged();
            }
        }
        public int NumMaxPersonas
        {
            get { return numMaxPersonas; }
            set
            {
                numMaxPersonas = value;
                OnPropertyChanged();
            }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
                OnPropertyChanged();
            }
        }
        public HorarioModel Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                OnPropertyChanged();
            }
        }
        public int Precio
        {
            get { return precio; }
            set
            {
                precio = value;
                OnPropertyChanged();
            }
        }
        public List<ReseñasModel> Reseñas
        {
            get { return reseñas; }
            set
            {
                reseñas = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters 
    }
} 