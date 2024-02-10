using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using QueueMessageProcessor.Application.Contracts;
using QueueMessageProcessor.Domain.Contracts;
using QueueMessageProcessor.Domain.Models;
using QueueMessageProcessor.ExternalServices.Dto;

namespace QueueMessageProcessor.ExternalServices.AzureStrorageServices;

public class MessageQueueStorageRepository : IMessageQueueStorageRepository
{
    private readonly ILogger<MessageQueueStorageRepository> _logger;
    private readonly CloudQueue _cloudQueue;
    private readonly IConfiguration _configuration;
    private readonly IEventSubjectProcessorService _eventSubjetProcessorg;

    private int _waitMilliSecons = 100;
    private int _maxMessagePerRequest = 32;
    //string connectionString = ""; // La cadena de conexión a tu cuenta de almacenamiento
    //string queueName = "billforcereception-poison"; // El nombre de tu cola de Azure Storage



    public MessageQueueStorageRepository(ILogger<MessageQueueStorageRepository> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

        //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("DefaultConnectionStorage"));
        //_queueClient = new QueueClient(configuration["StringConnectionAzureStorage"], configuration["QueuePosisonName"]);
        CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
        _cloudQueue = queueClient.GetQueueReference(configuration["AppSettings:QueueName"]);
        //_cloudQueue = queueClient.GetQueueReference(queueName);
    }

    public async Task DeleteMessageById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<Notification> GetMessagesById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Notification>> GetMessagesByQuantity(int numberMessageToProcess)
    {
        var notificationsList = new List<Notification>();
        int totalMessageProcessed = 0;
        if (numberMessageToProcess < _maxMessagePerRequest)
        {
            _maxMessagePerRequest = numberMessageToProcess;
        }
        while (true)
        {
            var missingMessages = numberMessageToProcess - totalMessageProcessed;
            if (missingMessages <= 0)
            {
                break;
            }
            if (missingMessages < _maxMessagePerRequest)
            {
                _maxMessagePerRequest = missingMessages;
            }
            IEnumerable<CloudQueueMessage> messages = await _cloudQueue.GetMessagesAsync(_maxMessagePerRequest);
            int totalRecovered = messages.Count();
            totalMessageProcessed += totalRecovered;

            foreach (var message in messages)
            {
                if (message != null)
                {
                    try
                    {
                        string messageContent = message.AsString;
                        var notification = NotificationMapper.MapJsonToNotification(messageContent);
                        notification.MessageSubject.ReceiverIdentification = GetEmailUser(notification.MessageSubject.ReceiverEmail);
                        notificationsList.Add(notification);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Error processing message: " + ex.Message);
                    }
                }
            }
        }
        return notificationsList.ToList();
    }

    private string GetEmailUser(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return string.Empty;
        }
        return email.Trim().Split('@').First();
    }

}
