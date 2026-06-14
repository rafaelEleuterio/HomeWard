using System.ComponentModel;
using System.Reflection;

namespace HomeWorkTimeDesktopApp.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        // Get the type info of the enum
        Type type = value.GetType();

        // Get the specific field name
        string name = Enum.GetName(type, value);
        if (name == null) return value.ToString();

        // Get the field details from the type
        FieldInfo field = type.GetField(name);
        if (field == null) return value.ToString();

        // Search the field for the DescriptionAttribute
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

        // Return the description if found, otherwise fall back to the name string
        return attribute != null ? attribute.Description : value.ToString();
    }
}
