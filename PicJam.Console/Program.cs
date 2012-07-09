using System.IO;

namespace PicJam.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				System.Console.WriteLine("To use this sample program, write the file you want to mash and the output file name. Eg: 'picjam.exe [in] [out]");
				return;
			}

			var inFile = args[0];
			var outFile = args[1];

			if (!File.Exists(inFile))
			{
				System.Console.WriteLine("File '{0}' not found.", inFile);
				return;
			}
	
			// THE INTERESTING BIT
			var masher = new JpegMasher();
			masher.Mash(inFile, outFile);
			//

			var inSize = FileSizeKb(inFile);
			var outSize = FileSizeKb(outFile);

			System.Console.WriteLine("Original filesize: {0:0.00} kb", inSize);
			System.Console.WriteLine("New filesize:      {0:0.00} kb", outSize);
			System.Console.WriteLine("Saved space:       {0:0.00} %", (1 - outSize / inSize) * 100);
		}

		private static float FileSizeKb(string file)
		{
			return new FileInfo(file).Length / 1000.0f;
		}
	}
}
