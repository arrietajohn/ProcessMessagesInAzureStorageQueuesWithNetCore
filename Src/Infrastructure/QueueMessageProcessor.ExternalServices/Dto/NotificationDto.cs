namespace QueueMessageProcessor.ExternalServices.Dto;



    public class HeaderDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class CommonHeadersDto
    {
        public string ReturnPath { get; set; }
        public List<string> From { get; set; }
        public string Date { get; set; }
        public List<string> To { get; set; }
        public string MessageId { get; set; }
        public string Subject { get; set; }
    }

    public class MailDto
    {
        public string Timestamp { get; set; }
        public string Source { get; set; }
        public string MessageId { get; set; }
        public List<string> Destination { get; set; }
        public bool HeadersTruncated { get; set; }
        public List<HeaderDto> Headers { get; set; }
        public CommonHeadersDto CommonHeaders { get; set; }
    }

    public class MessageDto
    {
        public string NotificationType { get; set; }
        public MailDto Mail { get; set; }

    }

    public class RootDto
    {
        public string Type { get; set; }
        public string MessageId { get; set; }
        public string TopicArn { get; set; }
        public string Subject { get; set; }
        public MessageDto Message { get; set; }
        public DateTime Timestamp { get; set; }
    }


