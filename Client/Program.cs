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

			System.Console.WriteLine("[x] Sending RPC");

			int sleepMilliseconds = 100;

			while (1 == 1)
			{
				System.Threading.Thread.Sleep
					(millisecondsTimeout: sleepMilliseconds);

				var stopWatch =
					new System.Diagnostics.Stopwatch();

				stopWatch.Start();

				// **************************************************
				var transaction =
					CreateTransaction();

				//var jsonResult =
				//	await client.CallAsync(value: jsonMessage);

				var jsonResult =
					await serviceProvider.GetService<RpcClient>().CallAsync(value: transaction);

				var transactionResult =
					Dtx.Messaging.Utility.ConvertJsonToObject<Dtos.Transaction>(json: jsonResult);
				// **************************************************

				stopWatch.Stop();

				string message =
					$"{ transactionResult.Result.Code } - Elapsed: {stopWatch.Elapsed:ss\\:fff}";

				System.Console.WriteLine(message);
			}
		}

		private static Dtos.Transaction CreateTransaction()
		{
			var transaction =
				new Dtos.Transaction();

			transaction.Bill.Amount = 1000;
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
		}
	}
}
