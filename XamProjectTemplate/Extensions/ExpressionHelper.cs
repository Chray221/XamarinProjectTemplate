using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace XamProjectTemplate.Extensions
{
	public static class ExpressionHelper
	{
		private static readonly DataTable dt = new DataTable();
		private static readonly Dictionary<string, string> expressionCache = new Dictionary<string, string>();
		private static readonly Dictionary<string, object> resultCache = new Dictionary<string, object>();

		// to be amended with necessary transforms
		private static readonly (string old, string @new)[] tokens = new[] { ("&&", "AND"), ("||", "OR") };

		public static T Compute<T>(this string expression, params (string name, object value)[] arguments) =>
			(T)Convert.ChangeType(expression.Transform().GetResult(arguments), typeof(T));

		private static object GetResult(this string expression, params (string name, object value)[] arguments)
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

		private static string Transform(this string expression)
		{
			if (expressionCache.TryGetValue(expression, out var result))
				return result;

			App.Log("(Transform)ExpressionHelper: {v}");
			result = expression;
			foreach (var t in tokens)
				result = result.Replace(t.old, t.@new);

			return expressionCache[expression] = result;
		}

		public static string ToDataTableStringValue(this string expression)
		{
			if (!string.IsNullOrEmpty(expression))
			{
				string matching = "";
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