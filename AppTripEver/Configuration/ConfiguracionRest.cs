using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Configuration
{
    public class ConfiguracionRest
    {
        #region Properties
        private readonly string NameSpaceRest;
        public Dictionary<string, string> VerbosConfiguracion { get; set; }
        #endregion Properties

        #region Initialize
        public ConfiguracionRest() 
        {
            NameSpaceRest = "AppTripEver.Services.APIRest.";
            InicializarVerbosConfiguracion();
        }
        #endregion Initialize

        #region Métodos
        private void InicializarVerbosConfiguracion()
        {
            VerbosConfiguracion = new Dictionary<string, string>();
            VerbosConfiguracion.Add("GET", string.Concat(NameSpaceRest, "RequestParametros`1"));
            VerbosConfiguracion.Add("DELETE", string.Concat(NameSpaceRest, "RequestParametros`1"));
            VerbosConfiguracion.Add("POST", string.Concat(NameSpaceRest, "RequestBody`1"));
            VerbosConfiguracion.Add("PUT", string.Concat(NameSpaceRest, "RequestBody`1"));
        }
        #endregion Métodos
    }
}
