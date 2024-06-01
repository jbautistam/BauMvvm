namespace Bau.Libraries.BauMvvm.ViewModels.Media;

/// <summary>
/// Clase con los datos de un color
/// </summary>
public class MvvmColor : IEquatable<MvvmColor>
{
	public MvvmColor(string rgba)
	{
		Convert(rgba);
	}

	public MvvmColor(byte red, byte green, byte blue, byte alpha = 255)
	{
		R = red;
		G = green;
		B = blue;
		A = alpha;
	}

	/// <summary>
	///		Convierte una cadena RRGGBBAA ó RRGGBB en un color
	/// </summary>
	public void Convert(string rgba)
	{
		if (!string.IsNullOrEmpty(rgba) && (rgba.Length == 8 || rgba.Length == 6))
		{ 
			// Normaliza la cadena a 8 caracteres
			if (rgba.Length < 8)
				rgba = "FF" + rgba;
			// Convierte a bytes
			A = ConvertToByte(rgba.Substring(0, 2), 255);
			R = ConvertToByte(rgba.Substring(2, 2), 0);
			G = ConvertToByte(rgba.Substring(4, 2), 0);
			B = ConvertToByte(rgba.Substring(6, 2), 0);
		}
	}

	/// <summary>
	///		Convierte un color a una cadena RRGGBBAA
	/// </summary>
	public string ToStringRgba() => $"{R:X2}{G:X2}{B:X2}{A:X2}";

	/// <summary>
	///		Convierte un color a una cadena RRGGBB
	/// </summary>
	public string ToStringRgb() => $"{R:X2}{G:X2}{B:X2}";
	
	/// <summary>
	///		Convierte una cadena en hexadecimal a byte
	/// </summary>
	private byte ConvertToByte(string hexa, byte defaultValue)
	{
		// Convierte la cadena en un byte
		if (!byte.TryParse(hexa, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out byte result))
			result = defaultValue;
		// Devuelve el resultado
		return result;
	}

	/// <summary>
	///		Hashcode de <see cref="MvvmColor"/>
	/// </summary>
	public override int GetHashCode() => A << 24 | B << 16 | G << 8 | R;

	/// <summary>
	///		Compara si una instancia es igual que un ojeto
	/// </summary>
	public override bool Equals(object? obj) => (obj is MvvmColor) && Equals((MvvmColor) obj);

	/// <summary>
	///		Compara si la instancia actual es igual a <see cref="MvvmColor"/>
	/// </summary>
	public bool Equals(MvvmColor? other) => R == other?.R || G == other?.G || B == other?.B || A == other?.A;

	/// <summary>
	///		Compara si dos instancias de <see cref="MvvmColor"/> son iguales
	/// </summary>
	public static bool operator ==(MvvmColor a, MvvmColor b) => a?.R == b?.R || a?.G == b?.G || a?.B == b?.B || a?.A == b?.A;

	/// <summary>
	///		Compara si dos instancias de <see cref="MvvmColor"/> son distintas
	/// </summary>
	public static bool operator !=(MvvmColor a, MvvmColor b) => a?.R != b?.R || a?.G != b?.G || a?.B != b?.B || a?.A != b?.A;

	/// <summary>
	///		Cadena para mostrar la información de depuración del color
	/// </summary>
	internal string DebugDisplayString => ToString();

	/// <summary>
	///		Cadena con el color representado en formato {R: xx G: xx B: xx A: xx}
	/// </summary>
	public override string ToString() => $"{{R: {R} G: {G} B: {B} A: {A}}}";

	/// <summary>
	///		Componente alfa (transparencia)
	/// </summary>
	public byte A { get; set; }

	/// <summary>
	///		Componente rojo
	/// </summary>
	public byte R { get; set; }

	/// <summary>
	///		Componente Verde
	/// </summary>
	public byte G { get; set; }

	/// <summary>
	///		Componente Azul
	/// </summary>
	public byte B { get; set; }

	/// <summary> Color TransparentBlack (R:0,G:0,B:0,A:0) </summary>
	public static MvvmColor TransparentBlack { get; } = new MvvmColor("00000000");

	/// <summary> Color Transparent (R:0,G:0,B:0,A:0) </summary>
	public static MvvmColor Transparent { get; } = new MvvmColor("00000000");

