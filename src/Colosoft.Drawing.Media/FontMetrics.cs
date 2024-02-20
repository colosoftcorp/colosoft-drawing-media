using System;

namespace Colosoft.Drawing.Media
{
    public class FontMetrics : IFontMetrics
    {
        private readonly System.Windows.Media.Typeface typeface;
        private readonly System.Windows.Media.FormattedText baseFormattedText;
        private readonly System.Globalization.CultureInfo culture;

        public Font Font { get; }

        protected virtual System.Windows.Media.FontFamily FontFamily =>
            new System.Windows.Media.FontFamily(this.Font.FontFamily);

        protected virtual System.Windows.FontStyle Style
        {
            get
            {
                switch (this.Font.Style)
                {
                    case FontStyle.Italic:
                        return System.Windows.FontStyles.Italic;
                    case FontStyle.Oblique:
                        return System.Windows.FontStyles.Oblique;
                    default:
                        return System.Windows.FontStyles.Normal;
                }
            }
        }

        protected virtual System.Windows.FontWeight Weight =>
            this.Font.IsBold ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;

        protected System.Windows.Media.Typeface CreateTypeface() =>
            new System.Windows.Media.Typeface(
                this.FontFamily,
                this.Style,
                this.Weight,
                System.Windows.FontStretches.Normal);

        public double Height => this.baseFormattedText.Height;

        public double InternalLeading => this.Height - this.typeface.CapsHeight;

        public double Baseline => this.baseFormattedText.Baseline;

        public double LineSpacing => this.typeface.FontFamily.LineSpacing;

        public double Ascent => this.baseFormattedText.Height - this.baseFormattedText.Baseline;

        public double Descent => this.baseFormattedText.OverhangAfter;

        public FontMetrics(Font font, System.Globalization.CultureInfo culture)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            this.Font = font;
            this.culture = culture;
            this.typeface = this.CreateTypeface();
#pragma warning disable CS0618 // Type or member is obsolete
            this.baseFormattedText = new System.Windows.Media.FormattedText(
                "MgfSj",
                this.culture,
                System.Windows.FlowDirection.LeftToRight,
                this.typeface,
                font.Size,
                System.Windows.Media.Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public double StringWidth(string s, int startIndex, int length)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            var text = s.Substring(startIndex, length);
#pragma warning disable CS0618 // Type or member is obsolete
            var formattedText = new System.Windows.Media.FormattedText(
                text,
                this.culture,
                System.Windows.FlowDirection.LeftToRight,
                this.typeface,
                this.Font.Size,
                System.Windows.Media.Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete

            return formattedText.WidthIncludingTrailingWhitespace;
        }
    }
}
