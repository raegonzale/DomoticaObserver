using System;
using DomoticaObserver.Core.Alerts;

namespace DomoticaObserver.Infrastructure.Alerts
{
    /// <summary>
    /// Observador que representa la sirena física de la casa.
    /// </summary>
    public class SirenaAlarma : IAlertaObservador
    {
        public void Actualizar(string mensaje)
        {
            ActivarSirena(mensaje);
        }

        private void ActivarSirena(string motivo)
        {
            Console.WriteLine($"[Activación alarma] ¡Alerta!: {motivo}");
        }
    }
}
