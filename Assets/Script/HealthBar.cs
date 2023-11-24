using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image img;

    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
    }

    public void UpdateImg( float val )
    {
        img.fillAmount = val;
    }
}
