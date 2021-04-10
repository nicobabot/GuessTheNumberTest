using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ExtensionMethods
{
    public static void Shuffle<T>(this List<T> list)
    {
        int count = list.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i)
        {
            int r = Random.Range(i, count);
            T tmp = list.GetValueOrDefault(i);
            list[i] = list.GetValueOrDefault(r);
            list[r] = tmp;
        }
    }

    public static IEnumerator DoAlphaTransition(this TextMeshProUGUI textMesh,float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = textMesh.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            textMesh.alpha = newAlpha;
            yield return null;
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
