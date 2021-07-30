using Microsoft.Extensions.Logging;

namespace Dtx.Messaging.Rpc
{
	public abstract class BaseServer : Microsoft.Extensions.Hosting.BackgroundService
	{
		public BaseServer
			(Settings settings, Microsoft.Extensions.Logging.ILogger logger) : base()
		{
			Logger = logger;
			Settings = settings;
		}

		protected Settings Settings { get; }

		protected long MessageIndex { get; set; }

		protected RabbitMQ.Client.IModel Channel { get; set; }

		protected RabbitMQ.Client.IConnection Connection { get; set; }

		protected Microsoft.Extensions.Logging.ILogger Logger { get; }

		protected RabbitMQ.Client.ConnectionFactory Factory { get; set; }

		public override
			System.Threading.Tasks.Task
			StartAsync(System.Threading.CancellationToken cancellationToken)
		{
			MessageIndex = 0;

			Factory =
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

			Channel.QueueDeclare
				(queue: Settings.QueueName,
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

			// using Microsoft.Extensions.Logging;
			Logger.LogInformation
				($"Queue [{Settings.QueueName}] is waiting for messages. [{Utility.NowString}]");

			return base.StartAsync(cancellationToken);

			// دستور ذیل کار نمی‌کند
			//return System.Threading.Tasks.Task.CompletedTask;
		}

		public override
			System.Threading.Tasks.Task
			StopAsync(System.Threading.CancellationToken cancellationToken)
		{
			if (Channel != null)
			{
				Channel.Dispose();
			}

			if (Connection != null)
			{
				Connection.Close();
				Connection.Dispose();
			}

			// using Microsoft.Extensions.Logging;
			Logger.LogInformation
				($"Queue [{Settings.QueueName}] is closed. [{Utility.NowString}]");

			return base.StopAsync(cancellationToken);

			// دستور ذیل کار نمی‌کند
			//return System.Threading.Tasks.Task.CompletedTask;
		}
	}
}
