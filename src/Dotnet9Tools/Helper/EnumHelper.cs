using System.ComponentModel;
using System.Reflection;

namespace Dotnet9Tools.Helper;

public static class EnumHelper
{
    public static string? GetDescription(this Enum? obj)
    {
        if (obj == null)
        {
            return "";
        }

        Type type = obj.GetType();
        //获取到:Admin
        string? enumName = Enum.GetName(type, obj);
        if (enumName != null)
        {
            FieldInfo? field = type.GetField(enumName);
            //获取到:0
            int val = (int)(field?.GetValue(null) ?? throw new InvalidOperationException());
            object[] atts = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (atts.Length <= 0)
            {
                return "-";
            }

            //获取到：超级管理员
            DescriptionAttribute att = ((DescriptionAttribute[])atts)[0];
            string des = att.Description;
            return des;
        }

        return null;
    }
}