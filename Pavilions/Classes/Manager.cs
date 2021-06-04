using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Drawing;

namespace Pavilions.Classes
{
	class Manager
	{
		public static int IdStaffer { get; set; }
		public static ManagerCMenu cMenu { get; set; }
		public static BitmapImage CreateSource(byte[] bytes)
		{
			var bi = new BitmapImage();
			MemoryStream stream = new MemoryStream(bytes);
			Bitmap bmp = (Bitmap)System.Drawing.Image.FromStream(stream);
			using (var ms = new MemoryStream())
			{
				bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				ms.Position = 0;
				bi.BeginInit();
				bi.CacheOption = BitmapCacheOption.OnLoad;
				bi.StreamSource = ms;
				bi.EndInit();
			}
			return bi;
		}
	}
}
