using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public ParticleSystem dust;

    // Greift auf den Animator zu
    private Animator animator;

    // Variablen für Geschwindigkeit, Sprungkraft und Bewegung
    [Header("Movement")]
    public float speed;
    public float jumpForce;
    private float moveInput;

    // ruft den 2D Körper unter dem Kürzel rb ab
    private Rigidbody2D rb;

    // Abfrage, in welche Richtung die Figur schaut -> Flip
    private bool facingRight = true;

    [Header("Knockback")]
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    // Abfrage mittels Radius, ob die Figur sich auf dem Boden befindet
    // Außerdem fragt es ab, ob etwas als Boden deklariert ist
    [Header("Groundcheck")]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // Abfrage, wie lang die Figur sich mit dem ersten Sprung nach oben bewegen kann,
    // bevor sie durch die Schwerkraft wieder fällt
    // Außerdem fragt es ab, ob sich die Figur gerade im Sprung befindet
    [Header("Jumps")]
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public GameObject characterHolder;

    // Legt die Anzahl der Zusatzsprünge fest (1 = Doppelsprung, 2 = Triplesprung, usw)
    private int extraJumps;
    public int extraJumpsValue;

    public AudioSource jumpSounds;


    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        // Hier wird überprüft, ob die erlaubten Zusatzsprünge freigegeben werden
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;

        }
        // Hier wird die Abfrage, ob sich die Figur in der Luft befindet, ausgeführt
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("Grounded", false);
            Coroutine coroutine = _ = StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
            jumpSounds.Play();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button0) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Joystick1Button0) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
           
                dust.Play();
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            isJumping = false;
        }
        animator.SetBool("Grounded", isGrounded);
    }
        

    // Hier wird die Abfrage nach der Bodenberührung ausgeführt
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Hier ist die Steuerung festgelegt
        moveInput = Input.GetAxis("Horizontal");
        
        if (knockbackCount <= 0)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else
        {
            if (knockFromRight)
                rb.velocity = new Vector2(-knockback, knockback);
            if (!knockFromRight)
                rb.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }


        // Hier werden die Abfrage der Blickrichtung und - falls nötig - der Flip ausgeführt
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

    }




    // Hier wird der Flip bei Richtungswechsel ausgeführt
    void Flip()
    {
        CreateDust();
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }

    void CreateDust()
    {
    dust.Play();
    }
}
