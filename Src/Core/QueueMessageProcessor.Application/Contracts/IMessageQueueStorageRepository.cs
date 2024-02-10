using QueueMessageProcessor.Domain.Models;

namespace QueueMessageProcessor.Application.Contracts;

public interface IMessageQueueStorageRepository
{

    Task<IEnumerable<Notification>> GetMessagesByQuantity(int cuantity);
    Task<Notification> GetMessagesById(string id);
    Task DeleteMessageById(string id);

}

