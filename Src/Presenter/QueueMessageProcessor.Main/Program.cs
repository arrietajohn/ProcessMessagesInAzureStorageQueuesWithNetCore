// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QueueMessageProcessor.Application.Contracts;
using QueueMessageProcessor.Application.Models;
using QueueMessageProcessor.Application.Services;
using QueueMessageProcessor.Domain.Contracts;
using QueueMessageProcessor.ExternalServices.AzureStrorageServices;

var builder = new HostBuilder()
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.AddJsonFile("appsettings.json", optional: true);
               config.AddEnvironmentVariables(prefix: "PREFIX_");
           })
           .ConfigureServices((hostContext, services) =>
           {
               // Registrar los servicios de Proyecto1 y Proyecto2
               services.AddScoped<IEventSubjectProcessorService, EventSubjectProcessorService>();
               services.AddScoped<IMessageQueueStorageRepository, MessageQueueStorageRepository>();

               // Registrar ILogger
               services.AddLogging(builder =>
               {
                   builder.AddConsole();
                   builder.SetMinimumLevel(LogLevel.Debug); // Ajusta el nivel de log según sea necesario
               });
           });

var host = builder.Build();
var serviceProvider = host.Services;


var eventSubjectProcessor = serviceProvider.GetRequiredService<IEventSubjectProcessorService>();
var notificationsList = await eventSubjectProcessor.GetNotificationsByQuatity(100);


Console.WriteLine("");
Console.WriteLine("-------- ESTADISTICAS -------");
Console.WriteLine("-------- ************* -------");
Console.WriteLine("TOLTAL EVENTOS SOLICITADOS: " + notificationsList.Count());
Console.WriteLine($"EVENTOS INCORRECTOS: {notificationsList.Where(ev => ev.MessageSubject.Type.StartsWith(Constants.WRONG_MESSAGE) || ev.MessageSubject.Type.StartsWith(Constants.WRONG_SUBJECT_MESSAGE)).ToList().Count}");
Console.WriteLine($"EVENTOS TITULO VALOR: {notificationsList.Where(ev => ev.MessageSubject.Type.StartsWith(Constants.TITLE_VALUE_EVENT_MESSAGE)).ToList().Count}");
Console.WriteLine($"EVENTOS CIRC/NEG: {notificationsList.Where(ev => ev.MessageSubject.Type.StartsWith(Constants.BILLFORCE_EVENT_MESSAGE)).ToList().Count}");
Console.WriteLine("-------- ************* -------");
Console.WriteLine("");



var notificationsListCustom = eventSubjectProcessor.GetNotificationsByEventType(notificationsList, "Title");


Console.WriteLine("---------------------------------");
Console.WriteLine("---- DETALLES DE CADA EVENTO TITULO VALOR----");
Console.WriteLine("---------------------------------");
Console.WriteLine("");



foreach (var message in notificationsListCustom)
{
    Console.WriteLine(message);
}

notificationsListCustom = eventSubjectProcessor.GetNotificationsByEventTypeCode(notificationsList, "030");


Console.WriteLine("---------------------------------");
Console.WriteLine("---- DETALLES DE CADA EVENTO 030 ----");
Console.WriteLine("---------------------------------");
Console.WriteLine("");



foreach (var message in notificationsListCustom)
{
    Console.WriteLine(message);
}


notificationsListCustom = eventSubjectProcessor.GetNotificationsByEventTypeCode(notificationsList, "031");


Console.WriteLine("---------------------------------");
Console.WriteLine("---- DETALLES DE CADA EVENTO 031 ----");
Console.WriteLine("---------------------------------");
Console.WriteLine("");



foreach (var message in notificationsListCustom)
{
    Console.WriteLine(message);
}



notificationsListCustom = eventSubjectProcessor.GetNotificationsByEventTypeCode(notificationsList, "032");


Console.WriteLine("---------------------------------");
Console.WriteLine("---- DETALLES DE CADA EVENTO 032 ----");
Console.WriteLine("---------------------------------");
Console.WriteLine("");



foreach (var message in notificationsListCustom)
{
    Console.WriteLine(message);
}


notificationsListCustom = eventSubjectProcessor.GetNotificationsByEventTypeCode(notificationsList, "033");


Console.WriteLine("---------------------------------");
Console.WriteLine("---- DETALLES DE CADA EVENTO 033 ----");
Console.WriteLine("---------------------------------");
Console.WriteLine("");



foreach (var message in notificationsListCustom)
{
    Console.WriteLine(message);
}

notificationsListCustom = eventSubjectProcessor.GetNotificationsByEventTypeCode(notificationsList, "034");


Console.WriteLine("---------------------------------");
Console.WriteLine("---- DETALLES DE CADA EVENTO 034 ----");
Console.WriteLine("---------------------------------");
Console.WriteLine("");



foreach (var message in notificationsListCustom)
{
    Console.WriteLine(message);
}


notificationsListCustom = eventSubjectProcessor.GetNotificationsByEventType(notificationsList, "Billforce");


Console.WriteLine("---------------------------------");
Console.WriteLine("---- DETALLES DE CADA EVENTO BILLFORCE ----");
Console.WriteLine("---------------------------------");
Console.WriteLine("");



foreach (var message in notificationsListCustom)
{
    Console.WriteLine(message);
}

