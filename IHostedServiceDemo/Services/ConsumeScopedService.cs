using IHostedServiceDemo.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IHostedServiceDemo.Services
{
    public class ConsumeScopedService: IHostedService, IDisposable
    {
        private Timer _timer;
        public IServiceProvider Services { get; }

        public ConsumeScopedService(IServiceProvider services)
        {
            Services = services;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /*Descripción: Services es utilizado en el método para crear el contexto 
         * con scope para así utilizar el servicio HostedServiceLog con 
         * Entity Framework para guardar la información en la BD.
         */
        public void DoWork(object state)
        {
            //Creamos un pequeño contexto en donde utilizamos un servicio scope
            using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var message = "ConsumeScopedService. Received message at " + DateTime.Now.ToString();

                //Instanciamos nuestro objeto HostedServiceLog que es el que tenemos modelado para la BD
                //Posteriormente lo agregamos y guardamos los cambios (utilizando Entity Framework)
                var log = new HostedServiceLog() { Message = message };
                context.HostedServiceLog.Add(log);
                context.SaveChanges();
            }
        }
    }
}
