using System;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using XamProjectTemplate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamProjectTemplate
{
    public class BoldTextXamlExtension : IMarkupExtension
    {
        public string Text { get; set; }
        public Xamarin.Forms.Label LabelSource { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (LabelSource != null)
            {
                Regex.Split(Text, "(<b>)+|(</b>)+");
                FormattedString formattedString = new FormattedString();
                if (Text.Contains("<b>") || Text.Contains("</b>"))
                {
                    
                    bool isNextBold = false;
                    foreach (string value in Regex.Split(Text, "(<b>)+|(</b>)+"))
                    {
                        if (value.Equals("<b>"))
                        {
                            isNextBold = true;
                        }
                        else if (value.Equals("</b>"))
                        {

                        }
                        else
                        {
                            formattedString.Spans.Add(new Span()
                            {
                                Text = value,
                                FontSize = LabelSource.FontSize,
                                TextColor = LabelSource.TextColor,
                                FontFamily = isNextBold ? FontResource.BOLD : LabelSource.FontFamily
                            });
                            isNextBold = false;
                        }
                    }
                    return formattedString;
                }
                formattedString.Spans.Add(new Span()
                {
                    Text = Text,
                    FontSize = LabelSource.FontSize,
                    TextColor = LabelSource.TextColor,
                    FontFamily = LabelSource.FontFamily
                });
                return formattedString;
            }
            throw(new Exception("LabelSource should not be empty"));

        }
    }
}
