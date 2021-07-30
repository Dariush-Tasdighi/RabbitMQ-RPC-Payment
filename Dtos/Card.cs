namespace Dtos
{
	public class Card : object
	{
		public Card() : base()
		{
		}

		// **********
		/// <summary>
		/// شماره کارت
		/// </summary>
		public string Pan { get; set; }
		// **********

		// **********
		/// <summary>
		/// کد اعتبارسنجی دوم کارت
		/// </summary>
		public string Cvv2 { get; set; }
		// **********

		// **********
		/// <summary>
		/// رمز دوم (اينترنتی) کارت
		/// </summary>
		public string Pin2 { get; set; }
		// **********

		// **********
		/// <summary>
		/// تاريخ انقضای کارت
		/// </summary>
		public string ExpireDate { get; set; }
		// **********
	}
}
