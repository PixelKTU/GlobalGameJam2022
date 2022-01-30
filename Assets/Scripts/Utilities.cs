using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utilities
{
    public static T PickRandom<T>(this IEnumerable<T> collection)
    {
        var array = collection.ToArray();
        return array[Random.Range(0, array.Length)];
    }
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    public static bool TryGetComponentInParent<T>(this Component parentComponent, out T component)
    {
        component = parentComponent.GetComponentInParent<T>();
        return component != null;
    }
    public static bool TryGetComponentInChildren<T>(this Component parentComponent, out T component)
    {
        component = parentComponent.GetComponentInChildren<T>();
        return component != null;
    }
    public static bool IsWithinBounds(this Vector2 range, float value)
    {
        return value >= range.x && value <= range.y;
    }
    public static float RandomFromRange(this Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }
}