using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Checkpoint
    public Vector2 lastCheckPointPos;

    //Coins
    public TextMeshProUGUI coins;
    int score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        coins.text = score.ToString();
    }
}
