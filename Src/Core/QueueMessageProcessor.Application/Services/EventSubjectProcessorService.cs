
using Microsoft.Extensions.Logging;
using QueueMessageProcessor.Application.Contracts;
using QueueMessageProcessor.Application.Models;
using QueueMessageProcessor.Domain.Contracts;
using QueueMessageProcessor.Domain.Models;

namespace QueueMessageProcessor.Application.Services;

public class EventSubjectProcessorService : IEventSubjectProcessorService
{
    private readonly IMessageQueueStorageRepository _messageQueueStorageRepository;
    private readonly ILogger<EventSubjectProcessorService> _logger;

    public EventSubjectProcessorService(IMessageQueueStorageRepository iMessageQueueStorageRepository, ILogger<EventSubjectProcessorService> logger)
    {
        _messageQueueStorageRepository = iMessageQueueStorageRepository;
        _logger = logger;
    }

    private void SubjectProcessor(Subject eventSubject, string wronMessage)
    {
        //await Task.Yield();
        try
        {
            var subjectParts = eventSubject.OriginalSubject.Split(';');
            var eventTypeCodeInSubject = string.Empty;

            if (subjectParts.Length <= 5)
            {
                eventSubject.Type = $"{Constants.WRONG_SUBJECT_MESSAGE}: It is imcomplete";
            }
            eventSubject.DocumentNumber = (subjectParts.Length >= 2) ? subjectParts[1].Trim() : "Invalid";
            eventSubject.SenderIdentification = (subjectParts.Length >= 3) ? subjectParts[2].Trim() : "Invalid";
            eventSubject.SenderName = (subjectParts.Length >= 4) ? subjectParts[3].Trim() : "Invalid";
            eventSubject.EventDocumentNumber = (subjectParts.Length >= 5) ? subjectParts[4].Trim() : "Invalid";
            eventSubject.EventTypeCode = (subjectParts.Length >= 6) ? subjectParts[5].Trim() : "Invalid";
            eventSubject.EventTypeName = (subjectParts.Length >= 6) ? Constants.billforceEventTypeDictionary.GetValueOrDefault(subjectParts[5].Trim()) : "Invalid";
            eventSubject.DomainLine = (subjectParts.Length >= 7) ? subjectParts[6].Trim() : "Invalid";

            string[] tvEventTypeCode = Constants.billforceEventTypeDictionary.Keys.Take(4).ToArray();
            string[] billforceEventTypeCode = Constants.billforceEventTypeDictionary.Keys.Skip(4).ToArray();

            if (!billforceEventTypeCode.Contains(eventSubject.EventTypeCode) && !tvEventTypeCode.Contains(eventSubject.EventTypeCode))
            {
                eventSubject.Type = $"{Constants.WRONG_SUBJECT_MESSAGE}: Isn't a not valid event";
            }
            else
            if (tvEventTypeCode.Contains(eventSubject.EventTypeCode))
            {
                eventSubject.Type = $"{Constants.TITLE_VALUE_EVENT_MESSAGE}";
            }
            else
            {
                eventSubject.Type = $"{Constants.BILLFORCE_EVENT_MESSAGE} ";
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error processing the notification subject");
        }

    }

    public async Task<IEnumerable<Notification>> GetNotificationsByQuatity(int quantity)
    {
        var notificacions = await _messageQueueStorageRepository.GetMessagesByQuantity(quantity);

        foreach (var notification in notificacions)
        {
            SubjectProcessor(notification.MessageSubject, Constants.WRONG_MESSAGE);
        }
        return notificacions;
    }

    public IEnumerable<Notification> GetNotificationsByEventType(IEnumerable<Notification> notifications, string eventType)
    {
        if (notifications == null)
        {
            throw new ArgumentNullException("The notification can´t be Null");
        }
        if (!notifications.Any())
        {
            throw new Exception("The notification can´t be Empty");
        }
        return notifications.Where(n => n.MessageSubject.Type.Contains(eventType)).ToList();
    }


    public IEnumerable<Notification> GetNotificationsByDocumentNumber(IEnumerable<Notification> notifications, string documentNumber)
    {
        if (notifications == null)
        {
            throw new ArgumentNullException("The notification can´t be Null");
        }
        if (!notifications.Any())
        {
            throw new Exception("The notification can´t be Empty");
        }
        return notifications.Where(n => n.MessageSubject.DocumentNumber.Equals(documentNumber)).ToList();
    }

    public IEnumerable<Notification> GetNotificationsBySenderId(IEnumerable<Notification> notifications, string senderIdentification)
    {
        if (notifications == null)
        {
            throw new ArgumentNullException("The notification can´t be Null");
        }
        if (!notifications.Any())
        {
            throw new Exception("The notification can´t be Empty");
        }
        return notifications.Where(n => n.MessageSubject.SenderIdentification.Equals(senderIdentification)).ToList();
    }


    public IEnumerable<Notification> GetNotificationsBySenderName(IEnumerable<Notification> notifications, string senderame)
    {
        if (notifications == null)
        {
            throw new ArgumentNullException("The notification can´t be Null");
        }
        if (!notifications.Any())
        {
            throw new Exception("The notification can´t be Empty");
        }
        return notifications.Where(n => n.MessageSubject.SenderName.Contains(senderame)).ToList();
    }

    public IEnumerable<Notification> GetNotificationsByEventTypeCode(IEnumerable<Notification> notifications, string eventTypeCode)
    {
        if (notifications == null)
        {
            throw new ArgumentNullException("The notification can´t be Null");
        }
        if (!notifications.Any())
        {
            throw new Exception("The notification can´t be Empty");
        }
        return notifications.Where(n => n.MessageSubject.EventTypeCode.Equals(eventTypeCode)).ToList();
    }

    public IEnumerable<Notification> GetNotificationsByEventTypeName(IEnumerable<Notification> notifications, string eventTypeName)
    {
        if (notifications == null)
        {
            throw new ArgumentNullException("The notification can´t be Null");
        }
        if (!notifications.Any())
        {
            throw new Exception("The notification can´t be Empty");
        }
        return notifications.Where(n => (n.MessageSubject.EventTypeName ?? string.Empty).Length > 0 && n.MessageSubject.EventTypeName.Contains(eventTypeName)).ToList();
    }

    public IEnumerable<Notification> GetNotificationsByDomainLine(IEnumerable<Notification> notifications, string domainLine)
    {
        if (notifications == null)
        {
            throw new ArgumentNullException("The notification can´t be Null");
        }
        if (!notifications.Any())
        {
            throw new Exception("The notification can´t be Empty");
        }
        return notifications.Where(n => (n.MessageSubject.DomainLine ?? string.Empty).Length > 0 && n.MessageSubject.DomainLine.Contains(domainLine)).ToList();
    }

    public IEnumerable<Notification> GetNotificationsByReceiverId(IEnumerable<Notification> notifications, string receiverIdentification)
    {
        if (notifications == null)
        {
            throw new ArgumentNullException("The notification can´t be Null");
        }
        if (!notifications.Any())
        {
            throw new Exception("The notification can´t be Empty");
        }
        return notifications.Where(n => n.MessageSubject.ReceiverIdentification.Equals(receiverIdentification)).ToList();
    }

}



