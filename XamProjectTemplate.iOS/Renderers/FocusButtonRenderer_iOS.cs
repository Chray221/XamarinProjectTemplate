using System;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamProjectTemplate;
using XamProjectTemplate.iOS.Renderers;

[assembly:ExportRenderer(typeof(FocusButton), typeof(FocusButtonRenderer_iOS))]
namespace XamProjectTemplate.iOS.Renderers
{
    public class FocusButtonRenderer_iOS : ButtonRenderer
    {
        bool isFocus;
        public FocusButtonRenderer_iOS()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Element.Clicked += FocusButton_Clicked;
        }

        private void FocusButton_Clicked(object sender, EventArgs e)
        {
            isFocus = true;
        }

        public override bool PointInside(CGPoint point, UIEvent uievent)
        {
            bool isFocusButtonFocused = base.PointInside(point, uievent);
            if(!isFocusButtonFocused && Element is FocusButton focusButton && isFocus)
            {
                focusButton.ExecuteUnfocusCommand();
                isFocus = false;
            }
            return isFocusButtonFocused;
        }
    }
}
