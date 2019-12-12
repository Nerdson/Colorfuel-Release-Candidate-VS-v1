using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToGive;

    public AudioSource pickupSounds;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController2D>() == null)
            return;

        HealthManager.HurtPlayer(-healthToGive);

        pickupSounds.Play();

        Destroy(gameObject);
    }
}
