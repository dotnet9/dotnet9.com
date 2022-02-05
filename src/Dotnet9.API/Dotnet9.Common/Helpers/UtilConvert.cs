namespace Dotnet9.Common.Helpers;

public static class UtilConvert
{
    public static int ObjToInt(this object thisValue)
    {
        var reval = 0;
        if (thisValue == null) return 0;
        if (thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return reval;
    }

    public static int ObjToInt(this object thisValue, int errorValue)
    {
        var reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return errorValue;
    }

    public static double ObjToMoney(this object thisValue)
    {
        double reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return 0;
    }

    public static double ObjToMoney(this object thisValue, double errorValue)
    {
        double reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return errorValue;
    }

    public static string ObjToString(this object thisValue)
    {
        if (thisValue != null) return thisValue.ToString().Trim();
        return "";
    }

    public static bool IsNotEmptyOrNull(this object thisValue)
    {
        return ObjToString(thisValue) != "" && ObjToString(thisValue) != "undefined" && ObjToString(thisValue) != "null";
    }

    public static string ObjToString(this object thisValue, string errorValue)
    {
        if (thisValue != null) return thisValue.ToString().Trim();
        return errorValue;
    }

    public static Decimal ObjToDecimal(this object thisValue)
    {
        Decimal reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return 0;
    }

    public static Decimal ObjToDecimal(this object thisValue, decimal errorValue)
    {
        Decimal reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return errorValue;
    }

    public static DateTime ObjToDate(this object thisValue)
    {
        DateTime reval = DateTime.MinValue;
        if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
        {
            reval = Convert.ToDateTime(thisValue);
        }
        return reval;
    }

    public static DateTime ObjToDate(this object thisValue, DateTime errorValue)
    {
        DateTime reval = DateTime.MinValue;
        if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return errorValue;
    }

    public static bool ObjToBool(this object thisValue)
    {
        bool reval = false;
        if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return reval;
    }

    public static string DateToTimeStamp(this DateTime thisValue)
    {
        TimeSpan ts = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }
}