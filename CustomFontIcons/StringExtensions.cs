using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace UIKitAbuse.CustomFontIcons {
    public static class StringExtensions {
        /// <summary>
        /// Gets the maximum font size that will fit the given string within the desired size area.
        /// </summary>
        /// <returns>The max font size.</returns>
        /// <param name="source">Source.</param>
        /// <param name="font">Font.</param>
        /// <param name="sizeRestriction">Size restriction.</param>
        public static float GetMaxFontSize(this string source, UIFont font, SizeF sizeRestriction) {
            // The expected StringSize method doesn't return a useful value for me, so here's a hack.
            // NSString.StringSize(font, 0f, ref maximumFontSize, textWidthRestriction, lineBreakMode);
            // This is only accurate within a 0.1f value.

            float maxFontSize = font.PointSize;
            SizeF latest = SizeF.Empty;
            using (NSString nssDescriptionWithoutHtml = new NSString(source.ToString())) {
                while (latest.Width < sizeRestriction.Width && latest.Height < sizeRestriction.Height) {
                    latest = nssDescriptionWithoutHtml.StringSize(font.WithSize(maxFontSize), sizeRestriction.Width, UILineBreakMode.Clip);
                    if (latest.Width < sizeRestriction.Width && latest.Height < sizeRestriction.Height) {
                        maxFontSize += 0.1f;
                    }
                }
            }
            return maxFontSize;
        }
    }
}