using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IHostedServiceDemo.Services
{
    //IDisposable es para limpiar los recursos del timer.
    public class WriteToFileHostedService2 : IHostedService, IDisposable
    {
        private readonly IHostingEnvironment environment;
        private readonly string fileName = "file2.txt";
        private Timer timer;

        public WriteToFileHostedService2(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public void Dispose()
        {
            //El timer se ejecuta siempre y cuando no es nulo
            timer?.Dispose();
        }

        /* Descripción: La función `DoWork` llama a su vez a la función `WriteToFile` 
         * para escribir en el archivo correspondiente al servicio una cadena de texto 
         * con la hora en formato dd/MM/yyyy hh:mm:ss. 
         */
        public void DoWork(object state)
        {
            WriteToFile("WriteToFileHostedService: Doing some work at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteTofileHostedServices: Process Started");
            //Aquí con el timer, llamamos a la funcion DoWork e indicamos el rango de tiempo
            //en el que debe ejecutarse, para este caso es cada 7 segundos.
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(7));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteTofileHostedServices: Process Stopped");
            //Desactivando el timer
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void WriteToFile(string message)
        {
            //Define la ruta donde se encontrará el archivo
            var path = $@"{environment.ContentRootPath}\wwwroot\{fileName}";
            //el StreamWriter es para escribir en el archivo
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
