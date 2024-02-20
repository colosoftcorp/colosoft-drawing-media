using System;

namespace Colosoft.Drawing.Media
{
    public class FontMetricsProvider : IFontMetricsProvider
    {
        public System.Globalization.CultureInfo Culture { get; }

        public FontMetricsProvider(System.Globalization.CultureInfo culture = null)
        {
            this.Culture = culture ?? System.Globalization.CultureInfo.InvariantCulture;
        }

        public IFontMetrics GetMetrics(Font font)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            return new FontMetrics(font, this.Culture);
        }
    }
}
