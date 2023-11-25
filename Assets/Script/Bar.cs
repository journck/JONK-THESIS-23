using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Image img;

    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
        img.fillAmount = 0f;
    }

    public void UpdateImg( float val )
    {
        img.fillAmount = val;
    }
}
