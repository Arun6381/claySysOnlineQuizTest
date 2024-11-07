using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ClaysysOnlineQuizTest.Utitlities
{
	public static class Logger
	{
		// Define the path to the Logs folder
		private static readonly string logFolder = HttpContext.Current.Server.MapPath("~/Logs");

		// Ensure the Logs folder exists
		static Logger()
		{
			if (!Directory.Exists(logFolder))
			{
				Directory.CreateDirectory(logFolder); // Create Logs folder if it doesn't exist
			}
		}

		// Log activity, warning, or error
		public static void LogActivity(string message)
		{
			Log("Activity", message);
		}

		public static void LogWarning(string message)
		{
			Log("Warning", message);
		}

		public static void LogError(string message)
		{
			Log("Error", message);
		}

		// Method to log messages to a file
		private static void Log(string logType, string message)
		{
			try
			{
				string logFileName = $"logfile-{DateTime.Now.ToString("yyyy-MM-dd")}.log";
				string logFilePath = Path.Combine(logFolder, logFileName);

				// Ensure that the log file exists; if not, create it
				if (!File.Exists(logFilePath))
				{
					File.Create(logFilePath).Dispose(); // Dispose to close the file handle after creating it
				}

				// Write the log message to the file
				using (StreamWriter writer = new StreamWriter(logFilePath, true))
				{
					writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{logType}] {message}");
				}
			}
			catch (Exception ex)
			{
				// In case logging fails, you can log the error in a fallback location
				File.AppendAllText(HttpContext.Current.Server.MapPath("~/Logs/error.log"), $"Error occurred while logging: {ex.Message}\n");
			}
		}
	}
}