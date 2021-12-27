using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XamProjectTemplate.DependencyServices;
using XamProjectTemplate.Resources;

namespace XamProjectTemplate
{
    public partial class MainNavigationPage : ContentPage
    {

        public static readonly BindableProperty NavigationBackgroundColorProperty = BindableProperty.Create(nameof(NavigationBackgroundColor), typeof(Color), typeof(MainNavigationPage), Color.Transparent);
        public Color NavigationBackgroundColor
        {
            set { SetValue(NavigationBackgroundColorProperty, value); }
            get { return (Color)GetValue(NavigationBackgroundColorProperty); }
        }

        public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(nameof(PageTitle), typeof(string), typeof(MainNavigationPage), null);
        public string PageTitle
        {
            set { SetValue(PageTitleProperty, value); }
            get { return (string)GetValue(PageTitleProperty); }
        }

        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(nameof(TitleFontSize), typeof(double), typeof(MainNavigationPage), 17.0);
        public double TitleFontSize
        {
            set { SetValue(TitleFontSizeProperty, value); }
            get { return (double)GetValue(TitleFontSizeProperty); }
        }

        public static readonly BindableProperty TitleFontFamilyProperty = BindableProperty.Create(nameof(TitleFontFamily), typeof(string), typeof(MainNavigationPage), FontResource.REGULAR);
        public string TitleFontFamily
        {
            set { SetValue(TitleFontFamilyProperty, value); }
            get { return (string)GetValue(TitleFontFamilyProperty); }
        }

        public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(nameof(TitleTextColor), typeof(Color), typeof(MainNavigationPage), Color.White);
        public Color TitleTextColor
        {
            set { SetValue(TitleTextColorProperty, value); }
            get { return (Color)GetValue(TitleTextColorProperty); }
        }

        public static readonly BindableProperty LeftTextProperty = BindableProperty.Create(nameof(LeftText), typeof(string), typeof(MainNavigationPage), null);
        public string LeftText
        {
            set { SetValue(LeftTextProperty, value); }
            get { return (string)GetValue(LeftTextProperty); }
        }

        public static readonly BindableProperty LeftIconProperty = BindableProperty.Create(nameof(LeftIcon), typeof(ImageSource), typeof(MainNavigationPage), null);
        public ImageSource LeftIcon
        {
            set { SetValue(LeftIconProperty, value); }
            get { return (ImageSource)GetValue(LeftIconProperty); }
        }

        public static readonly BindableProperty LeftIconSizeProperty =
            BindableProperty.Create(
                propertyName: nameof(LeftIconSize),
                returnType: typeof(double),
                declaringType: typeof(MainNavigationPage),
                defaultValue: (double)20.ScaleHeight());
        public double LeftIconSize
        {
            get { return (double)GetValue(LeftIconSizeProperty); }
            set { SetValue(LeftIconSizeProperty, value); }
        }

        public static readonly BindableProperty LeftIconTypeProperty =
            BindableProperty.Create(
                propertyName: nameof(LeftIconType),
                returnType: typeof(IconType),
                declaringType: typeof(MainNavigationPage),
                defaultValue: IconType.FontAwesome5Solid);
        public IconType LeftIconType
        {
            get { return (IconType)GetValue(LeftIconTypeProperty); }
            set { SetValue(LeftIconTypeProperty, value); }
        }

        public static readonly BindableProperty LeftIconColorProperty = BindableProperty.Create(nameof(LeftIconColor), typeof(Color), typeof(MainNavigationPage), Color.Default);
        public Color LeftIconColor
        {
            get { return (Color)GetValue(LeftIconColorProperty); }
            set { SetValue(LeftIconColorProperty, value); }
        }

        public static readonly BindableProperty LeftButtonCommandProperty = BindableProperty.Create(nameof(LeftButtonCommand), typeof(ICommand), typeof(MainNavigationPage), null);
        public ICommand LeftButtonCommand
        {
            set { SetValue(LeftButtonCommandProperty, value); }
            get { return (ICommand)GetValue(LeftButtonCommandProperty); }
        }

