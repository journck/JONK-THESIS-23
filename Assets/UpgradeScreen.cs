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
    public TMP_Text levelTextRef;
    public TMP_Text titleTextRef;
    public TMP_Text descTextRef;

    //represents all of the possible upgrades that could be chosen;
    public List<Upgrade> upgradeOptionPrefabs;

    //represents the current upgrades in the screen
    public Upgrade[] upgrades;

    [Header("Dynamic")]


    // I feel like there's gotta be a better way to do this
    public int _selectedUpgradeIndex;

    public int SelectedUpgradeIndex
    {
        get { return _selectedUpgradeIndex; }
        set
        {
            _selectedUpgradeIndex = value;
            SelectedUpgrade = upgrades[value];
        }
    }
    public Upgrade _selectedUpgrade;
    public Upgrade SelectedUpgrade
    {
        get { return _selectedUpgrade; }
        set {
            _selectedUpgrade.IsSelected = false;
            _selectedUpgrade = value;
            _selectedUpgrade.IsSelected = true;
            UpdateText(_selectedUpgrade);
        }
    }


    const int SPACING = 150;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Assert(upgrades.Length <= upgradeOptions.Count);
        _selectedUpgradeIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Q) )
        {
            IncrementUpgradeIndex(false);
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            IncrementUpgradeIndex(true);

        }
    }

    void UpdateText(Upgrade selected)
    {
        titleTextRef.text = selected.title;
        descTextRef.text = selected.description;
    }


    void GenerateUpgrades()
    {
        Debug.Log("generating upgrades");
        int upgradeCount = upgrades.Length;
        foreach ( Upgrade upgrade in upgrades)
        {
            Destroy(upgrade.gameObject);
            Debug.Log(upgrades.Length);
        }


        List<Upgrade> workingList = upgradeOptionPrefabs;
        for ( int i = 0; i < upgradeCount; i++ )
        {
            Debug.Log("in for loop");
            int randomIndex = Random.Range(0, workingList.Count);
            Upgrade instantiatedUpgrade = Instantiate(workingList[randomIndex], this.transform);
            Vector3 drawLocation = new Vector3(SPACING * (i - 1), 0, 0);
            instantiatedUpgrade.transform.position = drawLocation;
            upgrades[i] = instantiatedUpgrade;
            Debug.Log("instantiating upgrade");

            //making sure same upgrade can't be displayed twice
            workingList.RemoveAt(randomIndex);
        }
        //Debug.Log("should show upgrades here");
    }

    private void OnEnable()
    {
        levelTextRef.text = "Level Up! You are now level " + parentField.player.level;
        GenerateUpgrades();

        Invoke(nameof(ReturnToGame), 10);
    }

    private void ReturnToGame()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Utility.SetActiveGOAndChildren(this.parentField.gameObject, true);
    }


    // true if we want to increment to the next upgrade
    // false if we want to go to an earlier one. this wraps around!
    public void IncrementUpgradeIndex ( bool increase )
    {
        int intermediate;
        int cachedPos = SelectedUpgradeIndex;
        intermediate = increase ? SelectedUpgradeIndex + 1 : SelectedUpgradeIndex - 1;

        // wrap around
        if ( intermediate < 0 )
        {
            intermediate = upgrades.Length - 1;
        }
        else if ( intermediate >= upgrades.Length )
        {
            intermediate = 0;
        }
        Debug.Log("old was " + cachedPos + " new is " + intermediate);
        SelectedUpgradeIndex = intermediate;
    }
}
