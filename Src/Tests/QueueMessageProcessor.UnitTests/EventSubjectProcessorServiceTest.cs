using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using QueueMessageProcessor.Application.Services;
using QueueMessageProcessor.ExternalServices.AzureStrorageServices;

namespace QueueMessageProcessor.UnitTests;
[TestClass]
public class EventSubjectProcessorServiceTest
{
    [TestMethod]
    public async Task Test1_GetNotificationsWithEventSubjectAsync_ReturnListNotifications_WhenAnyExixts()
    {
        // Arrange
        var configBuilder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Ahora puedes acceder a las configuraciones como lo harías normalmente en tu aplicación

        var logger = new Mock<ILogger<MessageQueueStorageRepository>>().Object;
        var messageQueueStorageRepository = new MessageQueueStorageRepository(logger, configBuilder.Build());
        var logger2 = new Mock<ILogger<EventSubjectProcessorService>>().Object;
        var eventSubjectProcessorService = new EventSubjectProcessorService(messageQueueStorageRepository, logger2);
        //Act
        var list = await eventSubjectProcessorService.GetNotificationsByQuatity(20);
        // Assert
        Assert.IsNotNull(list);
        Assert.IsTrue(list.Any());
    }



}
