using RabbitMQ.Client;

namespace Dtx.Messaging.Rpc
{
	public abstract class BaseClient : object, System.IDisposable
	{
		public BaseClient
			(Settings settings, Microsoft.Extensions.Logging.ILogger logger) : base()
		{
			Logger = logger;
			Settings = settings;

			// **************************************************
			CallbackMapper =
				new System.Collections.Concurrent.ConcurrentDictionary
				<string, System.Threading.Tasks.TaskCompletionSource<string>>();
			// **************************************************

			// **************************************************
			MessageIndex = 0;

			var Factory =
				new RabbitMQ.Client.ConnectionFactory()
				{
					Port = Settings.Port,
					HostName = Settings.HostName,
					Password = Settings.Password,
					UserName = Settings.Username,
				};

			Connection =
				Factory.CreateConnection();

			Channel =
				Connection.CreateModel();

			Consumer =
				new RabbitMQ.Client.Events.EventingBasicConsumer(model: Channel);
			// **************************************************

			// **************************************************
			// using RabbitMQ.Client;
			// Declare a server-named queue
			ReplyQueueName =
				Channel.QueueDeclare(queue: string.Empty).QueueName;
			// **************************************************

			Consumer.Received += ConsumerReceived;
		}

		protected virtual void ConsumerReceived
			(object sender, RabbitMQ.Client.Events.BasicDeliverEventArgs e)
		{
			// **************************************************
			bool result =
				CallbackMapper.TryRemove(key: e.BasicProperties.CorrelationId,
				value: out System.Threading.Tasks.TaskCompletionSource<string> taskCompletionSource);

			if (result == false)
			{
				return;
			}
			// **************************************************

			// **************************************************
			var body =
				e.Body.ToArray();

			var response =
				System.Text.Encoding.UTF8.GetString(body);
			// **************************************************

			// **************************************************
			taskCompletionSource.TrySetResult(result: response);
			// **************************************************
		}

		protected Settings Settings { get; }

		protected string ReplyQueueName { get; }

		protected long MessageIndex { get; set; }

		protected RabbitMQ.Client.IModel Channel { get; set; }

		protected RabbitMQ.Client.IConnection Connection { get; set; }

		protected Microsoft.Extensions.Logging.ILogger Logger { get; }

		protected RabbitMQ.Client.Events.EventingBasicConsumer Consumer { get; }

		protected System.Collections.Concurrent.ConcurrentDictionary
			<string, System.Threading.Tasks.TaskCompletionSource<string>> CallbackMapper
		{ get; }

		public virtual System.Threading.Tasks.Task<string> CallAsync<T>
			(T value, System.Threading.CancellationToken cancellationToken = default) where T : IMessage
		{
			// **************************************************
			var correlationId =
				System.Guid.NewGuid().ToString();

			var taskCompletionSource =
				new System.Threading.Tasks.TaskCompletionSource<string>();

			CallbackMapper.TryAdd
				(key: correlationId, value: taskCompletionSource);
			// **************************************************

			// **************************************************
			// **************************************************
			// **************************************************
			var properties =
				Channel.CreateBasicProperties();

			properties.ReplyTo = ReplyQueueName;
			properties.CorrelationId = correlationId;

			value.CorrelationId = correlationId;
			value.QueueName = Settings.QueueName;
			value.ReplyQueueName = ReplyQueueName;
			// **************************************************

			// **************************************************
			var messageJson =
				Utility.ConvertObjectToJson(value: value);

			var messageBytes =
				Utility.ConvertJsonToByteArray(json: messageJson);
			// **************************************************

			// **************************************************
			// using RabbitMQ.Client;
			Channel.BasicPublish
				(exchange: string.Empty,
				routingKey: Settings.QueueName,
				basicProperties: properties,
				body: messageBytes);

			// using RabbitMQ.Client;
			Channel.BasicConsume
				(consumer: Consumer,
				queue: ReplyQueueName,
				autoAck: true);
			// **************************************************
			// **************************************************
			// **************************************************

			// **************************************************
			cancellationToken
				.Register(() => CallbackMapper.TryRemove(key: correlationId, value: out var temp));
			// **************************************************

			return taskCompletionSource.Task;
		}

		public void Dispose()
		{
			if (Channel != null)
			{
				Channel.Close();
				Channel.Dispose();
			}

			if (Connection != null)
			{
				Connection.Close();
				Connection.Dispose();
			}
		}
	}
}
