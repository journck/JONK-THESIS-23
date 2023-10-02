using UnityEngine;
using UnityEngine.Assertions;

public static class Utility
{

    public static Color HealthColor ( float currentHealth, float maxHealth )
    {
        Assert.IsTrue( maxHealth > 0 && currentHealth <= maxHealth, "incorrect currentHealth & maxHealth");
        float frac = currentHealth / maxHealth;
        bool aboveHalf = frac > 0.5f;
        Color maxColor = aboveHalf ? Color.green : Color.yellow;
        Color minColor = aboveHalf ? Color.yellow : Color.red;

        float r = lerp3(minColor.r, maxColor.r, frac);
        float g = lerp3(minColor.g, maxColor.g, frac);
        float b = lerp3(minColor.b, maxColor.b, frac);
        return Color.black;
    }

    public static float lerp3 ( float y0, float y1, float alpha)
    {
        return (1 - alpha) * y0 + alpha * y1;
    }
}
