using UnityEngine;
using UnityEngine.Assertions;

public static class Utility
{
    // important that this is in descending order
    private static readonly uint[] PossibleDenominations =
    {
        500,
        250,
        100,
        50,
        25,
        10,
        5,
        1
    };

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

    // returns the amount of exp needed to reach a level
    // ripped from halls of torment : https://hot.fandom.com/wiki/Game_Mechanics
    public static float ExpForLevel(uint currentLevel)
    {
        return (100 * Mathf.Pow(1.01f, currentLevel) - 7 * Mathf.Pow(0.97f, currentLevel) - 1) * currentLevel;
    }

    //// returns a dictionary with 
    //public static Vector2[] CreateDenominations ( uint num )
    //{
    //    uint workingNum = num;
    //    while ( workingNum > 0 )
    //    {
    //        uint fitsInto = Mathf.FloorToInt(workingNum)
    //    }
    //}

    public static void SetActiveGOAndChildren(GameObject go, bool boolean)
    {
        if (go == null)
            return;

        

        for ( int i = 0; i < go.transform.childCount; i++)
        {
            GameObject childGO = go.transform.GetChild(i).gameObject;
            Utility.SetActiveGOAndChildren(childGO, boolean);
            //Debug.Log("SetActiveGOAndChildren Child #" + i);
        }

        go.SetActive(boolean);

        //Debug.Log("Done setting this gameObject and its children to " + boolean);
    }
}
