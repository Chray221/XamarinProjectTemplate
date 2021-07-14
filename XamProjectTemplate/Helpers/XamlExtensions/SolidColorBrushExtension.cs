using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamProjectTemplate
{
    public class SolidColorBrushExtension : IMarkupExtension<SolidColorBrush>
    {
        public Color Value { get; set; }
        public SolidColorBrush ProvideValue(IServiceProvider serviceProvider)
        {
            return new SolidColorBrush(Value);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<SolidColorBrush>).ProvideValue(serviceProvider);
        }
    }
}
