using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoticaObserver.Core.Alerts
{
    /// <summary>
    /// Subject: entidad que permite suscripción/notificación.
    /// </summary>
    public interface ISujetoAlerta
    {
        void Suscribir(IAlertaObservador obs);
        void Desuscribir(IAlertaObservador obs);
        void Notificar(string mensaje);
    }
}
