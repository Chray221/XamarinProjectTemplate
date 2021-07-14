using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Util;
using Android.Views;
using XamProjectTemplate;
using XamProjectTemplate.Droid.Renderers;
using ImTools;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Text.Style;
using System.Text.RegularExpressions;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace XamProjectTemplate.Droid.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                Control.SetPadding(0, 0, 0, 0);
                this.Control.HorizontalScrollBarEnabled = false;
                this.Control.VerticalScrollBarEnabled = false;
                this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}
