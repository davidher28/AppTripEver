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
        public static readonly string EDITAR_USUARIO = "/updateUsuario";
        public static readonly string ELIMINAR_USUARIO = "/deleteUsuario";
        public static readonly string CONSULTAR_USUARIO_HOST = "/getHost";
        public static readonly string CREAR_USUARIO_HOST = "/createHost";
        public static readonly string CREAR_SERVICIO = "/createServicio";


    }
}
