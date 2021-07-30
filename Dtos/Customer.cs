namespace Dtos
{
	public class Customer : object
	{
		public Customer() : base()
		{
		}

		// **********
		/// <summary>
		/// شناسه مشتری
		/// اين عنصر به عنوان شناسه مشتری به صورت اختياری وجود دارد
		/// در صورت مقداردهی، اين تراکنش شناسه دار محسوب شده و در صورتحساب های
		/// بانک به حساب پذيرنده، منعکس خواهد شد
		/// </summary>
		public string Identity { get; set; }
		// **********

		// **********
		/// <summary>
		/// کد پذیرنده
		/// یک عدد ۱۵ رقمی
		/// </summary>
		public string AcceptorCode { get; set; }
		// **********

		// **********
		/// <summary>
		/// کد پایانه
		/// یک عدد ۸ رقمی
		/// </summary>
		public string TerminalCode { get; set; }
		// **********
	}
}
