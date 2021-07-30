namespace Dtos
{
	public class Transaction : object
	{
		public Transaction() : base()
		{
			Timestamp =
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
		public System.DateTime Timestamp { get; set; }
		// **********

		// **********
		/// <summary>
		/// کد پیگیری - کدی که پی اس پی تولید می‌کند
		/// </summary>
		public string ConsistencyIdentity { get; set; }
		// **********
	}
}
