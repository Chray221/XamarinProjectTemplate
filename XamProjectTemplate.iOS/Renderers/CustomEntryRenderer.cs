﻿using System;
using XamProjectTemplate;
using XamProjectTemplate.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using CoreGraphics;
using System.ComponentModel;
using System.Linq;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace XamProjectTemplate.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;
        CustomEntry entry;
        CustomEntry CustomElement => Element as CustomEntry;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            entry = (CustomEntry)this.Element;
            if (this.Control == null)
            {
                return;
            }
            if (e.OldElement != null)
            {
                UnregisterForKeyboardNotifications();
            }
            if (e.NewElement != null)
            {
                RegisterForKeyboardNotifications();
                Control.BorderStyle = UITextBorderStyle.None;
                Control.SpellCheckingType = UITextSpellCheckingType.No;
                Control.AutocorrectionType = UITextAutocorrectionType.No;

                if (entry.AutoCapitalization == AutoCapitalizationType.Words)
                {
                    Control.AutocapitalizationType = UITextAutocapitalizationType.Words;
                }
                else if (entry.AutoCapitalization == AutoCapitalizationType.Sentences)
                {
                    Control.AutocapitalizationType = UITextAutocapitalizationType.Sentences;
                }
                else if (entry.AutoCapitalization == AutoCapitalizationType.All)
                {
                    Control.AutocapitalizationType = UITextAutocapitalizationType.AllCharacters;
                }
                else
                {
                    Control.AutocapitalizationType = UITextAutocapitalizationType.None;
                }

                switch(entry.ContentType)
                {
                    case ContentType.OneTimeCode:
                        Control.TextContentType = UITextContentType.OneTimeCode;
                        break;
                }
                UpdatePlaceholderFont();
                //Control.AutocapitalizationType = UITextAutocapitalizationType.None;
                //if (entry.Keyboard == Keyboard.Numeric || entry.Keyboard == Keyboard.Telephone)
                //{
                //    var toolbar = new UIToolbar(new System.Drawing.RectangleF(0.0f, 0.0f, (float)Control.Frame.Size.Width, 44.0f));
                //    toolbar.Items = new[]
                //    {
                //        new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                //        new UIBarButtonItem(entry.ReturnType.ToString(), UIBarButtonItemStyle.Bordered, delegate
                //        {
                //            if(entry.ReturnType == ReturnType.Next)
                //            {
                //                if(entry.NextView != null)
                //                {
                //                    entry.NextView.Focus();
                //                }
                //            }
                //            else if(entry.ReturnType == ReturnType.Done)
                //            {
                //                Control.ResignFirstResponder();
                //                //entry.InvokeCompleted();
                //            }
                //        })
                //    };
                //    Control.InputAccessoryView = toolbar;
                //}
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomEntry.PlaceholderFontFamilyProperty.PropertyName)
                UpdatePlaceholderFont();
        }

        void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
        }

        void UnregisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver != null)
            {
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }
            if (_keyboardHideObserver != null)
            {
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }

        void OnKeyboardShow(object sender, UIKeyboardEventArgs args)
        {
            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            CGSize keyboardSize = result.RectangleFValue.Size;
            if (entry != null && entry.MoveUp && Control.IsEditing)
            {
                UIView parentWindow = getParentView();
                if (parentWindow != null)
                {
                    var textfieldRect = parentWindow.ConvertRectFromView(Control.Bounds, Control);
                    var viewRect = parentWindow.ConvertRectFromView(parentWindow.Bounds, parentWindow);
                    float textfieldY = (float)(textfieldRect.Y + textfieldRect.Height + entry.MoveUpOffset);
                    float keyboardY = (float)(viewRect.Height - keyboardSize.Height);
                    var viewFrame = parentWindow.Frame;
                    App.Log("textfieldY: " + textfieldY + " keyboardY: " + keyboardY);
                    if (textfieldY <= keyboardY)
                    {
                        viewFrame.Y = 0.0f;
                    }
                    else
                    {
                        viewFrame.Y = -(float)Math.Floor(textfieldY - keyboardY);
                    }
                    UIView.BeginAnimations(null, (IntPtr)null);
                    UIView.SetAnimationBeginsFromCurrentState(true);
                    UIView.SetAnimationDuration(entry.MoveUpAnimationSpeed / 1000);
                    parentWindow.Frame = viewFrame;
                    UIView.CommitAnimations();
                }
            }
        }

        void OnKeyboardHide(object sender, UIKeyboardEventArgs args)
        {
            if (entry != null && entry.MoveUp)
            {
                UIView parentView = getParentView();
                if (parentView != null)
                {
                    App.Log("OnKeyboardHide");
                    var viewFrame = parentView.Bounds;
                    viewFrame.Y = 0.0f;
                    UIView.BeginAnimations(null, (IntPtr)null);
                    UIView.SetAnimationBeginsFromCurrentState(true);
                    UIView.SetAnimationDuration(entry.MoveUpAnimationSpeed / 1000);
                    parentView.Frame = viewFrame;
                    UIView.CommitAnimations();
                }
            }
        }

        UIView getParentView()
        {
            UIView view = Control.Superview;
            while (view != null && !(view is UIWindow))
            {
                view = view.Superview;
            }
            return view;
        }

        private void UpdatePlaceholderFont()
        {
            if (CustomElement.Placeholder != null)
            {
                var paragraphStyle = new NSMutableParagraphStyle
                {
                    Alignment = UITextAlignment.Left
                };

                var placeholderFont = FindFont(CustomElement.PlaceholderFontFamily, (float)CustomElement.FontSize);

                Control.AttributedPlaceholder = new NSAttributedString(CustomElement.Placeholder, placeholderFont, foregroundColor: (CustomElement.PlaceholderColor).ToUIColor(), paragraphStyle: paragraphStyle);
            }
        }

        private UIFont FindFont(string fontFamily, float fontSize)
        {
            if (fontFamily != null)
            {
                try
                {
                    if (UIFont.FamilyNames.Contains(fontFamily))
                    {
                        var descriptor = new UIFontDescriptor().CreateWithFamily(fontFamily);
                        return UIFont.FromDescriptor(descriptor, fontSize);
                    }

                    return UIFont.FromName(fontFamily, fontSize);
                }
                catch { }
            }

            return UIFont.SystemFontOfSize(fontSize);
        }

    }
}

