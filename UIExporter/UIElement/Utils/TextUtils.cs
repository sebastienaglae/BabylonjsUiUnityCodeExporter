using System.Linq;
using TMPro;

namespace PROJECT
{
	
public static class TextUtils
{
	public enum TextWrapping
	{
		WordWrap,
		Clip,
		WordWrapEllipsis,
		Ellipsis,
		Truncate,
		None
	}

	public static TextWrapping GetOverflow(TextMeshProUGUI textPro)
	{
		switch (textPro.overflowMode)
		{
		case TextOverflowModes.Overflow:
			return textPro.enableWordWrapping ? TextWrapping.WordWrap : TextWrapping.Clip;
		case TextOverflowModes.Ellipsis:
			return textPro.enableWordWrapping ? TextWrapping.WordWrapEllipsis : TextWrapping.Ellipsis;
		case TextOverflowModes.Truncate:
			return TextWrapping.Truncate;
		case TextOverflowModes.ScrollRect:
		case TextOverflowModes.Masking:
		case TextOverflowModes.Page:
		case TextOverflowModes.Linked:
		default:
			return TextWrapping.None;
		}
	}

	public static bool GetFontOptions(FontStyles target, FontStyles flags)
	{
		return flags.GetFlags().Contains(target);
	}

	public static string GetFontStylePro(TextMeshProUGUI textPro)
	{
		if (GetFontOptions(FontStyles.Italic, textPro.fontStyle))
			return FontStyles.Italic.ToString().ToLower();

		return FontStyles.Normal.ToString().ToLower();
	}

	public static string GetFontWeightPro(TextMeshProUGUI textPro)
	{
		if (GetFontOptions(FontStyles.Bold, textPro.fontStyle))
			return FontStyles.Bold.ToString().ToLower();

		return FontStyles.Normal.ToString().ToLower();
	}

	public static int GetVerticalAlignmentPro(TextMeshProUGUI textPro)
	{
		switch (textPro.alignment)
		{
		case TextAlignmentOptions.TopLeft:
		case TextAlignmentOptions.Top:
		case TextAlignmentOptions.TopRight:
		case TextAlignmentOptions.TopJustified:
		case TextAlignmentOptions.TopFlush:
		case TextAlignmentOptions.TopGeoAligned:
			return (int)TextVerticalAlignment.TOP;
		case TextAlignmentOptions.Left:
		case TextAlignmentOptions.Center:
		case TextAlignmentOptions.Right:
		case TextAlignmentOptions.Justified:
		case TextAlignmentOptions.Flush:
		case TextAlignmentOptions.CenterGeoAligned:
			return (int)TextVerticalAlignment.CENTER;
		case TextAlignmentOptions.BottomLeft:
		case TextAlignmentOptions.Bottom:
		case TextAlignmentOptions.BottomRight:
		case TextAlignmentOptions.BottomJustified:
		case TextAlignmentOptions.BottomFlush:
		case TextAlignmentOptions.BottomGeoAligned:
			return (int)TextVerticalAlignment.BOTTOM;
		case TextAlignmentOptions.BaselineLeft:
		case TextAlignmentOptions.Baseline:
		case TextAlignmentOptions.BaselineRight:
		case TextAlignmentOptions.BaselineJustified:
		case TextAlignmentOptions.BaselineFlush:
		case TextAlignmentOptions.BaselineGeoAligned:
			return (int)TextVerticalAlignment.BOTTOM;
		case TextAlignmentOptions.MidlineLeft:
		case TextAlignmentOptions.Midline:
		case TextAlignmentOptions.MidlineRight:
		case TextAlignmentOptions.MidlineJustified:
		case TextAlignmentOptions.MidlineFlush:
		case TextAlignmentOptions.MidlineGeoAligned:
			return (int)TextVerticalAlignment.CENTER;
		case TextAlignmentOptions.CaplineLeft:
		case TextAlignmentOptions.Capline:
		case TextAlignmentOptions.CaplineRight:
		case TextAlignmentOptions.CaplineJustified:
		case TextAlignmentOptions.CaplineFlush:
		case TextAlignmentOptions.CaplineGeoAligned:
		case TextAlignmentOptions.Converted:
		default:
			return (int)TextVerticalAlignment.TOP;
		}
	}

	public static int GetHorizontalAlignmentPro(TextMeshProUGUI textPro)
	{
		switch (textPro.alignment)
		{
		case TextAlignmentOptions.TopLeft:
		case TextAlignmentOptions.Left:
		case TextAlignmentOptions.BottomLeft:
		case TextAlignmentOptions.BaselineLeft:
		case TextAlignmentOptions.MidlineLeft:
		case TextAlignmentOptions.CaplineLeft:
			return (int)TextHorizontalAlignment.LEFT;
		case TextAlignmentOptions.TopRight:
		case TextAlignmentOptions.Right:
		case TextAlignmentOptions.BottomRight:
		case TextAlignmentOptions.BaselineRight:
		case TextAlignmentOptions.MidlineRight:
		case TextAlignmentOptions.CaplineRight:
			return (int)TextHorizontalAlignment.RIGHT;
		case TextAlignmentOptions.Center:
		case TextAlignmentOptions.CenterGeoAligned:
		case TextAlignmentOptions.Midline:
		case TextAlignmentOptions.MidlineJustified:
		case TextAlignmentOptions.MidlineFlush:
		case TextAlignmentOptions.MidlineGeoAligned:
			return (int)TextHorizontalAlignment.CENTER;
		case TextAlignmentOptions.Top:
		case TextAlignmentOptions.TopJustified:
		case TextAlignmentOptions.TopFlush:
		case TextAlignmentOptions.TopGeoAligned:
		case TextAlignmentOptions.Justified:
		case TextAlignmentOptions.Flush:
		case TextAlignmentOptions.Bottom:
		case TextAlignmentOptions.BottomJustified:
		case TextAlignmentOptions.BottomFlush:
		case TextAlignmentOptions.BottomGeoAligned:
		case TextAlignmentOptions.Baseline:
		case TextAlignmentOptions.BaselineJustified:
		case TextAlignmentOptions.BaselineFlush:
		case TextAlignmentOptions.BaselineGeoAligned:
		case TextAlignmentOptions.Capline:
		case TextAlignmentOptions.CaplineJustified:
		case TextAlignmentOptions.CaplineFlush:
		case TextAlignmentOptions.CaplineGeoAligned:
		case TextAlignmentOptions.Converted:
		default:
			return (int)TextHorizontalAlignment.CENTER;
		}
	}

	private enum TextHorizontalAlignment
	{
		LEFT,
		RIGHT,
		CENTER
	}

	private enum TextVerticalAlignment
	{
		TOP,
		BOTTOM,
		CENTER
	}
}
}
