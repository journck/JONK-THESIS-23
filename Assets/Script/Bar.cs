using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public enum BarType
    {
        exp,
        health
    }

    private Image img;
    static readonly Color FULL_HEALTH = Color.green;
    static readonly Color MEDIUM_HEALTH = Color.yellow;
    static readonly Color LOW_HEALTH = Color.red;

    [Header("Inscribed")]
    public BarType barType;


    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
        img.fillAmount = 0f;
    }

    public void UpdateImg( float val )
    {
        switch (barType)
        {
            case BarType.health:
                if (val > 0.5f)
                {
                    img.color = FULL_HEALTH;
                }
                else if (val > 0.1f)
                {
                    img.color = MEDIUM_HEALTH;
                }
                else
                {
                    img.color = LOW_HEALTH;
                }
                break;
            default:
                break;
        }
        img.fillAmount = val;
    }
}
