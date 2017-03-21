using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace set_wallpaper
{
	class Program
	{
		const int SetDesktopWallpaper = 20;
		const int UpdateIniFile = 0x01;
		const int SendWinIniChange = 0x02;
		const string RegkeyWallpaperStyle = "WallpaperStyle";
		const string RegkeyTileWallpaper = "TileWallpaper";

		const string Stretch = "2";
		const string NoTile = "0";

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				PrintHelp();
				return;
			}

			string path = args[0];

			SystemParametersInfo(SetDesktopWallpaper, 0, path, UpdateIniFile | SendWinIniChange);
			RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);

			key.SetValue(RegkeyWallpaperStyle, Stretch);
			key.SetValue(RegkeyTileWallpaper, NoTile);
			key.Close();
		}

		private static void PrintHelp()
		{
			string help = @"
Sets a *.bmp file as desktop wallpaper with 'Stretch' & 'No Tile' settings.
* Note: only .bmp format is accepted

Usage: 
/> set-wallpaper c:\Pictures\my-wallpaper.bmp
";
			Console.WriteLine(help);
			Console.Read();
		}
	}
}
