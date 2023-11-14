using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerpos : MonoBehaviour
{
    private GameManager gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        transform.position = gm.lastCheckPointPos;
    }

}
