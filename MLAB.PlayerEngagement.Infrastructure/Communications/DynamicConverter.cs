
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace MLAB.PlayerEngagement.Infrastructure.Communications
{
    public static class DynamicConverter
    {
        public static List<Dictionary<string, object>> ConvertToDictionaries(IEnumerable<dynamic> dynamicData)
        {
            var dictionaries = new List<Dictionary<string, object>>();

            foreach (var item in dynamicData)
            {
                var dictionary = new Dictionary<string, object>();

                var expando = (IDictionary<string, object>)item;

                foreach (var kvp in expando)
                {
                    dictionary.Add(kvp.Key, kvp.Value);
                }

                dictionaries.Add(dictionary);
            }

            return dictionaries;
        }

        public static List<T> ConvertToModels<T>(List<Dictionary<string, object>> dictionaries)
        {
            var models = new List<T>();

            foreach (var data in dictionaries)
            {
                var model = Activator.CreateInstance<T>();

                foreach (var property in typeof(T).GetProperties())
                {
                    if (data.TryGetValue(property.Name, out var value))
                    {
                        if (TryConvertValue(property.PropertyType, value, out var convertedValue))
                        {
                            property.SetValue(model, convertedValue);
                        }
                        else
                        {
                            property.SetValue(model, value);
                        }
                    }
                }

                models.Add(model);
            }

            return models;
        }
        private static bool TryConvertValue(Type propertyType, object value, out object convertedValue)
        {
            convertedValue = null;

            if (propertyType == typeof(int))
            {
                return TryConvertToInt(value, out convertedValue) || TryConvertFromOtherTypes(value, out convertedValue);
            }
            else if (propertyType == typeof(string))
            {
                return TryConvertToString(value, out convertedValue) || TryConvertFromOtherTypes(value, out convertedValue);
            }
            else if (propertyType == typeof(double))
            {
                return TryConvertToDouble(value, out convertedValue) || TryConvertFromOtherTypes(value, out convertedValue);
            }
            else if (propertyType == typeof(decimal))
            {
                return TryConvertToDecimal(value, out convertedValue) || TryConvertFromOtherTypes(value, out convertedValue);
            }
            else if (propertyType == typeof(double?))
            {
                return TryConvertToNullableDouble(value, out convertedValue) || TryConvertFromOtherTypes(value, out convertedValue);
            }

            return false;
        }

        private static bool TryConvertToInt(object value, out object convertedValue)
        {
            convertedValue = null;

            if (value is long longValue)
            {
                convertedValue = (int)longValue;
                return true;
            }
            else if (value is string stringValue && int.TryParse(stringValue, out int intValue))
            {
                convertedValue = intValue;
                return true;
            }
            else if (value is decimal decVal)
            {
                convertedValue = (int)decVal;
                return true;
            }

            return false;
        }

        private static bool TryConvertToString(object value, out object convertedValue)
        {
            convertedValue = null;

            if (value is DateTime dateValue)
            {
                convertedValue = dateValue.ToString("yyyy-MM-dd HH:mm:ss.fff");
                return true;
            }
            else if (value is long longVal)
            {
                convertedValue = longVal.ToString();
                return true;
            }
            else if (value is int intVal)
            {
                convertedValue = intVal.ToString();
                return true;
            }

            return false;
        }

        private static bool TryConvertToDouble(object value, out object convertedValue)
        {
            convertedValue = null;

            if (value is double doubleVal)
            {
                convertedValue = (decimal)doubleVal;
                return true;
            }
            else if (value is decimal decimalVal)
            {
                convertedValue = (double)decimalVal;
                return true;
            }

            return false;
        }

        private static bool TryConvertToDecimal(object value, out object convertedValue)
        {
            convertedValue = null;

            if (value is double doubleVal)
            {
                convertedValue = (decimal)doubleVal;
                return true;
            }

            return false;
        }

        private static bool TryConvertToNullableDouble(object value, out object convertedValue)
        {
            convertedValue = null;

            if (value is decimal decimalVal)
            {
                convertedValue = (double?)decimalVal;
                return true;
            }

            return false;
        }

        private static bool TryConvertFromOtherTypes(object value, out object convertedValue)
        {
            // Handle conversions from other types for each supported property type.
            // Add cases as needed.
            convertedValue = null;
            return false;
        }

    }


}
