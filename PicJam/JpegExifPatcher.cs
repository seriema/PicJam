using System.IO;

namespace PicJam
{
	// "Borrowed" from http://techmikael.blogspot.se/2009/07/removing-exif-data-continued.html
	public class JpegExifPatcher
	{
		static public void PatchAwayExif(string inFile, string outFile)
		{
			var inStream = new FileStream(inFile, FileMode.Open);
			var outStream = new FileStream(outFile, FileMode.OpenOrCreate);

			PatchAwayExif(inStream, outStream);

			inStream.Close();
			outStream.Close();
		}

		static public Stream PatchAwayExif(Stream inStream, Stream outStream)
		{
			var jpegHeader = new byte[2];
			jpegHeader[0] = (byte)inStream.ReadByte();
			jpegHeader[1] = (byte)inStream.ReadByte();
			if (jpegHeader[0] == 0xff && jpegHeader[1] == 0xd8) //check if it's a jpeg file
			{
				SkipAppHeaderSection(inStream);
			}
			outStream.WriteByte(0xff);
			outStream.WriteByte(0xd8);

			int readCount;
			var readBuffer = new byte[4096];
			while ((readCount = inStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
				outStream.Write(readBuffer, 0, readCount);

			return outStream;
		}

		private static void SkipAppHeaderSection(Stream inStream)
		{
			var header = new byte[2];
			header[0] = (byte)inStream.ReadByte();
			header[1] = (byte)inStream.ReadByte();

			while (header[0] == 0xff && (header[1] >= 0xe0 && header[1] <= 0xef))
			{
				var exifLength = inStream.ReadByte();
				exifLength = exifLength << 8;
				exifLength |= inStream.ReadByte();

				for (var i = 0; i < exifLength - 2; i++)
				{
					inStream.ReadByte();
				}
				header[0] = (byte)inStream.ReadByte();
				header[1] = (byte)inStream.ReadByte();
			}
			inStream.Position -= 2; //skip back two bytes
		}
	}
}
