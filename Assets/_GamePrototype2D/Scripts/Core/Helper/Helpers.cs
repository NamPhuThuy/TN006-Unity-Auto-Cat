using UnityEngine;

public static class Helpers
{
    public static float Map(float value, float originMin, float originMax, float newMin, float newMax, bool clamp)
    {
        float newValue = (value - originMin) / (originMax - originMin) * (newMax - newMin) + newMin;

        if (clamp)
        {
            newValue = Mathf.Clamp(newValue, newMin, newMax);
        }

        return newValue;
    }
    
    public static float ReverseMap(float value, float originMin, float originMax, float newMin, float newMax, bool clamp)
    {
        float newValue = (1f - ((value - originMin) / (originMax - originMin))) * (newMax - newMin) + newMin;

        if (clamp)
        {
            newValue = Mathf.Clamp(newValue, newMin, newMax);
        }

        return newValue;
    }
}
