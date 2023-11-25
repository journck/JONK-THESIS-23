using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Character : MonoBehaviour
{
    [Header("Character - Inscribed")]
    public float health;
    public float maxHealth;
    public int baseMoveSpeed;
    public Field parentField;



    public virtual void TakeDamage ( float iDamage )
    {
        float damage = iDamage;
        // DO ANY DAMAGE MODIFIERS ETC HERE.

        if ( health - damage <= 0 )
        {
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
        if ( this is Player )
        {
            GameManager.instance.CheckForRestart();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool IsDead()
    {
        return this.health <= 0;
    }

}
