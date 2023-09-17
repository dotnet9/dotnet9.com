namespace Dotnet9.Commons;

public static class RandomExtensions
{
    /// <summary>
    ///     Returns a random integer that is within a specified range.
    /// </summary>
    /// <param name="random"></param>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">
    ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
    ///     to minValue.
    /// </param>
    /// <returns></returns>
    public static double NextDouble(this Random random, double minValue, double maxValue)
    {
        if (minValue >= maxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(minValue), "minValue cannot be bigger than maxValue");
        }

        //https://stackoverflow.com/questions/65900931/c-sharp-random-number-between-double-minvalue-and-double-maxvalue
        var x = random.NextDouble();
        return x * maxValue + (1 - x) * minValue;
    }

    public static IList<T> RandomItems<T>(this IList<T> source, int count)
    {
        if (source.Count <= count)
        {
            return source;
        }

        IList<T> dataOfGetList = new List<T>(source);
        IList<T> dataOfTarget = new List<T>();


        for (int i = 0; i < count; i++)
        {
            if (dataOfGetList.Count > 0)
            {
                int arrIndex = Random.Shared.Next(0, dataOfGetList.Count);
                dataOfTarget.Add(dataOfGetList[arrIndex]);
                dataOfGetList.RemoveAt(arrIndex);
            }
            else
            {
                break;
            }
        }

        return dataOfTarget;
    }
}