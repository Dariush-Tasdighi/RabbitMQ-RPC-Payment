namespace Dtos
{
	public class Bill : object
	{
		public Bill() : base()
		{
		}

		// **********
		/// <summary>
		/// مبلغ قبض
		/// </summary>
		public int Amount { get; set; }
		// **********

		// **********
		/// <summary>
		/// شناسه قبض
		/// </summary>
		public string Identity { get; set; }
		// **********

		// **********
		/// <summary>
		/// شناسه پرداخت قبض
		/// </summary>
		public string PaymentIdentity { get; set; }
		// **********
	}
}
