using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gm;
    private bool active = false;
    public Animator CheckpointActiveAnim;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (active==false))
        {
            gm.lastCheckPointPos = transform.position;
            CheckpointActiveAnim.SetBool("Active", true);
            SoundManager.PlaySound("checkpoint");
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckpointActiveAnim.SetBool("Active", false);
    }
}
