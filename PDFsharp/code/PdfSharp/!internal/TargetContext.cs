﻿using PdfSharp.Core.Enums;

namespace PdfSharp.Internal
{
#if GDI && WPF
	/// <summary>
	///     Internal switch indicating what context has to be used if both GDI and WPF are defined.
	/// </summary>
	internal static class TargetContextHelper
	{
		public static XGraphicTargetContext TargetContext = XGraphicTargetContext.WPF;
	}
#endif
}