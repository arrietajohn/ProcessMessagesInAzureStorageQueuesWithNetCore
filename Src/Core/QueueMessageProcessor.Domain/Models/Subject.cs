namespace QueueMessageProcessor.Domain.Models;

public class Subject
{

    public string Type { get; set; } = "Evento";
    public string DocumentNumber { get; set; }
    public string SenderIdentification { get; set; }
    public string SenderName { get; set; }
    public string EventDocumentNumber { get; set; }
    public string EventTypeCode { get; set; }
    public string EventTypeName { get; set; }
    public string? DomainLine { get; set; }
    public string ReceiverEmail { get; set; }
    public string ReceiverIdentification { get; set; }
    public string OriginalSubject { get; set; }



    public Subject()
    {
    }

    public Subject(string type, string documentNumber, string senderIdentification
                        , string senderName, string eventDocumentNumber, string eventTypeCode
                        , string domainLine, string recetivedEmail)
    {
        Type = type;
        DocumentNumber = documentNumber;
        SenderIdentification = senderIdentification;
        SenderName = senderName;
        EventDocumentNumber = eventDocumentNumber;
        EventTypeCode = eventTypeCode;
        DomainLine = domainLine;
        ReceiverEmail = recetivedEmail;
    }




    public override string ToString()
    {
        var data = $"Type: {Type}\n";
        data += $"Document Number: {DocumentNumber}\n";
        data += $"Sender Identification: {SenderIdentification}\n";
        data += $"Sender Name: {SenderName} \n";
        data += $"Evento Document Number: {EventDocumentNumber}\n";
        data += $"Event Type: {EventTypeCode}\n";
        data += $"Event Name: {EventTypeName}\n";
        data += $"Domain Line: {DomainLine ?? ""}\n";
        data += $"Receiver Identification: {ReceiverIdentification}\n";
        data += $"Receiver Email: {ReceiverEmail}\n";
        data += $"Orignal Subject: {OriginalSubject}\n";
        return data;
    }


}