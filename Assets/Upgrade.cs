using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{

    [Header("Inscribed")]
    public string title;
    public string description;
    public UnityEvent upgradeEvent;
    public bool badUpgrade;
    //public string attribute;


    [Header("Dynamic")]
    public UpgradeScreen parentScreen;
    public Outline outline;
    public bool _isSelected;
    public Player player
    {
        get { return parentScreen.parentField.player; }
    }

    public bool IsSelected
    {
        get { return _isSelected; }
        set {
            _isSelected = value;
            outline.effectColor = badUpgrade ? Color.red : Color.black;
            outline.enabled = value;
            //Debug.Log(value);
        }
    }
    public Image img;

    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
        outline = GetComponent<Outline>();
        if (upgradeEvent == null)
        {
            upgradeEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreasePlayerStat(string attribute)
    {
        player.upgrades[attribute]++;
    }


    // NOTE- scalar should be > 1 in order to increase.
    public void DoubleSpawn(float scalar)
    {
        player.parentField.enemyManager.b *= scalar;
    }

    public void DamagePlayer(int percent)
    {
        player.TakeDamage(player.maxHealth * (percent / 100));
    }

    public void ShrinkCamera(int amount)
    {
        //TODO cmon cam.cam......
        player.parentField.cam.cam.orthographicSize = Mathf.Max(player.parentField.cam.cam.orthographicSize - amount, 1);
    }
}
