using System.IO;

namespace PicJam
{
	public class JpegMasher
	{
		public void Mash(string fileIn, string fileOut)
		{
			var extension = new FileInfo(fileOut).Extension;
			var fileMiddle = fileOut.Replace(extension, "middle" + extension);
			JpegTranWrapper.Optimize(fileIn, fileMiddle);
			JpegExifPatcher.PatchAwayExif(fileMiddle, fileOut);
			File.Delete(fileMiddle);
		}
	}
}
