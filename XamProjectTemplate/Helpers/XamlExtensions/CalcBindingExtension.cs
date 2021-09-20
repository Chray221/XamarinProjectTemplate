using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamProjectTemplate
{
    public class CalcBindingExtension : IMarkupExtension<BindingBase>
    {

        public string Path { get; set; }
        public object Source { get; set; } = null;
        public string Calculation { get; set; } = null;
        public CalcBindingExtension()
        {
        }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            //Type property = typeof(double);
            //IProvideValueTarget provideValueTarget = serviceProvider.GetService<IProvideValueTarget>();
            //if (provideValueTarget != null)
            //{
            //    if (provideValueTarget.TargetObject is Setter setter)
            //        property = setter.Property.ReturnType;
            //    else if (provideValueTarget.TargetProperty is BindableProperty bindableProperty)
            //        property = bindableProperty.ReturnType;
            //}

            if (Source == null)
                return new Binding
                {
                    Path = Path,
                    Mode = BindingMode.OneWay,
                    Converter = new CalcBindingConverter(),
                    ConverterParameter = Calculation
                };
            return new Binding
            {
                Path = Path,
                Source = Source,
                Mode = BindingMode.OneWay,
                Converter = new CalcBindingConverter(),
                ConverterParameter = Calculation
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public class CalcBindingConverter : IValueConverter
        {
            public string Calculation { get; set; } = "{0}";
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                App.Log($"BINDING CALC TARGET TYPE: {targetType}");
                if (parameter is string stringValue)
                {
                    //if (value is DateTime dateTimeValue)
                    //    return stringValue.Compute<bool>(("{0}", string.Format(boolValues.StringFormat, dateTimeValue))) ? boolValues.True : boolValues.False;
                    //System.Convert.ChangeType(value, value.GetType(),IFormatProvider)
                    return stringValue.Compute(targetType,("{0}", value)) ;
                }

                return Activator.CreateInstance(targetType);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class CalcMultiBindingConverter : IMultiValueConverter
    {
        public string Calculation { get; set; } = "{0}";

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            App.Log($"BINDING CALC TARGET TYPE: {targetType}");
            List<(string, object)> parameters = new List<(string, object)>();
            for (int index = 0; index < values.Length; index++)
            {
                parameters.Add(("{" + index + "}", values[index]));
            }
            return Calculation.Compute(targetType, parameters.ToArray());

            //return Activator.CreateInstance(targetType);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
