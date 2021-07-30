namespace Dtx.Messaging
{
	public class Settings : object
	{
		public Settings() : base()
		{
			Port = 5672;
			Username = "guest";
			Password = "guest";
			HostName = "localhost";
		}

		// **********
		public int Port { get; set; }
		// **********

		// **********
		public string HostName { get; set; }
		// **********

		// **********
		public string Password { get; set; }
		// **********

		// **********
		public string Username { get; set; }
		// **********

		// **********
		public string QueueName { get; set; }
		// **********
	}
}
