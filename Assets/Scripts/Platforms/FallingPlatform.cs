using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float fallDelay;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlatformManager.Instance.StartCoroutine("SpawnPlatform",
                new Vector2(transform.position.x, transform.position.y));
            StartCoroutine(Fall());
            Destroy(gameObject, 4f);
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.isKinematic = false;
        
        
    }

}
