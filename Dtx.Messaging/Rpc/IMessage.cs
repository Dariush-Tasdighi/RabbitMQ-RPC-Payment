namespace Dtx.Messaging.Rpc
{
	public interface IMessage : Messaging.IMessage
	{
		string CorrelationId { get; set; }

		string ReplyQueueName { get; set; }
	}
}
