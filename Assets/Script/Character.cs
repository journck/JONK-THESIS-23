using System;
using UnityEngine;
using System.Collections;


public class Character : MonoBehaviour
{
    public float health;
    public int moveSpeed;

    public void takeDamage ( float iDamage )
    {
        float damage = iDamage;
        // DO ANY DAMAGE MODIFIERS ETC HERE.

        // PLAYER DIED
        if ( health - damage <= 0 )
        {
            Debug.Log("DOING DAMAGE!!");
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
        // ALL DEATH LOGIC HERE.
    }

}
