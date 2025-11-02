using System;
using DomoticaObserver.Core.Alerts;
using DomoticaObserver.Infrastructure.Alerts;

namespace DomoticaObserver.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema Domótica (Patrón Observer) ===");

            // 1. Creamos el sujeto (sensor de humo) del dominio
            var sensor = new SensorDeHumo();

            // 2. Creamos observadores concretos (infraestructura)
            var sirena = new SirenaAlarma();
            var notificador = new NotificadorCelular();
            var registro = new RegistroEventos();

            // 3. Suscribimos observadores al sensor
            sensor.Suscribir(sirena);
            sensor.Suscribir(notificador);
            sensor.Suscribir(registro);

            Console.WriteLine("Sistema iniciado. Observadores suscritos.\n");

            // 4. Simulamos que el usuario provoca una lectura peligrosa de humo:
            Console.WriteLine(">>> Simulando detección de humo con nivel 0.8 (peligroso)...");
            sensor.DetectarHumo(0.8f);

            // 5. Simulamos otra lectura no peligrosa
            Console.WriteLine("\n>>> Simulando detección de humo con nivel 0.3 (seguro)...");
            sensor.DetectarHumo(0.3f);

            Console.WriteLine("\n=== Fin de la simulación ===");
            Console.ReadLine();
        }
    }
}
