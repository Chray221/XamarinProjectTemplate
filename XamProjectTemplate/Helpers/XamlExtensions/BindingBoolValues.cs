using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamProjectTemplate
{
    public class BindingBoolValues : IMarkupExtension<BindingBase>
    {
        public string Path { get; set; }
        public string StringFormat { get; set; } = null;
        public string Condition { get; set; } = null;
        public object True { get; set; }
        public object False { get; set; }
        public BindingBoolValues()
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
            return new Binding
            {
                Path = Path,
                StringFormat = StringFormat,
                Mode = BindingMode.OneWay,
                Converter = new BindingBoolValuesConverter(),
                ConverterParameter = new BoolValues()
                {
                    True = True,
                    False = False,
                    Condition = Condition,
                    StringFormat = StringFormat
                }
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }



        public class BindingBoolValuesConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                App.Log($"BINDING BOOL TARGET TYPE: {targetType.ToString()}");
                if (parameter is BoolValues boolValues)
                {
                    if (value is bool boolValue)
                    {
                        return boolValue ? boolValues.True : boolValues.False;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(boolValues.Condition))
                        {
                            if (value is DateTime dateTimeValue)
                                return boolValues.Condition.Compute<bool>(("{0}", string.Format(boolValues.StringFormat, dateTimeValue))) ? boolValues.True : boolValues.False;
                            //System.Convert.ChangeType(value, value.GetType(),IFormatProvider)
                            return boolValues.Condition.Compute<bool>(("{0}", string.Format(boolValues.StringFormat ?? "{0}", value.ToString()))) ? boolValues.True : boolValues.False;
                        }
                    }
                }

                return Activator.CreateInstance(targetType);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class BoolValues
        {
            public object True { get; set; }
            public object False { get; set; }
            public string Condition { get; set; }
            public string StringFormat { get; set; }
        }



        public class BoolValues<T>
        {
            public BoolValues(T type)
            {

            }
            public T True { get; set; }
            public T False { get; set; }
        }
    }


    public static class ExpressionHelper
    {
        private static readonly DataTable dt = new DataTable();
        private static readonly Dictionary<string, string> expressionCache = new Dictionary<string, string>();
        private static readonly Dictionary<string, object> resultCache = new Dictionary<string, object>();

        // to be amended with necessary transforms
        private static readonly (string old, string @new)[] tokens = new[] { ("&&", "AND"), ("||", "OR") };

        public static T Compute<T>(this string expression, params (string name, object value)[] arguments) =>
            (T)Convert.ChangeType(expression.Transform().GetResult(arguments), typeof(T));
        public static object Compute(this string expression, Type type, params (string name, object value)[] arguments) =>
            Convert.ChangeType(expression.Transform().GetResult(arguments), type);

        private static object GetResult(this string expression, params (string name, object value)[] arguments)
        {
            try
            {
                expression = expression.ToDataTableStringValue();
                foreach (var arg in arguments)
                {
                    if (arg.value is string)
                        expression = expression.Replace(arg.name, "'" + arg.value + "'");
                    else
                        expression = expression.Replace(arg.name, arg.value.ToString());
                }

                expression = expression.Replace("==", "=");
                App.Log($"(Compute)ExpressionHelper: {expression}");
                if (resultCache.TryGetValue(expression, out var result))
                    return result;

                App.Log($"(Compute)ExpressionHelper: {expression}");
                return resultCache[expression] = dt.Compute(expression, string.Empty);
            }
            catch(Exception ex)
            {
                App.LogException(ex);
            }
            return "Task Cannot Performed";
        }

        private static string Transform(this string expression)
        {
            if (expressionCache.TryGetValue(expression, out var result))
                return result;

            App.Log($"(Transform)ExpressionHelper: {expression}");
            result = expression;
            foreach (var (old, @new) in tokens)
                result = result.Replace(old, @new);

            return expressionCache[expression] = result;
        }

        public static string ToDataTableStringValue(this string expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                string matching = expression;
                string patern = "[a-zA-Z]+";

                MatchCollection matches = Regex.Matches(expression, patern);
                foreach (var match in matches)
                {
                    matching = expression.Replace(match.ToString(), string.Format("'{0}'", match.ToString()));
                    App.Log($"ExpressionHelper string matches: {matching}");
                }
                return matching;
            }
            return expression;
        }
    }
}


