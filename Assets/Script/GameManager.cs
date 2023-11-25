using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //TODO - iterate over array of players.
    //public Player[] players = new Player[4];


    [Header("Inscribed")]
    public Field[] fields;
    const float diffConst = 0.23f;
    const float diffExp = 1.0025f;

    [Header("Dynamic")]
    public float difficulty;
    public static GameManager instance;
    public Player[] players;
    public bool gamePaused = false;
    public bool fieldsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        players = new Player[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            players[i] = fields[i].player;
        }
    }

    public void CheckForRestart()
    {
        foreach (Player p in players)
        {
            if (!p.IsDead())
            {
                return;
            }
        }
        RestartGame();
    }   

    // called when all players die
    void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    void Update()
    {
        difficulty = Mathf.Pow( diffConst * Time.timeSinceLevelLoad, diffExp);
        if ( Input.GetKeyDown(KeyCode.Escape ) )
        {
            gamePaused = !gamePaused;
            Time.timeScale = gamePaused ? 1 : 0;
        }

        if ( Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Field f in fields)
            {
                Utility.SetActiveGOAndChildren(f.gameObject, fieldsPaused);
            }
            fieldsPaused = !fieldsPaused;
        }
    }
}
