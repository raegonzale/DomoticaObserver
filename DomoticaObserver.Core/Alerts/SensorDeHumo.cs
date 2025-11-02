using System.Collections.Generic;

namespace DomoticaObserver.Core.Alerts
{
    /// <summary>
    /// Implementa el sujeto/Subject del patrón Observer.
    /// Mantiene la lista de observers y los notifica cuando detecta humo.
    /// </summary>
    public class SensorDeHumo : ISujetoAlerta
    {
        private readonly List<IAlertaObservador> _observadores = new();

        public void Suscribir(IAlertaObservador obs)
        {
            if (!_observadores.Contains(obs))
            {
                _observadores.Add(obs);
            }
        }

        public void Desuscribir(IAlertaObservador obs)
        {
            _observadores.Remove(obs);
        }

        public void Notificar(string mensaje)
        {
            foreach (var obs in _observadores)
            {
                obs.Actualizar(mensaje);
            }
        }

        /// <summary>
        /// Simula la lectura del sensor físico.
        /// Si el nivel supera el umbral, se dispara la alerta.
        /// </summary>
        public void DetectarHumo(float nivelHumo)
        {
            const float UMBRAL = 0.7f; // umbral arbitrario

            if (nivelHumo >= UMBRAL)
            {
                Notificar("¡Su casa se quema parcero!");
            }
        }
    }
}
