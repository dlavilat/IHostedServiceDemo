# IHostedServiceDemo

Corresponde a una práctica del curso de "Construyendo Web Api RestFul" con ASP.NET Core 2.2 en Udemy por Felipe Gavilán.<br>
IHostedService es una interfaz que nos permite crear un servicio, el cuál va a ejecutar una función cuando levantemos el web host y cuando lo detengamos.

## Introducción y Conceptos Previos

Este proyecto consiste en la construcción y manipulación de servicios en APS.NET Core, para ello se deben tener claro unos conceptos previos que explicarán a continuación.

__Tiempo de vida__

- __Transient:__ Cada vez que un servicio es solicitado, se crea una instancia de la clase. Es decir, los objetos transitorios son siempre diferentes. Se proporciona una nueva instancia para cada controlador y cada servicio.
- __Scoped:__ Los objetos son los mismos dentro de una misma solicitud (se les entrega la misma instancia), pero son diferentes en diferentes solicitudes. 
- __Singleton:__ Siempre se nos va a dar la misma instancia. Sólo varía cuando el servidor se apaga y se enciende nuevamente.

## Contenido

Este proyecto consiste en dos servicios WriteToFileHostedService y WriteToFileHostedService2 los cuales implementan las interfaces IHostedService, IDisposable.<br>
El __IHostedService__ es el que indica que la clase es un servicio que se ejecuta al lanzar la aplicación y al detenerla. Esto es posible por los métodos `StartAsync` y  `StopAsync`. <br>
El __IDisposable__ es ncesario para limpiar los recursos del `timer`.<br>
La Clase `Timer` la utilizamos para ejecutar una función (DoWork) cada x tiempo, el tiempo puede darse en horas, minutos, segundos, etc. Para nuestra aplicación en el servicio WriteToFileHostedService2 el objeto Timer se instancia con 7 segundos.

La función `DoWork` llama a su vez a la función `WriteToFile` para escribir en el archivo correspondiente al servicio una cadena de texto con la hora en formato hh:mm:ss. Al compilar y ejecutar se visualiza en el archivo que se va escribiendo cada x segundo el valor de la hora. Esto ocurre hasta que se detenga la aplicación.

Por otro lado por inyección de dependencias se crea el objeto `environment` de la interfaz IHostingEnvironment. Éste es necesario para obtener información de la ruta de la aplicación a través del método `ContentRootPath`. Éste se utiliza para almacenar los documentos .txt que se crearán. <br>
Con este pequeño proyecto vemos como funcionan dos servicios IHostedService corriendo simultáneamente.

## Configuración en .Net Framework

Creamos un nuevo proyecto -> web -> ASP.Net Core Web Application -> Web application (Model-View-Controller). Debemos revisar que los desplegables se seleccionen con .Net Core y ASP.Net Core 2.2. 
