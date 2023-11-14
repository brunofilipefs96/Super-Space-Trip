using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector2 speed;
    public float distance;
    public LayerMask whatIsSolid;
    public int damage;
    public GameObject ExplosionGO;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
        SoundManager.PlaySound("shot");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = speed;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                ScreenShake.instance.StartShake(.5f, .3f);
            }
            if (hitInfo.collider.CompareTag("Ground"))
            {
                ScreenShake.instance.StartShake(.2f, .1f);
            }
            SoundManager.PlaySound("shotHit");
            PlayExplosion();
            Destroy(gameObject);
        }
        
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
        Destroy(explosion);
    }
}
