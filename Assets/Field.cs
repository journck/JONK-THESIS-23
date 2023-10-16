using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [Header("Inscribed")]
    public Player[] players;


    [Header("Dynamic")]
    public List<Enemy> enemyList;
    public Player player;
    public EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        Player playerPrefab = players[Random.Range(0, players.Length)];
        player = Instantiate(playerPrefab, this.transform);
        player.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 4) * 90);
        enemyManager = GetComponentInChildren<EnemyManager>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
