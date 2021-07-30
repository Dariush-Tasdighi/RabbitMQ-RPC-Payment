namespace Application
{
	public class RpcClient : Dtx.Messaging.Rpc.BaseClient
	{
		public RpcClient(Microsoft.Extensions.Options.IOptions<Settings> options,
			Microsoft.Extensions.Logging.ILogger<RpcClient> logger) :
			base(settings: options.Value.Messaging, logger: logger)
		{
		}
	}
}
