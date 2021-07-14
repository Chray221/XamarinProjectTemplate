using System;
using Xamarin.Forms;

namespace XamProjectTemplate
{
    public class ZoomScrollView : ScrollView
    {
        public static readonly BindableProperty IsZoomableProperty =
            BindableProperty.Create(
                propertyName: nameof(IsZoomable),
                returnType: typeof(bool),
                declaringType: typeof(ZoomScrollView),
                defaultValue: true);

        public bool IsZoomable
        {
            get { return (bool)GetValue(IsZoomableProperty); }
            set { SetValue(IsZoomableProperty, value); }
        }


        public ZoomScrollView()
        {
        }
    }
}
