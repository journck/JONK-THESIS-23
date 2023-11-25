using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeScreen : MonoBehaviour
{
    //starts off. the parentField will activate this when time.

    [Header("Inscribed")]
    public Field parentField;
    public TMP_Text textRef;

    //public Upgrade[] upgrades;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateUpgrades()
    {
        Debug.Log("should show upgrades here");
    }

    private void OnEnable()
    {
        textRef.text = "Level Up! You are now level " + parentField.player.level;
        GenerateUpgrades();
        Invoke(nameof(ReturnToGame), 4);
    }

    private void ReturnToGame()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Utility.SetActiveGOAndChildren(this.parentField.gameObject, true);
    }
}