        public static readonly BindableProperty LeftButtonColorProperty = BindableProperty.Create(nameof(LeftButtonColor), typeof(Color), typeof(MainNavigationPage), Color.Transparent);
        public Color LeftButtonColor
        {
            set { SetValue(LeftButtonColorProperty, value); }
            get { return (Color)GetValue(LeftButtonColorProperty); }
        }

        public static readonly BindableProperty RightTextProperty = BindableProperty.Create(nameof(RightText), typeof(string), typeof(MainNavigationPage), null);
        public string RightText
        {
            set { SetValue(RightTextProperty, value); }
            get { return (string)GetValue(RightTextProperty); }
        }

        public static readonly BindableProperty RightIconProperty = BindableProperty.Create(nameof(RightIcon), typeof(ImageSource), typeof(MainNavigationPage), null);
        public ImageSource RightIcon
        {
            set { SetValue(RightIconProperty, value); }
            get { return (ImageSource)GetValue(RightIconProperty); }
        }
        public static readonly BindableProperty RightIconSizeProperty =
            BindableProperty.Create(
                propertyName: nameof(RightIconSize),
                returnType: typeof(double),
                declaringType: typeof(MainNavigationPage),
                defaultValue: (double)20.ScaleHeight());

        public double RightIconSize
        {
            get { return (double)GetValue(RightIconSizeProperty); }
            set { SetValue(RightIconSizeProperty, value); }
        }

        public static readonly BindableProperty RightIconTypeProperty =
            BindableProperty.Create(
                propertyName: nameof(RightIconType),
                returnType: typeof(IconType),
                declaringType: typeof(MainNavigationPage),
                defaultValue: IconType.FontAwesome5Solid);
        public IconType RightIconType
        {
            get { return (IconType)GetValue(RightIconTypeProperty); }
            set { SetValue(RightIconTypeProperty, value); }
        }

        public static readonly BindableProperty RightIconColorProperty = BindableProperty.Create(nameof(RightIconColor), typeof(Color), typeof(MainNavigationPage), Color.Default);
        public Color RightIconColor
        {
            set { SetValue(RightIconColorProperty, value); }
            get { return (Color)GetValue(RightIconColorProperty); }
        }

        public static readonly BindableProperty RightButtonCommandProperty = BindableProperty.Create(nameof(RightButtonCommand), typeof(ICommand), typeof(MainNavigationPage), null);
        public ICommand RightButtonCommand
        {
            set { SetValue(RightButtonCommandProperty, value); }
            get { return (ICommand)GetValue(RightButtonCommandProperty); }
        }

        public static readonly BindableProperty RightButtonColorProperty = BindableProperty.Create(nameof(RightButtonColor), typeof(Color), typeof(MainNavigationPage), Color.Transparent);
        public Color RightButtonColor
        {
            set { SetValue(RightButtonColorProperty, value); }
            get { return (Color)GetValue(RightButtonColorProperty); }
        }
        public static readonly BindableProperty StatusBarStyleProperty =
            BindableProperty.Create(
                propertyName: nameof(StatusBarStyle),
                returnType: typeof(BarStyle),
                declaringType: typeof(MainNavigationPage),
                defaultValue: BarStyle.Light,
                propertyChanged: OnStatusBarStyle_PropertyChanged);

        private static void OnStatusBarStyle_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MainNavigationPage view = bindable as MainNavigationPage;
            if (newValue is BarStyle)
            {
                view.ChangeStatusBarStyle();
            }
        }

        public BarStyle StatusBarStyle
        {
            get { return (BarStyle)GetValue(StatusBarStyleProperty); }
            set { SetValue(StatusBarStyleProperty, value); }
        }

        public MainNavigationPage()
        {
            InitializeComponent();
            ChangeStatusBarStyle();
        }

        void ChangeStatusBarStyle()
        {
            DependencyService.Get<IChangeBarColor>().ChangeColor(StatusBarStyle);
        }
    }

    public enum IconType
    {
        FontAwesome5Solid,
        FontAwesome5Regular,
        FontAwesome5Brand,
        SVG

    }
}