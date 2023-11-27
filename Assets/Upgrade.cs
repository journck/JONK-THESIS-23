using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{

    [Header("Inscribed")]
    public string title;
    public string description;


    [Header("Dynamic")]
    public Outline outline;
    public bool _isSelected;

    public bool IsSelected
    {
        get { return _isSelected; }
        set {
            _isSelected = value;
            outline.enabled = value;
            Debug.Log(value);
        }
    }
    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
