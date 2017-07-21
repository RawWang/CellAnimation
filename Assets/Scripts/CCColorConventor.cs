using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCColorConventor 
{
	//十六进制颜色转换为RGB
	// use: HexToRGB("66ccff")
	public static Color HexToRGB(string hex)
	{
		byte r = byte.Parse (hex.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse (hex.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse (hex.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);

		return new Color32 (r, g, b, 255);
	}
}
