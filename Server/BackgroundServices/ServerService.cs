using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace Application
{
	public class ServerService : Dtx.Messaging.Rpc.BaseServer
	{
		public ServerService
			(Microsoft.Extensions.Options.IOptions<Settings> options,
			Microsoft.Extensions.Logging.ILogger<ServerService> logger) :
			base(settings: options.Value.Messaging, logger: logger)
		{
		}

		protected override
			System.Threading.Tasks.Task
			ExecuteAsync(System.Threading.CancellationToken cancellationToken)
		{
			while (cancellationToken.IsCancellationRequested == false)
			{
				var consumer =
					new RabbitMQ.Client.Events.EventingBasicConsumer(model: Channel);

				// using RabbitMQ.Client;
				Channel.BasicConsume
					(queue: Settings.QueueName,
					autoAck: false,
					consumer: consumer);

				consumer.Received += (model, eventArgs) =>
				{
					MessageIndex++;

					// **************************************************
					var properties =
						eventArgs.BasicProperties;

					var replyProperties =
						Channel.CreateBasicProperties();

					replyProperties.CorrelationId = properties.CorrelationId;
					// **************************************************

					string responseJson = null;
					Dtos.Transaction transaction = null;

					try
					{
						// **************************************************
						string json =
							Dtx.Messaging.Utility.ConvertByteArrayToJson(eventArgs.Body);

						transaction =
							Dtx.Messaging.Utility.ConvertJsonToObject<Dtos.Transaction>(json: json);

						// پرداخت قبض
						DoSomething(transaction);
						// **************************************************

						// **************************************************
						string message =
							$"Message with the index of { MessageIndex } resolved!";

						// using Microsoft.Extensions.Logging;
						Logger.LogInformation(message);
						// **************************************************
					}
					catch (System.Exception ex)
					{
						if (transaction == null)
						{
							transaction =
								new Dtos.Transaction();
						}

						transaction.Result.Code = "123";
						transaction.Result.Number = null;
						transaction.Result.Message = ex.Message;

						Logger.LogError(exception: ex, message: ex.Message);
					}
					finally
					{
						responseJson =
							System.Text.Json.JsonSerializer.Serialize(transaction);

						var responseBytes =
							System.Text.Encoding.UTF8.GetBytes(responseJson);

						// using RabbitMQ.Client;
						Channel.BasicPublish
							(exchange: string.Empty,
							routingKey: properties.ReplyTo,
							basicProperties: replyProperties,
							body: responseBytes);

						Channel.BasicAck
							(deliveryTag: eventArgs.DeliveryTag, multiple: false);
					}
				};

				// using Microsoft.Extensions.Logging;
				Logger.LogInformation
					("Worker running at: {time}", System.DateTimeOffset.Now);
			}

			return System.Threading.Tasks.Task.CompletedTask;
		}

		public void DoSomething(Dtos.Transaction transaction)
		{
			transaction.Result.Code = "100";
			transaction.Result.Message = "OK";

			transaction.Result.Number =
				System.Guid.NewGuid().ToString().Replace("-", string.Empty);
		}
	}
}
