using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

// Greift auf den Animator zu
    private Animator animator;

    // Variablen für Geschwindigkeit, Sprungkraft und Bewegung
    public float speed;
    public float jumpForce;
    private float moveInput;

// ruft den 2D Körper unter dem Kürzel rb ab
    private Rigidbody2D rb;

// Abfrage, in welche Richtung die Figur schaut -> Flip
    private bool facingRight = true;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

// Abfrage mittels Radius, ob die Figur sich auf dem Boden befindet
// Außerdem fragt es ab, ob etwas als Boden deklariert ist
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

// Abfrage, wie lang die Figur sich mit dem ersten Sprung nach oben bewegen kann,
// bevor sie durch die Schwerkraft wieder fällt
// Außerdem fragt es ab, ob sich die Figur gerade im Sprung befindet
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

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
        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;

        }
// Hier wird die Abfrage, ob sich die Figur in der Luft befindet, ausgeführt
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("Grounded", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                jumpSounds.Play();
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        animator.SetBool("Grounded", isGrounded);

        // Controller-Steuerung fürs Springen
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("Grounded", false);
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
                jumpSounds.Play();
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
        Debug.Log(moveInput);
        if(knockbackCount <= 0)
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
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
