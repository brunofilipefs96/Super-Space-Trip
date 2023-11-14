using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public GameObject CoinDestroy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            GameManager.instance.ChangeScore(coinValue);
            CoinDestroyer();
            Destroy(gameObject);
        }
    }

    void CoinDestroyer()
    {
        GameObject dead = (GameObject)Instantiate(CoinDestroy);
        dead.transform.position = transform.position;
    }
}
