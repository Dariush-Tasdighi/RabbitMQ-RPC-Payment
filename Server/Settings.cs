namespace Application
{
	public class Settings : object
	{
		public const string KeyName = "Settings";

		public Settings() : base()
		{
		}

		public Dtx.Messaging.Settings Messaging { get; set; }
	}
}
