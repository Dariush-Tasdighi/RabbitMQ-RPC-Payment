using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class Program
	{
		static Program()
		{
		}

		private static async System.Threading.Tasks.Task Main()
		{
			// **************************************************
			var services =
				new Microsoft.Extensions.DependencyInjection.ServiceCollection();

			ConfigureServices(services);

			var serviceProvider =
				services.BuildServiceProvider();

			//var client =
			//	new RpcClient();
			// **************************************************

			int sleepMilliseconds = 100;

			System.Console.WriteLine("[x] Sending RPC");

			while (1 == 1)
			{
				var stopWatch =
					new System.Diagnostics.Stopwatch();

				stopWatch.Start();

				System.Threading.Thread.Sleep
					(millisecondsTimeout: sleepMilliseconds);

				// **************************************************
				string jsonMessage =
					CreateJsonMessage();

				//var jsonResult =
				//	await client.CallAsync(message: jsonMessage);

				var jsonResult =
					await serviceProvider.GetService<RpcClient>().CallAsync(message: jsonMessage);

				var transactionResult =
					Dtx.Messaging.Utility.ConvertJsonToObject<Dtos.Transaction>(json: jsonResult);
				// **************************************************

				stopWatch.Stop();

				var extraTime =
					new System.TimeSpan
					(days: 0, hours: 0, minutes: 0, seconds: 0, milliseconds: sleepMilliseconds);

				var elapsedTime =
					stopWatch.Elapsed - extraTime;

				string elapsedSecondsString =
					elapsedTime.Seconds.ToString().PadLeft(totalWidth: 2, paddingChar: '_');

				string elapsedMillisecondsString =
					elapsedTime.Milliseconds.ToString().PadLeft(totalWidth: 4, paddingChar: '_');

				string message =
					$"{ transactionResult.Result.Code } - Seconds: { elapsedSecondsString } - Milliseconds: { elapsedMillisecondsString }";

				System.Console.WriteLine(message);
			}
		}

		private static string CreateJsonMessage()
		{
			var transaction =
				CreateTransaction();

			string jsonMessage =
				System.Text.Json.JsonSerializer.Serialize(transaction);

			return jsonMessage;
		}

		private static Dtos.Transaction CreateTransaction()
		{
			var transaction =
				new Dtos.Transaction();

			transaction.Bill.Amount = 100;
			transaction.Bill.Identity = "12345";
			transaction.Bill.PaymentIdentity = "54321";

			transaction.Card.Pan = "1234 1234 1234 1234";
			transaction.Card.Cvv2 = "123";
			transaction.Card.Pin2 = "12341234";
			transaction.Card.ExpireDate = "0303";

			transaction.Customer.AcceptorCode = "111112222233333";
			transaction.Customer.Identity = "ABCD";
			transaction.Customer.TerminalCode = "12345678";

			transaction.User.IP = "192.168.1.1";
			transaction.User.CellPhoneNumber = "09121087461";
			transaction.User.EmailAddress = "DariushT@GMail.com";
			transaction.User.MacAddress = "AB:CD:EF:GH:IJ";

			transaction.ConsistencyIdentity =
				System.Guid.NewGuid().ToString().Replace("-", string.Empty);

			return transaction;
		}

		private static void ConfigureServices
			(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			services.AddLogging(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
			});

			var configuration =
				new Microsoft.Extensions.Configuration.ConfigurationBuilder()
				.SetBasePath(System.IO.Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false)
				.AddEnvironmentVariables()
				.Build();

			services.Configure<Settings>(configuration.GetSection(Settings.KeyName));

			services.AddSingleton<RpcClient>();
			//services.AddTransient<RpcClient>();
		}
	}
}
