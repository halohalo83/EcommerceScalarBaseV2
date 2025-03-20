using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Domain.Extensions;

public static class EnumExtensions
{
    public static TEnum ParseDictionaryToFlag<TEnum>(Dictionary<string, bool>? action, TEnum noneFlag)
        where TEnum : struct
    {
        return action!.Aggregate(noneFlag, (acc, curr) =>
        {
            if (curr.Value && Enum.TryParse(curr.Key, out TEnum funcAction))
            {
                return (TEnum)(object)((int)(object)acc | (int)(object)funcAction);
            }

            return acc;
        });
    }

    public static string GetDescription(this Enum enumValue)
    {
        object[] attr = enumValue.GetType().GetField(enumValue.ToString())!
            .GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attr.Length > 0)
        {
            return ((DescriptionAttribute)attr[0]).Description;
        }

        string result = enumValue.ToString();
        result = Regex.Replace(result, "([a-z])([A-Z])", "$1 $2");
        result = Regex.Replace(result, "([A-Za-z])([0-9])", "$1 $2");
        result = Regex.Replace(result, "([0-9])([A-Za-z])", "$1 $2");
        result = Regex.Replace(result, "(?<!^)(?<! )([A-Z][a-z])", " $1");
        return result;
    }

    public static string GetEnumDescription(this Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo == null)
        {
            return "Enum data does not exist";
        }

        var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
    }

    public static string GetTextEnumDescription(Enum value)
    {
        return GetTextEnumDescription(value.GetType(), value.ToString());
    }

    public static string GetTextEnumDescription(Type type, string value)
    {
        var fi = type.GetField(value);

        if (fi == null)
        {
            return string.Empty;
        }

        if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                is DescriptionAttribute[] attributes && attributes.Any())
        {
            return attributes.First().Description;
        }

        return value;
    }

    public static TEnum? GetEnumByKeyword<TEnum>(string keyword) where TEnum : struct, Enum
    {
        if (string.IsNullOrEmpty(keyword))
        {
            return null;
        }

        var enums = Enum.GetValues<TEnum>().ToDictionary(x => x, x => x.GetEnumDescription().ToLower());
        var result = enums.FirstOrDefault(x => x.Value.Contains(keyword.ToLower())).Key;
        if ((int)(object)result == 0)
        {
            return null;
        }

        return result;
    }
}