using System.ComponentModel;

namespace MyAccounts.Api.Modules.Shared.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString())!;
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes == null || attributes.Length == 0)
                return value.ToString();
            
            return ((DescriptionAttribute)attributes[0]).Description;
        }

        public static int GetCode(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static string GetName(this Enum value)
        {
            return value.ToString();
        }
    }
}
