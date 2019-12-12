using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingNut : MonoBehaviour
{
    public int damageToGive;

    public Animator animator;

    public AudioSource hurtSounds;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Crystine"))
            rb.isKinematic = false;
    }

    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.name == "Crystine")
        {
            animator.SetTrigger("Hurt");

            HealthManager.HurtPlayer(damageToGive);

            hurtSounds.Play();

            var player = other.GetComponent<PlayerController2D>();
            player.knockbackCount = player.knockbackLength;

            if (other.transform.position.x < transform.position.x)
                player.knockFromRight = true;
            else
                player.knockFromRight = false;

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
