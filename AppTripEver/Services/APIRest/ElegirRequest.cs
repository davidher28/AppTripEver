﻿using AppTripEver.Configuration;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTripEver.Services.APIRest
{
    public class ElegirRequest<T> 
    {
        #region Properties
        public Request<T> EstrategiaEnvio { get; set; }
        public ConfiguracionRest ConfiguracionRest { get; set; }
        #endregion Properties

        #region Initialize
        public ElegirRequest() 
        {
            ConfiguracionRest = new ConfiguracionRest();
        }
        #endregion Initialize

        #region Métodos
        public void ElegirEstrategia(string verbo, string url)
        {
            var diccionario = ConfiguracionRest.VerbosConfiguracion;
            string nombreClase;
            if (diccionario.TryGetValue(verbo.ToUpper(), out nombreClase))
            {
                Type tipoClase = Type.GetType(nombreClase);
                Type[] typeArgs = { typeof(T) };
                var genericClass = tipoClase.MakeGenericType(typeArgs);
                EstrategiaEnvio = (Request<T>)Activator.CreateInstance(genericClass, url, verbo.ToUpper());
            }
        }
        
        public async Task<APIResponse> EjecutarEstrategia(T objecto, ParametersRequest parametersRequest = null, string Json = null)
        {
            parametersRequest = parametersRequest ?? new ParametersRequest();
            await EstrategiaEnvio.ConstruirURL(parametersRequest);
            var response = await EstrategiaEnvio.SendRequest(objecto,Json);
            return response;
        }
        #endregion Métodos
    }
}
