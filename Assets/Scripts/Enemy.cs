using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject MonsterDead;

    private void Update()
    {
        if (health <= 0)
        {
            SoundManager.PlaySound("enemyDeath");
            PlayMonsterDead();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void PlayMonsterDead()
    {
        GameObject dead = (GameObject)Instantiate(MonsterDead);
        dead.transform.position = transform.position;
    }
}
