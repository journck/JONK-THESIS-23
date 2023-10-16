using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Character : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int moveSpeed;

    public virtual void takeDamage ( float iDamage )
    {
        float damage = iDamage;
        // DO ANY DAMAGE MODIFIERS ETC HERE.

        // PLAYER DIED
        if ( health - damage <= 0 )
        {
            Debug.Log("DEAD!");
            health = 0;
            doDeath();
        }
        else
        {
            health -= damage;
        }
    }

    public void doDeath()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
