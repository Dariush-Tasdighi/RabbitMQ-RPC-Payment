namespace Dtos
{
	public class Result : object
	{
		public Result() : base()
		{
		}

		// **********
		public string Code { get; set; }
		// **********

		// **********
		/// <summary>
		/// شماره ارجاع - شماره‌ای که سوئيچ یا خدمات برای ما ارسال می‌کند
		/// </summary>
		public string Number { get; set; }
		// **********

		// **********
		public string Message { get; set; }
		// **********
	}
}
