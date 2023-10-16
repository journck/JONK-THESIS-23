﻿using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Character : MonoBehaviour
{
    [Header("Character - Inscribed")]
    public float health;
    public float maxHealth;
    public int moveSpeed;
    public float bulletMoveSpeed;
    public float bulletDamage;


    public virtual void TakeDamage ( float iDamage )
    {
        float damage = iDamage;
        // DO ANY DAMAGE MODIFIERS ETC HERE.

        // PLAYER DIED
        if ( health - damage <= 0 )
        {
            Debug.Log("DEAD!");
            health = 0;
            DoDeath();
        }
        else
        {
            health -= damage;
        }
    }

    public void DoDeath()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
