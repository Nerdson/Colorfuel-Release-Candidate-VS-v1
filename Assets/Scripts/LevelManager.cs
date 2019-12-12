using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public float respawnDelay;

    public GameObject currentCheckpoint;

    private PlayerController2D player;

    public HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController2D>();

        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        player.enabled = false;
        
        GetComponent<Renderer>();
       
        yield return new WaitForSeconds(respawnDelay);
        
        player.transform.position = currentCheckpoint.transform.position;

        player.knockbackCount = 0;
        
        player.enabled = true;
        
        GetComponent<Renderer>();
        
        healthManager.FullHealth();
       
        healthManager.isDead = false;
    }

}
