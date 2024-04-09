using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Inscribed")]
    public Field[] fields;
    const float diffConst = 0.5f;
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

        //for ( uint i = 0; i < 10; i++)
        //{
        //    Debug.Log(Utility.ExpForLevel(i));
        //}
    }

    public void CheckForRestart()
    {
        bool anyAlive = false;
        foreach (Player p in players)
        {
            if (p.IsDead())
            {
                Utility.SetActiveGOAndChildren(p.parentField.gameObject, false);
            }
            else
            {
                anyAlive = true;
            }
        }
        if (anyAlive == false)
        {
            RestartGame();
        }
    }   

    // called when all players die
    void RestartGame()
    {
        Debug.Log("The size of the bullet pool @ the end of the game is " + BulletPooling.instance.bulletPool.Count);
        SceneManager.LoadScene("Title");
    }

    // Update is called once per frame
    void Update()
    {
        difficulty = Mathf.Pow(diffConst * Time.timeSinceLevelLoad, diffExp);
        //Debug.Log(difficulty);
        if ( Input.GetKeyDown(KeyCode.Escape ) )
        {
            gamePaused = !gamePaused;
            Time.timeScale = gamePaused ? 1 : 0;
        }

        if ( Input.GetKeyDown(KeyCode.Space))
        {
            //foreach (Field f in fields)
            //{
            //    Utility.SetActiveGOAndChildren(f.gameObject, fieldsPaused);
            //}

            //Utility.SetActiveGOAndChildren(fields[1].gameObject, fieldsPaused);

            //fieldsPaused = !fieldsPaused;

            //foreach ( Field f in fields)
            //{
            //    f.player.GainXP(1000);
            //}
        }
    }

    
}
