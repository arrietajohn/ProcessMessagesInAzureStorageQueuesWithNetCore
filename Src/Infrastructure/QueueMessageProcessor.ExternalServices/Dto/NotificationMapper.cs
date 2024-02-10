using Newtonsoft.Json;
using QueueMessageProcessor.Domain.Models;

namespace QueueMessageProcessor.ExternalServices.Dto;

public static class NotificationMapper
{
    public static Notification MapJsonToNotification(string json)
    {
        // Reemplazar dobles barras por una sola barra
        //json = Regex.Replace(json, @"\\\\", @"\");
        json = json.Replace("\\\\", "\\");
        json = json.Replace("\\\"", "\"");
        json = json.Replace("\"{", "{");
        json = json.Replace("}\"", "}");
        // Reemplazar comillas dobles escapadas por comillas dobles
        //json = Regex.Replace(json, @"\\\""", @"""");
        // Deserializar el JSON a un objeto anónimo o a una clase auxiliar
        var responseRootDto = JsonConvert.DeserializeObject<RootDto>(json);

        // Crear una instancia de la clase de dominio Notification y asignar los datos
        var notification = new Notification
        {
            Type = responseRootDto.Type,
            MessageId = responseRootDto.MessageId,
            TopicArn = responseRootDto.TopicArn,
            Subject = responseRootDto.Subject,
            Timestamp = responseRootDto.Timestamp,
            MessageSubject = new Subject
            {
                OriginalSubject = responseRootDto.Message.Mail.CommonHeaders.Subject,
                ReceiverEmail = responseRootDto.Message.Mail.Destination[0]
            }
        };

        return notification;
    }
}