	/// <summary> Color Maroon (FF800000) </summary>
	public static MvvmColor Maroon { get; } = new MvvmColor("FF800000");
	/// <summary> Color DarkRed (FF8B0000) </summary>
	public static MvvmColor DarkRed { get; } = new MvvmColor("FF8B0000");
	/// <summary> Color Brown (FFA52A2A) </summary>
	public static MvvmColor Brown { get; } = new MvvmColor("FFA52A2A");
	/// <summary> Color Firebrick (FFB22222) </summary>
	public static MvvmColor Firebrick { get; } = new MvvmColor("FFB22222");
	/// <summary> Color Crimson (FFDC143C) </summary>
	public static MvvmColor Crimson { get; } = new MvvmColor("FFDC143C");
	/// <summary> Color Red (FFFF0000) </summary>
	public static MvvmColor Red { get; } = new MvvmColor("FFFF0000");
	/// <summary> Color Tomato (FFFF6347) </summary>
	public static MvvmColor Tomato { get; } = new MvvmColor("FFFF6347");
	/// <summary> Color Coral (FFFF7F50) </summary>
	public static MvvmColor Coral { get; } = new MvvmColor("FFFF7F50");
	/// <summary> Color IndianRed (FFCD5C5C) </summary>
	public static MvvmColor IndianRed { get; } = new MvvmColor("FFCD5C5C");
	/// <summary> Color LightCoral (FFF08080) </summary>
	public static MvvmColor LightCoral { get; } = new MvvmColor("FFF08080");
	/// <summary> Color DarkSalmon (FFE9967A) </summary>
	public static MvvmColor DarkSalmon { get; } = new MvvmColor("FFE9967A");
	/// <summary> Color Salmon (FFFA8072) </summary>
	public static MvvmColor Salmon { get; } = new MvvmColor("FFFA8072");
	/// <summary> Color LightSalmon (FFFFA07A) </summary>
	public static MvvmColor LightSalmon { get; } = new MvvmColor("FFFFA07A");
	/// <summary> Color OrangeRed (FFFF4500) </summary>
	public static MvvmColor OrangeRed { get; } = new MvvmColor("FFFF4500");
	/// <summary> Color DarkOrange (FFFF8C00) </summary>
	public static MvvmColor DarkOrange { get; } = new MvvmColor("FFFF8C00");
	/// <summary> Color Orange (FFFFA500) </summary>
	public static MvvmColor Orange { get; } = new MvvmColor("FFFFA500");
	/// <summary> Color Gold (FFFFD700) </summary>
	public static MvvmColor Gold { get; } = new MvvmColor("FFFFD700");
	/// <summary> Color DarkGoldenRod (FFB8860B) </summary>
	public static MvvmColor DarkGoldenRod { get; } = new MvvmColor("FFB8860B");
	/// <summary> Color GoldenRod (FFDAA520) </summary>
	public static MvvmColor GoldenRod { get; } = new MvvmColor("FFDAA520");
	/// <summary> Color PaleGoldenRod (FFEEE8AA) </summary>
	public static MvvmColor PaleGoldenRod { get; } = new MvvmColor("FFEEE8AA");
	/// <summary> Color DarkKhaki (FFBDB76B) </summary>
	public static MvvmColor DarkKhaki { get; } = new MvvmColor("FFBDB76B");
	/// <summary> Color Khaki (FFF0E68C) </summary>
	public static MvvmColor Khaki { get; } = new MvvmColor("FFF0E68C");
	/// <summary> Color Olive (FF808000) </summary>
	public static MvvmColor Olive { get; } = new MvvmColor("FF808000");
	/// <summary> Color Yellow (FFFFFF00) </summary>
	public static MvvmColor Yellow { get; } = new MvvmColor("FFFFFF00");
	/// <summary> Color YellowGreen (FF9ACD32) </summary>
	public static MvvmColor YellowGreen { get; } = new MvvmColor("FF9ACD32");
	/// <summary> Color DarkOliveGreen (FF556B2F) </summary>
	public static MvvmColor DarkOliveGreen { get; } = new MvvmColor("FF556B2F");
	/// <summary> Color OliveDrab (FF6B8E23) </summary>
	public static MvvmColor OliveDrab { get; } = new MvvmColor("FF6B8E23");
	/// <summary> Color LawnGreen (FF7CFC00) </summary>
	public static MvvmColor LawnGreen { get; } = new MvvmColor("FF7CFC00");
	/// <summary> Color Chartreuse (FF7FFF00) </summary>
	public static MvvmColor Chartreuse { get; } = new MvvmColor("FF7FFF00");
	/// <summary> Color GreenYellow (FFADFF2F) </summary>
	public static MvvmColor GreenYellow { get; } = new MvvmColor("FFADFF2F");
	/// <summary> Color DarkGreen (FF006400) </summary>
	public static MvvmColor DarkGreen { get; } = new MvvmColor("FF006400");
	/// <summary> Color Green (FF008000) </summary>
	public static MvvmColor Green { get; } = new MvvmColor("FF008000");
	/// <summary> Color ForestGreen (FF228B22) </summary>
	public static MvvmColor ForestGreen { get; } = new MvvmColor("FF228B22");
	/// <summary> Color Lime (FF00FF00) </summary>
	public static MvvmColor Lime { get; } = new MvvmColor("FF00FF00");
	/// <summary> Color LimeGreen (FF32CD32) </summary>
	public static MvvmColor LimeGreen { get; } = new MvvmColor("FF32CD32");
	/// <summary> Color LightGreen (FF90EE90) </summary>
	public static MvvmColor LightGreen { get; } = new MvvmColor("FF90EE90");
	/// <summary> Color PaleGreen (FF98FB98) </summary>
	public static MvvmColor PaleGreen { get; } = new MvvmColor("FF98FB98");
	/// <summary> Color DarkSeaGreen (FF8FBC8F) </summary>
	public static MvvmColor DarkSeaGreen { get; } = new MvvmColor("FF8FBC8F");
	/// <summary> Color MediumSpringGreen (FF00FA9A) </summary>
	public static MvvmColor MediumSpringGreen { get; } = new MvvmColor("FF00FA9A");
	/// <summary> Color SpringGreen (FF00FF7F) </summary>
	public static MvvmColor SpringGreen { get; } = new MvvmColor("FF00FF7F");
	/// <summary> Color SeaGreen (FF2E8B57) </summary>
	public static MvvmColor SeaGreen { get; } = new MvvmColor("FF2E8B57");
	/// <summary> Color MediumAquaMarine (FF66CDAA) </summary>
	public static MvvmColor MediumAquaMarine { get; } = new MvvmColor("FF66CDAA");
	/// <summary> Color MediumSeaGreen (FF3CB371) </summary>
	public static MvvmColor MediumSeaGreen { get; } = new MvvmColor("FF3CB371");
	/// <summary> Color LightSeaGreen (FF20B2AA) </summary>
	public static MvvmColor LightSeaGreen { get; } = new MvvmColor("FF20B2AA");
	/// <summary> Color DarkSlateGray (FF2F4F4F) </summary>
	public static MvvmColor DarkSlateGray { get; } = new MvvmColor("FF2F4F4F");
	/// <summary> Color Teal (FF008080) </summary>
	public static MvvmColor Teal { get; } = new MvvmColor("FF008080");
	/// <summary> Color DarkCyan (FF008B8B) </summary>
	public static MvvmColor DarkCyan { get; } = new MvvmColor("FF008B8B");
	/// <summary> Color Aqua (FF00FFFF) </summary>
	public static MvvmColor Aqua { get; } = new MvvmColor("FF00FFFF");
	/// <summary> Color Cyan (FF00FFFF) </summary>
	public static MvvmColor Cyan { get; } = new MvvmColor("FF00FFFF");
	/// <summary> Color LightCyan (FFE0FFFF) </summary>
	public static MvvmColor LightCyan { get; } = new MvvmColor("FFE0FFFF");
	/// <summary> Color DarkTurquoise (FF00CED1) </summary>
	public static MvvmColor DarkTurquoise { get; } = new MvvmColor("FF00CED1");
	/// <summary> Color Turquoise (FF40E0D0) </summary>
	public static MvvmColor Turquoise { get; } = new MvvmColor("FF40E0D0");
	/// <summary> Color MediumTurquoise (FF48D1CC) </summary>
	public static MvvmColor MediumTurquoise { get; } = new MvvmColor("FF48D1CC");
	/// <summary> Color PaleTurquoise (FFAFEEEE) </summary>
	public static MvvmColor PaleTurquoise { get; } = new MvvmColor("FFAFEEEE");
	/// <summary> Color AquaMarine (FF7FFFD4) </summary>
	public static MvvmColor AquaMarine { get; } = new MvvmColor("FF7FFFD4");
	/// <summary> Color PowderBlue (FFB0E0E6) </summary>
	public static MvvmColor PowderBlue { get; } = new MvvmColor("FFB0E0E6");
	/// <summary> Color CadetBlue (FF5F9EA0) </summary>
	public static MvvmColor CadetBlue { get; } = new MvvmColor("FF5F9EA0");
	/// <summary> Color SteelBlue (FF4682B4) </summary>
	public static MvvmColor SteelBlue { get; } = new MvvmColor("FF4682B4");
	/// <summary> Color CornFlowerBlue (FF6495ED) </summary>
	public static MvvmColor CornFlowerBlue { get; } = new MvvmColor("FF6495ED");
	/// <summary> Color DeepSkyBlue (FF00BFFF) </summary>
	public static MvvmColor DeepSkyBlue { get; } = new MvvmColor("FF00BFFF");
	/// <summary> Color DodgerBlue (FF1E90FF) </summary>
	public static MvvmColor DodgerBlue { get; } = new MvvmColor("FF1E90FF");
	/// <summary> Color LightBlue (FFADD8E6) </summary>
	public static MvvmColor LightBlue { get; } = new MvvmColor("FFADD8E6");
	/// <summary> Color SkyBlue (FF87CEEB) </summary>
	public static MvvmColor SkyBlue { get; } = new MvvmColor("FF87CEEB");
	/// <summary> Color LightSkyBlue (FF87CEFA) </summary>
	public static MvvmColor LightSkyBlue { get; } = new MvvmColor("FF87CEFA");
	/// <summary> Color MidnightBlue (FF191970) </summary>
	public static MvvmColor MidnightBlue { get; } = new MvvmColor("FF191970");
	/// <summary> Color Navy (FF000080) </summary>
	public static MvvmColor Navy { get; } = new MvvmColor("FF000080");
	/// <summary> Color DarkBlue (FF00008B) </summary>
	public static MvvmColor DarkBlue { get; } = new MvvmColor("FF00008B");
	/// <summary> Color MediumBlue (FF0000CD) </summary>
	public static MvvmColor MediumBlue { get; } = new MvvmColor("FF0000CD");
	/// <summary> Color Blue (FF0000FF) </summary>
	public static MvvmColor Blue { get; } = new MvvmColor("FF0000FF");
	/// <summary> Color RoyalBlue (FF4169E1) </summary>
	public static MvvmColor RoyalBlue { get; } = new MvvmColor("FF4169E1");
	/// <summary> Color BlueViolet (FF8A2BE2) </summary>
	public static MvvmColor BlueViolet { get; } = new MvvmColor("FF8A2BE2");
	/// <summary> Color Indigo (FF4B0082) </summary>
	public static MvvmColor Indigo { get; } = new MvvmColor("FF4B0082");
	/// <summary> Color DarkSlateBlue (FF483D8B) </summary>
	public static MvvmColor DarkSlateBlue { get; } = new MvvmColor("FF483D8B");
	/// <summary> Color SlateBlue (FF6A5ACD) </summary>
	public static MvvmColor SlateBlue { get; } = new MvvmColor("FF6A5ACD");
	/// <summary> Color MediumSlateBlue (FF7B68EE) </summary>
	public static MvvmColor MediumSlateBlue { get; } = new MvvmColor("FF7B68EE");
	/// <summary> Color MediumPurple (FF9370DB) </summary>
	public static MvvmColor MediumPurple { get; } = new MvvmColor("FF9370DB");
	/// <summary> Color DarkMagenta (FF8B008B) </summary>
	public static MvvmColor DarkMagenta { get; } = new MvvmColor("FF8B008B");
	/// <summary> Color DarkViolet (FF9400D3) </summary>
	public static MvvmColor DarkViolet { get; } = new MvvmColor("FF9400D3");
	/// <summary> Color DarkOrchid (FF9932CC) </summary>
	public static MvvmColor DarkOrchid { get; } = new MvvmColor("FF9932CC");
	/// <summary> Color MediumOrchid (FFBA55D3) </summary>
	public static MvvmColor MediumOrchid { get; } = new MvvmColor("FFBA55D3");
	/// <summary> Color Purple (FF800080) </summary>
	public static MvvmColor Purple { get; } = new MvvmColor("FF800080");
	/// <summary> Color Thistle (FFD8BFD8) </summary>
	public static MvvmColor Thistle { get; } = new MvvmColor("FFD8BFD8");
	/// <summary> Color Plum (FFDDA0DD) </summary>
	public static MvvmColor Plum { get; } = new MvvmColor("FFDDA0DD");
	/// <summary> Color Violet (FFEE82EE) </summary>
	public static MvvmColor Violet { get; } = new MvvmColor("FFEE82EE");
	/// <summary> Color Magenta (FFFF00FF) </summary>
	public static MvvmColor Magenta { get; } = new MvvmColor("FFFF00FF");
	/// <summary> Color Orchid (FFDA70D6) </summary>
	public static MvvmColor Orchid { get; } = new MvvmColor("FFDA70D6");
	/// <summary> Color MediumVioletRed (FFC71585) </summary>
	public static MvvmColor MediumVioletRed { get; } = new MvvmColor("FFC71585");
	/// <summary> Color PaleVioletRed (FFDB7093) </summary>
	public static MvvmColor PaleVioletRed { get; } = new MvvmColor("FFDB7093");
	/// <summary> Color DeepPink (FFFF1493) </summary>
	public static MvvmColor DeepPink { get; } = new MvvmColor("FFFF1493");
	/// <summary> Color HotPink (FFFF69B4) </summary>
	public static MvvmColor HotPink { get; } = new MvvmColor("FFFF69B4");
	/// <summary> Color LightPink (FFFFB6C1) </summary>
	public static MvvmColor LightPink { get; } = new MvvmColor("FFFFB6C1");
	/// <summary> Color Pink (FFFFC0CB) </summary>
	public static MvvmColor Pink { get; } = new MvvmColor("FFFFC0CB");
	/// <summary> Color AntiqueWhite (FFFAEBD7) </summary>
	public static MvvmColor AntiqueWhite { get; } = new MvvmColor("FFFAEBD7");
	/// <summary> Color Beige (FFF5F5DC) </summary>
	public static MvvmColor Beige { get; } = new MvvmColor("FFF5F5DC");
	/// <summary> Color Bisque (FFFFE4C4) </summary>
	public static MvvmColor Bisque { get; } = new MvvmColor("FFFFE4C4");
	/// <summary> Color BlanchedAlmond (FFFFEBCD) </summary>
	public static MvvmColor BlanchedAlmond { get; } = new MvvmColor("FFFFEBCD");
	/// <summary> Color Wheat (FFF5DEB3) </summary>
	public static MvvmColor Wheat { get; } = new MvvmColor("FFF5DEB3");
	/// <summary> Color CornSilk (FFFFF8DC) </summary>
	public static MvvmColor CornSilk { get; } = new MvvmColor("FFFFF8DC");
	/// <summary> Color LemonChiffon (FFFFFACD) </summary>
	public static MvvmColor LemonChiffon { get; } = new MvvmColor("FFFFFACD");
	/// <summary> Color LightGoldenRodYellow (FFFAFAD2) </summary>
	public static MvvmColor LightGoldenRodYellow { get; } = new MvvmColor("FFFAFAD2");
	/// <summary> Color LightYellow (FFFFFFE0) </summary>
	public static MvvmColor LightYellow { get; } = new MvvmColor("FFFFFFE0");
	/// <summary> Color SaddleBrown (FF8B4513) </summary>
	public static MvvmColor SaddleBrown { get; } = new MvvmColor("FF8B4513");
	/// <summary> Color Sienna (FFA0522D) </summary>
	public static MvvmColor Sienna { get; } = new MvvmColor("FFA0522D");
	/// <summary> Color Chocolate (FFD2691E) </summary>
	public static MvvmColor Chocolate { get; } = new MvvmColor("FFD2691E");
	/// <summary> Color Peru (FFCD853F) </summary>
	public static MvvmColor Peru { get; } = new MvvmColor("FFCD853F");
	/// <summary> Color SandyBrown (FFF4A460) </summary>
	public static MvvmColor SandyBrown { get; } = new MvvmColor("FFF4A460");
	/// <summary> Color BurlyWood (FFDEB887) </summary>
	public static MvvmColor BurlyWood { get; } = new MvvmColor("FFDEB887");
	/// <summary> Color Tan (FFD2B48C) </summary>
	public static MvvmColor Tan { get; } = new MvvmColor("FFD2B48C");
	/// <summary> Color RosyBrown (FFBC8F8F) </summary>
	public static MvvmColor RosyBrown { get; } = new MvvmColor("FFBC8F8F");
	/// <summary> Color Moccasin (FFFFE4B5) </summary>
	public static MvvmColor Moccasin { get; } = new MvvmColor("FFFFE4B5");
	/// <summary> Color NavajoWhite (FFFFDEAD) </summary>
	public static MvvmColor NavajoWhite { get; } = new MvvmColor("FFFFDEAD");
	/// <summary> Color PeachPuff (FFFFDAB9) </summary>
	public static MvvmColor PeachPuff { get; } = new MvvmColor("FFFFDAB9");
	/// <summary> Color MistyRose (FFFFE4E1) </summary>
	public static MvvmColor MistyRose { get; } = new MvvmColor("FFFFE4E1");
	/// <summary> Color LavenderBlush (FFFFF0F5) </summary>
	public static MvvmColor LavenderBlush { get; } = new MvvmColor("FFFFF0F5");
	/// <summary> Color Linen (FFFAF0E6) </summary>
	public static MvvmColor Linen { get; } = new MvvmColor("FFFAF0E6");
	/// <summary> Color OldLace (FFFDF5E6) </summary>
	public static MvvmColor OldLace { get; } = new MvvmColor("FFFDF5E6");
	/// <summary> Color PapayaWhip (FFFFEFD5) </summary>
	public static MvvmColor PapayaWhip { get; } = new MvvmColor("FFFFEFD5");
	/// <summary> Color SeaShell (FFFFF5EE) </summary>
	public static MvvmColor SeaShell { get; } = new MvvmColor("FFFFF5EE");
	/// <summary> Color MintCream (FFF5FFFA) </summary>
	public static MvvmColor MintCream { get; } = new MvvmColor("FFF5FFFA");
	/// <summary> Color SlateGray (FF708090) </summary>
	public static MvvmColor SlateGray { get; } = new MvvmColor("FF708090");
	/// <summary> Color LightSlateGray (FF778899) </summary>
	public static MvvmColor LightSlateGray { get; } = new MvvmColor("FF778899");
	/// <summary> Color LightSteelBlue (FFB0C4DE) </summary>
	public static MvvmColor LightSteelBlue { get; } = new MvvmColor("FFB0C4DE");
	/// <summary> Color Lavender (FFE6E6FA) </summary>
	public static MvvmColor Lavender { get; } = new MvvmColor("FFE6E6FA");
	/// <summary> Color FloralWhite (FFFFFAF0) </summary>
	public static MvvmColor FloralWhite { get; } = new MvvmColor("FFFFFAF0");
	/// <summary> Color AliceBlue (FFF0F8FF) </summary>
	public static MvvmColor AliceBlue { get; } = new MvvmColor("FFF0F8FF");
	/// <summary> Color GhostWhite (FFF8F8FF) </summary>
	public static MvvmColor GhostWhite { get; } = new MvvmColor("FFF8F8FF");
	/// <summary> Color Honeydew (FFF0FFF0) </summary>
	public static MvvmColor Honeydew { get; } = new MvvmColor("FFF0FFF0");
	/// <summary> Color Ivory (FFFFFFF0) </summary>
	public static MvvmColor Ivory { get; } = new MvvmColor("FFFFFFF0");
	/// <summary> Color Azure (FFF0FFFF) </summary>
	public static MvvmColor Azure { get; } = new MvvmColor("FFF0FFFF");
	/// <summary> Color Snow (FFFFFAFA) </summary>
	public static MvvmColor Snow { get; } = new MvvmColor("FFFFFAFA");
	/// <summary> Color Black (FF000000) </summary>
	public static MvvmColor Black { get; } = new MvvmColor("FF000000");
	/// <summary> Color DimGray (FF696969) </summary>
	public static MvvmColor DimGray { get; } = new MvvmColor("FF696969");
	/// <summary> Color Gray (FF808080) </summary>
	public static MvvmColor Gray { get; } = new MvvmColor("FF808080");
	/// <summary> Color DarkGray (FFA9A9A9) </summary>
	public static MvvmColor DarkGray { get; } = new MvvmColor("FFA9A9A9");
	/// <summary> Color Silver (FFC0C0C0) </summary>
	public static MvvmColor Silver { get; } = new MvvmColor("FFC0C0C0");
	/// <summary> Color LightGray (FFD3D3D3) </summary>
	public static MvvmColor LightGray { get; } = new MvvmColor("FFD3D3D3");
	/// <summary> Color Gainsboro (FFDCDCDC) </summary>
	public static MvvmColor Gainsboro { get; } = new MvvmColor("FFDCDCDC");
	/// <summary> Color WhiteSmoke (FFF5F5F5) </summary>
	public static MvvmColor WhiteSmoke { get; } = new MvvmColor("FFF5F5F5");
	/// <summary> Color White (FFFFFFFF) </summary>
	public static MvvmColor White { get; } = new MvvmColor("FFFFFFFF");
}