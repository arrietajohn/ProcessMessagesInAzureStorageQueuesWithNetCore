using QueueMessageProcessor.Domain.Models;

namespace QueueMessageProcessor.Domain.Contracts;

public interface IEventSubjectProcessorService
{
    Task<IEnumerable<Notification>> GetNotificationsByQuatity(int quantity);

    public IEnumerable<Notification> GetNotificationsByEventType(IEnumerable<Notification> notifications, string eventType);
    public IEnumerable<Notification> GetNotificationsByDocumentNumber(IEnumerable<Notification> notifications, string documentNumber);
    public IEnumerable<Notification> GetNotificationsBySenderId(IEnumerable<Notification> notifications, string senderIdentification);
    public IEnumerable<Notification> GetNotificationsBySenderName(IEnumerable<Notification> notifications, string senderame);
    public IEnumerable<Notification> GetNotificationsByEventTypeCode(IEnumerable<Notification> notifications, string eventTypeCode);
    public IEnumerable<Notification> GetNotificationsByEventTypeName(IEnumerable<Notification> notifications, string eventTypeName);
    public IEnumerable<Notification> GetNotificationsByDomainLine(IEnumerable<Notification> notifications, string domainLine);
    public IEnumerable<Notification> GetNotificationsByReceiverId(IEnumerable<Notification> notifications, string receiverIdentification);

}
