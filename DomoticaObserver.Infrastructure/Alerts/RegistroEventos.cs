using System;
using System.Collections.Generic;
using DomoticaObserver.Core.Alerts;

namespace DomoticaObserver.Infrastructure.Alerts
{
    /// <summary>
    /// Observador que guarda histórico de eventos.
    /// Esto simula un log / base de datos.
    /// </summary>
    public class RegistroEventos : IAlertaObservador
    {
        private readonly List<string> _historial = new();

        public void Actualizar(string mensaje)
        {
            GuardarEnHistorial(mensaje);
        }

        private void GuardarEnHistorial(string mensaje)
        {
            var registro = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} -> {mensaje}";
            _historial.Add(registro);

            Console.WriteLine($"[LOG] Evento registrado: {registro}");
        }

        // Muestra el historial
        public IEnumerable<string> ObtenerHistorial()
        {
            return _historial.AsReadOnly();
        }
    }
}