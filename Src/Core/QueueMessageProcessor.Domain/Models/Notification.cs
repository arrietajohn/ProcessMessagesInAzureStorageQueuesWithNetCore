namespace QueueMessageProcessor.Domain.Models;



public class Notification
{
    public string Type { get; set; }
    public string MessageId { get; set; }
    public string TopicArn { get; set; }
    public string Subject { get; set; }
    public Subject MessageSubject { get; set; }
    public DateTime Timestamp { get; set; }

    // Constructor vacío
    public Notification()
    {
    }

    // Constructor con inicialización de propiedades
    public Notification(string type, string messageId, string topicArn, string subject, DateTime timestamp)
    {
        Type = type;
        MessageId = messageId;
        TopicArn = topicArn;
        Subject = subject;
        Timestamp = timestamp;
    }


    public override string ToString()
    {
        var data = $"NOTIFICATION:\n";
        data += $"Type: {Type}\n";
        data += $"MessageId: {MessageId}\n";
        data += $"TopicArn: {TopicArn} \n";
        data += $"ESubject: {Subject}\n";
        data += $"DTimestamp: {Timestamp}\n";
        data += "--------------------------\n";
        data += $"Event {MessageSubject}\n";
        return data;
    }
}

