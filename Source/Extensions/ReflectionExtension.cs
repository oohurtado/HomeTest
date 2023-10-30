using Home.Source.Common;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Home.Source.Extensions
{
    public static class ReflectionExtension
    {
        public static string GetName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name ?? enumValue.ToString();
        }

        public static string GetBasicName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<BasicAttribute>()?
                            .Name ?? enumValue.ToString();
        }

        public static string GetBasicDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<BasicAttribute>()?
                            .Description ?? enumValue.ToString();
        }

        public static string GetBasicCode(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<BasicAttribute>()?
                            .Code ?? enumValue.ToString();
        }
    }
}
