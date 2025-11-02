using System;
using DomoticaObserver.Core.Alerts;

namespace DomoticaObserver.Infrastructure.Alerts
{
    /// <summary>
    /// Observador que simula enviar una notificación push al dueño.
    /// </summary>
    public class NotificadorCelular : IAlertaObservador
    {
        public void Actualizar(string mensaje)
        {
            EnviarPushAlDueno(mensaje);
        }

        private void EnviarPushAlDueno(string msg)
        {
            Console.WriteLine($"[PUSH] Su casa se quema parcero: {msg}");
        }
    }
}
