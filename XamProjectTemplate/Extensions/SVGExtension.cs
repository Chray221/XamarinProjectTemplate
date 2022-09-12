using System;
using FFImageLoading.Svg.Forms;

namespace XamProjectTemplate.Helpers.Extentsions
{
    public static class SVGExtension
    {
        static SvgImageSourceConverter svgImageSourceConverter = new SvgImageSourceConverter();
        public static string SVGPathFormat { get { return "resource://XamProjectTemplate.Resources.SVG.{0}.svg"; } }
        /// <summary>
        /// Gets the svg file path using image name.
        /// <para>and returns:</para>
        /// <para>resource://XamProjectTemplate.Resources.SVG.[imageName].svg</para>
        /// </summary>
        /// <param name="imageName"></param>
        /// <value>resource://XamProjectTemplate.Resources.SVG.[imageName].svg</value>
        /// <returns>resource://XamProjectTemplate.Resources.SVG.[imageName].svg</returns>
        public static string GetSVGPath(this string imageName)
        {
            if (string.IsNullOrEmpty(imageName)) return null;
            return string.Format(SVGPathFormat, imageName);
        }

        public static object GetSVGImageSource(this string imageName)
        {
            if (string.IsNullOrEmpty(imageName)) return null;
            return svgImageSourceConverter.ConvertFromInvariantString(GetSVGPath(imageName));
        }
    }
}
//TODO Add FFImageLoading plugin