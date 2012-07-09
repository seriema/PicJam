using System.Diagnostics;

namespace PicJam
{
	public class JpegTranWrapper
	{
		static public void Optimize(string fileIn, string fileOut)
		{
			var start = new ProcessStartInfo
			{
				Arguments = "-optimize " + fileIn + " " + fileOut,
				FileName = @"jpegtran.exe",
				WindowStyle = ProcessWindowStyle.Hidden
			};

			Process.Start(start).WaitForExit(1000);
		}
	}
}
