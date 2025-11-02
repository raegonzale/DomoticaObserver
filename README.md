# Domótica Observer (.NET 8)

**Patrón de diseño aplicado:** `Observer`  
**Arquitectura:** Monolito modular con capas limpias
**Lenguaje:** C# / .NET 8  
**Autor:** Raúl González  
**Fecha:** Noviembre 2025

---

## Descripción general

Este proyecto implementa un **Sistema de Alerta Domótica** que utiliza el **patrón Observer** para simular la activación de una alarma cuando se detecta humo en una vivienda.  
El diseño busca demostrar los principios de **bajo acoplamiento**, **alta cohesión** y **cumplimiento SOLID**, aplicando una arquitectura **en capas limpias**, fácil de mantener y extender.

La simulación representa un escenario típico de IoT doméstico:

1. Un **Sensor de Humo** detecta niveles peligrosos.  
2. Se **notifica automáticamente** a múltiples observadores:
   - una **sirena** (alarma sonora),
   - un **notificador celular** (mensaje push),
   - y un **registro de eventos** (log histórico).

---

## Arquitectura del proyecto

El sistema está organizado en **tres capas** según el principio de dependencias apuntando hacia adentro:

### UML

```plantuml
@startuml
title Sistema de Alerta Domótica (Patrón Observer, Monolito Modular)

interface IAlertaObservador <<Core>> {
    +Actualizar(mensaje : string) : void
}

interface ISujetoAlerta <<Core>> {
    +Suscribir(obs : IAlertaObservador) : void
    +Desuscribir(obs : IAlertaObservador) : void
    +Notificar(mensaje : string) : void
}

class SensorDeHumo <<Core>> implements ISujetoAlerta {
    -observadores : List<IAlertaObservador>
    +Suscribir(obs : IAlertaObservador) : void
    +Desuscribir(obs : IAlertaObservador) : void
    +Notificar(mensaje : string) : void
    +DetectarHumo(nivelHumo : float) : void
}

class SirenaAlarma <<Infrastructure>> implements IAlertaObservador {
    +Actualizar(mensaje : string) : void
    -ActivarSirena() : void
}

class NotificadorCelular <<Infrastructure>> implements IAlertaObservador {
    +Actualizar(mensaje : string) : void
    -EnviarPushAlDueno(mensaje : string) : void
}

class RegistroEventos <<Infrastructure>> implements IAlertaObservador {
    +Actualizar(mensaje : string) : void
    -GuardarEnHistorial(mensaje : string) : void
}

class DomoticaApp <<App>> {
    +Main() : void
}

ISujetoAlerta <|.. SensorDeHumo
IAlertaObservador <|.. SirenaAlarma
IAlertaObservador <|.. NotificadorCelular
IAlertaObservador <|.. RegistroEventos

SensorDeHumo "1" o-- "*" IAlertaObservador : suscriptores
DomoticaApp --> SensorDeHumo : configura sensores
DomoticaApp --> IAlertaObservador : registra observadores

note right of SensorDeHumo::DetectarHumo
  Cuando detecta humo,
  llama a Notificar()
end note

note bottom of IAlertaObservador
  Todos los observadores
  implementan esta interfaz común.
  Se pueden agregar nuevos sin
  modificar el SensorDeHumo.
end note

note right of SensorDeHumo
  Mantiene solo IAlertaObservador,
  no conoce implementaciones concretas.
  => Bajo acoplamiento / DIP
end note
@enduml

### Secuencia

```plantuml
@startuml
title Secuencia: Activación de Alerta por Sensor de Humo

actor Usuario
participant DomoticaApp <<App>>
participant SensorDeHumo <<Core>>
participant SirenaAlarma <<Infrastructure>>
participant NotificadorCelular <<Infrastructure>>
participant RegistroEventos <<Infrastructure>>

Usuario -> DomoticaApp : Inicia sistema

DomoticaApp -> SensorDeHumo : Crear instancia / inicializar
DomoticaApp -> SirenaAlarma : Crear instancia
DomoticaApp -> NotificadorCelular : Crear instancia
DomoticaApp -> RegistroEventos : Crear instancia

DomoticaApp -> SensorDeHumo : Suscribir(SirenaAlarma)
DomoticaApp -> SensorDeHumo : Suscribir(NotificadorCelular)
DomoticaApp -> SensorDeHumo : Suscribir(RegistroEventos)

note right of SensorDeHumo
  SensorDeHumo mantiene una lista de IAlertaObservador,
  no conoce las clases concretas (SirenaAlarma, etc.).
  => Bajo acoplamiento / Inversión de Dependencias (DIP)
end note

Usuario -> SensorDeHumo : DetectarHumo(nivelHumo)

activate SensorDeHumo
SensorDeHumo -> SensorDeHumo : Notificar("¡Alerta de humo!")

SensorDeHumo -> SirenaAlarma : Actualizar("¡Alerta de humo!")
activate SirenaAlarma
SirenaAlarma -> SirenaAlarma : ActivarSirena()
deactivate SirenaAlarma

SensorDeHumo -> NotificadorCelular : Actualizar("¡Alerta de humo!")
activate NotificadorCelular
NotificadorCelular -> NotificadorCelular : EnviarPushAlDueno()
deactivate NotificadorCelular

SensorDeHumo -> RegistroEventos : Actualizar("¡Alerta de humo!")
activate RegistroEventos
RegistroEventos -> RegistroEventos : GuardarEnHistorial()
deactivate RegistroEventos

deactivate SensorDeHumo

@enduml

## Ejecución

cd DomoticaObserver.App
dotnet run

## Respuesta

=== Sistema Domótica (Patrón Observer) ===
Sistema iniciado. Observadores suscritos.

>>> Simulando detección de humo con nivel 0.8 (peligroso)...
[Activación alarma] ¡Alerta!: ¡Su casa se quema parcero!
[PUSH Mensaje al celular] ¡Alerta!: ¡Su casa se quema parcero!
[LOG] Evento registrado: 2025-11-02 17:50:21 -> ¡Su casa se quema parcero!

>>> Simulando detección de humo con nivel 0.3 (seguro)...

=== Fin de la simulación ===

## Concluimos que:
El sistema Domótica Observer demuestra cómo aplicar el patrón Observer de forma estructurada, integrando los principios SOLID dentro de una arquitectura limpia y modular.
Su principal fortaleza es la extensibilidad sin acoplamiento, que permite incorporar nuevas fuentes de notificación o sensores sin alterar el núcleo del dominio.

