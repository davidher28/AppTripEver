using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Configuration
{
    public class Endpoints
    {
        public static readonly string URL_SERVIDOR = "http://3.20.154.181:8080";
        public static readonly string CONSULTAR_ALL_USUARIOS = "/getUsuarios";
        public static readonly string CONSULTAR_USUARIO = "/getUsuario";
        public static readonly string CONSULTAR_USUARIO_NOMBRE = "/getUsuarioByUserContra";
        public static readonly string CREAR_USUARIO = "/createUsuario";
        public static readonly string EDITAR_USUARIO = "/updateUser";
        public static readonly string ELIMINAR_USUARIO = "/deleteUsuario";
        public static readonly string CONSULTAR_USUARIO_HOST = "/getHost";
        public static readonly string CREAR_USUARIO_HOST = "/createHost";
        public static readonly string CREAR_SERVICIO = "/createServicio";
        public static readonly string CONSULTAR_ALL_SERVICIOS = "/getServicios";
        public static readonly string CONSULTAR_ALL_SERVICIOS_ID = "/getServiciosId";
        public static readonly string CONSULTAR_ALL_SERVICIOS_ID_HOST = "/getServiciosIdHost";
        public static readonly string CREAR_RESERVA = "/createReserva";
        public static readonly string ACTUALIZAR_CARTERA = "/updateCartera";
        public static readonly string CONSULTAR_RESERVAS_USER = "/getReservaUser";
        public static readonly string CONSULTAR_RESERVAS_HOST = "/getReservaHost";
        public static readonly string CONSULTAR_FECHA = "/getHorarioServicio";
        public static readonly string EDITAR_SERVICIO = "/updateServicio";
        public static readonly string CONSULTAR_TARJETAREGALO = "/getTarjetaRegalo";
        public static readonly string EDITAR_HOST = "/updateHost";
        public static readonly string CONSULTAR_ESTADO = "/getReservaEstado";
        public static readonly string ACTUALIZAR_ESTADO_RESERVA = "/updateEstado";

    }
}
