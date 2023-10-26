using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Hank : MonoBehaviour
{

    public float health = 10f;
    public float baseHealth = 10f;
    public float damage = 2f;
    public float baseDamage = 2f;
    public float knockback = 0f;
    public bool isDead = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) isDead = true;
    }

    public void GainHealth(float healthAmount)
    {
        health += healthAmount;
        if (health > baseHealth) health = baseHealth;
    }

    public void TakeKnockback(Vector2 direction)
    {
        Rigidbody2D parentBody = gameObject.GetComponent<Rigidbody2D>();
        if (parentBody)
        {
            parentBody.AddForce(new Vector2(direction.x, direction.y) * knockback);
        }
    }
}
