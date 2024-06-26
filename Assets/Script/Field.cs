using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public enum FieldSpace
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    [Header("Inscribed")]
    public Player[] players;
    public FollowCam cam;
    public Bar healthBar;
    public Bar expBar;
    public UpgradeScreen upgradeScreen;
    public FieldSpace fieldSpace;
    public SoundController soundController;


    [Header("Dynamic")]
    public List<Enemy> enemyList;
    public Player player;
    public EnemyManager enemyManager;
    public bool fieldPaused;


    void Awake()
    {
        Player playerPrefab = players[Random.Range(0, players.Length)];
        player = Instantiate(playerPrefab, this.transform);
        player.transform.rotation = Quaternion.Euler(Random.Range(0, 4) * 90, 90, 270);
        player.soundController = this.soundController;
        enemyManager = GetComponentInChildren<EnemyManager>();
        cam.toFollow = player.gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        fieldPaused = false;
    }

    private void OnDisable()
    {
        fieldPaused = true;
    }
}
