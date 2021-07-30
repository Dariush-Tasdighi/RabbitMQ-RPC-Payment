namespace Dtx.Messaging
{
	public static class Utility
	{
		static Utility()
		{
		}

		public static System.DateTime Now
		{
			get
			{
				return System.DateTime.UtcNow;
			}
		}

		public static string NowString
		{
			get
			{
				var result =
					Now.ToString("yyyy/MM/dd - HH:mm:ss:fff");

				return result;
			}
		}

		public static T ConvertJsonToObject<T>(string json)
		{
			T result =
				System.Text.Json.JsonSerializer.Deserialize<T>(json: json);

			return result;
		}

		public static string GetJsonFromByteArray(System.ReadOnlyMemory<byte> body)
		{
			byte[] byteArray = body.ToArray();

			string result =
				System.Text.Encoding.UTF8.GetString(bytes: byteArray);

			return result;
		}
	}
}
