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

		public static string ConvertObjectToJson(object value)
		{
			string result =
				System.Text.Json.JsonSerializer.Serialize(value: value);

			return result;
		}

		public static byte[] ConvertJsonToByteArray(string json)
		{
			var result =
				System.Text.Encoding.UTF8.GetBytes(s: json);

			return result;
		}

		public static string ConvertByteArrayToJson(byte[] body)
		{
			string result =
				System.Text.Encoding.UTF8.GetString(bytes: body);

			return result;
		}

		public static string ConvertByteArrayToJson(System.ReadOnlyMemory<byte> body)
		{
			byte[] byteArray = body.ToArray();

			string result =
				ConvertByteArrayToJson(body: byteArray);

			return result;
		}
	}
}
