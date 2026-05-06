namespace FlashBeat.Common.Extensions;

using System.ComponentModel;

/// <summary>
/// Extensions methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Method to get the first value of an enum.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <returns>The first value of the enum <typeparamref name="TEnum"/>.</returns>
    public static TEnum First<TEnum>()
        where TEnum : struct, Enum
        => Enum.GetValues<TEnum>()[0];

    /// <summary>
    /// Method to get the last value of an enum.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <returns>The last value of the enum <typeparamref name="TEnum"/>.</returns>
    public static TEnum Last<TEnum>()
        where TEnum : struct, Enum
        => Enum.GetValues<TEnum>()[^1];

    /// <summary>
    /// Method to get the previous value of the <typeparamref name="TEnum"/> enum starting from
    /// <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="source">The enum value.</param>
    /// <returns>The previous enum value. The last value if the current is the first value.</returns>
    public static TEnum Previous<TEnum>(this TEnum source)
        where TEnum : struct, Enum
    {
        var enumValues = Enum.GetValues<TEnum>();
        var nextIndex = Array.IndexOf(enumValues, source) - 1;
        return nextIndex == -1 ? enumValues[^1] : enumValues[nextIndex];
    }

    /// <summary>
    /// Method to get the next value of the <typeparamref name="TEnum"/> enum starting from <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="source">The enum value.</param>
    /// <returns>The next enum value. The first value if the current is the last value.</returns>
    public static TEnum Next<TEnum>(this TEnum source)
        where TEnum : struct, Enum
    {
        var enumValues = Enum.GetValues<TEnum>();
        var nextIndex = Array.IndexOf(enumValues, source) + 1;
        return nextIndex == enumValues.Length ? enumValues[0] : enumValues[nextIndex];
    }

    /// <summary>
    /// Method to get the value of the <see cref="DescriptionAttribute"/> of an enum value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="source">The enum value.</param>
    /// <returns>The description of the source enum value.</returns>
    public static string GetEnumDescription<TEnum>(this TEnum? source)
        where TEnum : struct, Enum
    {
        if (source is null)
            return string.Empty;

        var enumType = typeof(TEnum);
        var memberInfo = enumType.GetMember(source.ToString()!).FirstOrDefault();
        var descriptionAttribute = memberInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() as DescriptionAttribute;
        return descriptionAttribute?.Description ?? source.ToString()!;
    }
}
