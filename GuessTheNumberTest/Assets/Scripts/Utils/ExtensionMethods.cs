using System.Collections.Generic;

public static class ExtensionMethods
{
    public static void Shuffle<T>(this List<T> list)
    {
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = list.GetValueOrDefault(i);
            list[i] = list.GetValueOrDefault(r);
            list[r] = tmp;
        }
    }

    public static T GetValueOrDefault<T>(this T[] array, int i)
    {
        if (array == null || i < 0 || i >= array.Length) return default;
        return array[i];
    }

    public static T GetValueOrDefault<T>(this T[] array, int i, T def)
    {
        if (array == null || i < 0 || i >= array.Length) return def;
        return array[i];
    }

    public static T GetValueOrDefault<T>(this T[,] array, int x, int y)
    {
        if (array == null || x < 0 || x >= array.GetLength(0) || y < 0 || y >= array.GetLength(1)) return default;
        return array[x,y];
    }

    public static T GetValueOrDefault<T>(this T[,] array, int x, int y, T def)
    {
        if (array == null || x < 0 || x >= array.GetLength(0) || y < 0 || y >= array.GetLength(1)) return def;
        return array[x, y];
    }

    public static T GetValueOrDefault<T>(this List<T> list, int i, T def)
    {
        if (list == null || i < 0 || i >= list.Count) return def;
        return list[i];
    }

    public static T GetValueOrDefault<T>(this List<T> list, int i)
    {
        if (list == null || i < 0 || i >= list.Count) return default;
        return list[i];
    }

    public static T GetValueOrDefault<T>(this Dictionary<string, T> dictionary, string key)
    {
        T retStr = default;
        if (dictionary != null && dictionary.TryGetValue(key, out retStr))
        {
            return retStr;
        }
        return retStr;
    }
}
