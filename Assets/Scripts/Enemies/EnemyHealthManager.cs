using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int enemyHealth;

    public AudioSource enemyDeathSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
            enemyDeathSounds.Play();
            Destroy(gameObject);
        }
    }
    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
    }
}
