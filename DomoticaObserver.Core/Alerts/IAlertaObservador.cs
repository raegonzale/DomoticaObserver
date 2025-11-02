using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoticaObserver.Core.Alerts
{
    /// <summary>
    /// Observer: cualquier componente que quiera reaccionar a una alerta.
    /// </summary>
    public interface IAlertaObservador
    {
        /// <summary>
        /// Método invocado cuando se recibe una alerta.
        /// </summary>
        /// <param name="mensaje">Contenido de la alerta recibida.</param
        void Actualizar(string mensaje);
    }
}

