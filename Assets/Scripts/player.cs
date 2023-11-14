using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class player : MonoBehaviour
{
    //GameManager
    private GameManager gm;

    //Colisões
    private bool waterTouch = false;

    //Balas
    public GameObject leftBullet, rightBullet;

    //Disparo
    public float startTimeBtwShots;
    private float timeBtwShots;

    //Vida
    public int HP = 2;
    public int Lifes = 3;
    public TextMeshProUGUI LifesUI;
    public Animator hpAnim;
    private bool dead = false;

    //Outras Variaveis
    private Rigidbody2D rb;
    private Animator anim;
    private float moveSpeed;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;
    Transform firePos;

    //Salto
    public float hangTime = .2f;
    public float hangCounter;
    private bool isGrounded;

    public float jumpBufferLength = .1f;
    public float jumpBufferCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        moveSpeed = 7f;
        firePos = transform.Find("firePos");
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxisRaw("Horizontal") * moveSpeed;

        //Hangtime
        if (isGrounded)
            hangCounter = hangTime;
        else
            hangCounter -= Time.deltaTime;

        //Jump Buffer
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
            jumpBufferCount = jumpBufferLength;
        else
            jumpBufferCount -= Time.deltaTime;

        //Saltar
        if (jumpBufferCount >= 0 && hangCounter > 0)
        {
            if (!dead)
            {
                anim.SetBool("isJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, 15f);
                jumpBufferCount = 0;
                SoundManager.PlaySound("jump");
            }
        }
        if (CrossPlatformInputManager.GetButtonUp("Jump") && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        if (isGrounded)
            anim.SetBool("isJumping", false);
        //

        //Animações
        anim.SetFloat("Speed", Mathf.Abs(dirX));

        //Disparar
        if (CrossPlatformInputManager.GetButtonDown("Shoot"))
        {
            if (!dead)
            {
                anim.SetBool("isShooting", true);
                rb.velocity = Vector2.zero;
            }
        }
        if (CrossPlatformInputManager.GetButtonUp("Shoot"))
            anim.SetBool("isShooting", false);

        if (timeBtwShots <= 0)
        {

            if (CrossPlatformInputManager.GetButtonDown("Shoot"))
            {
                if (!dead)
                {
                    Fire();
                    timeBtwShots = startTimeBtwShots;
                }
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        //

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    private void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Coins")
        {
            SoundManager.PlaySound("coins");
        }
        if (other.tag=="Enemy")
        {
            HpSystem();
        }
        if (other.tag=="Water")
        {
            if (waterTouch == false)
            {
                HpSystem();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform")) { 
            this.transform.parent = col.transform;
            isGrounded = true;
        }
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform")) { 
            this.transform.parent = null;
            isGrounded = false;
        }
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void Fire()
    {
        if (facingRight)
            Instantiate(rightBullet, firePos.position, Quaternion.identity);
        if (!facingRight)
            Instantiate(leftBullet, firePos.position, Quaternion.identity);
    }

    //Ser atacado por inimigos
    private void HpSystem()
    {
        if (waterTouch == true)
        {
            HP = 0;
            if (Lifes > 0)
            {
                Lifes -= 1;
                hpAnim.SetInteger("HP", HP);
                LifesUI.text = Lifes.ToString();
                waterTouch = true;
                hpAnim.SetBool("waterTouch", true);
                anim.SetBool("isDead", true);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                (this.GetComponent(typeof(CircleCollider2D)) as Collider2D).enabled = false;
                (this.GetComponent(typeof(BoxCollider2D)) as Collider2D).enabled = false;
                dead = true;
                Invoke("Respawn", 1);
                waterTouch = false;
                Debug.Log("WATER TOUCH IF|||| Lifes: " + Lifes + "  |  HP: " + HP);
            }
        }
        else
        {
            HP -= 1;
            hpAnim.SetInteger("HP", HP);
            Debug.Log("WATER TOUCH ELSE|||| Lifes: " + Lifes + "  |  HP: " + HP);
            if (HP <= 0)
            {
                anim.SetBool("isDead", true);
                if (Lifes > 0)
                {
                    Lifes -= 1;
                    LifesUI.text = Lifes.ToString();
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    (this.GetComponent(typeof(CircleCollider2D)) as Collider2D).enabled = false;
                    (this.GetComponent(typeof(BoxCollider2D)) as Collider2D).enabled = false;
                    dead = true;
                    Invoke("Respawn", 1);
                }
                else
                {
                    Destroy(gameObject);
                }

            }
        } 
    }

    //Respawn
    public void Respawn()
    {
        this.transform.position = gm.lastCheckPointPos;
        hpAnim.SetInteger("HP", 2);
        hpAnim.SetBool("waterTouch", false);
        anim.SetBool("isDead", false);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        (this.GetComponent(typeof(CircleCollider2D)) as Collider2D).enabled = true;
        (this.GetComponent(typeof(BoxCollider2D)) as Collider2D).enabled = true;
        dead = false;
        HP = 2;
    }

}


