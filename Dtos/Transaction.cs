namespace Dtos
{
	public class Transaction : object, Dtx.Messaging.Rpc.IMessage
	{
		public Transaction() : base()
		{
			InsertDateTime =
				System.DateTime.UtcNow;

			Bill = new Bill();
			Card = new Card();
			User = new User();
			Result = new Result();
			Customer = new Customer();
		}

		// **********
		public Bill Bill { get; set; }
		// **********

		// **********
		public Card Card { get; set; }
		// **********

		// **********
		public User User { get; set; }
		// **********

		// **********
		public Result Result { get; set; }
		// **********

		// **********
		public Customer Customer { get; set; }
		// **********



		// **********
		public System.DateTime InsertDateTime { get; set; }
		// **********

		// **********
		public System.DateTime? UpdateDateTime { get; set; }
		// **********

		// **********
		/// <summary>
		/// کد پیگیری - کدی که پی اس پی تولید می‌کند
		/// </summary>
		public string ConsistencyIdentity { get; set; }
		// **********



		// **********
		public string ClientIP { get; set; }
		// **********

		// **********
		public string ServerIP { get; set; }
		// **********

		// **********
		public string QueueName { get; set; }
		// **********

		// **********
		public string CorrelationId { get; set; }
		// **********

		// **********
		public string ReplyQueueName { get; set; }
		// **********
	}
}
