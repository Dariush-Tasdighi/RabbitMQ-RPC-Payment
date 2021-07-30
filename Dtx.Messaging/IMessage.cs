namespace Dtx.Messaging
{
	public interface IMessage
	{
		string ClientIP { get; set; }

		string ServerIP { get; set; }

		string QueueName { get; set; }
	}
}
